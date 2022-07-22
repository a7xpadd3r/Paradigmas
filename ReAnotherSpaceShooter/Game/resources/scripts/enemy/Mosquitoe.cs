using System;
using System.Numerics;

namespace Game
{
    public class Mosquitoe : ShipObject
    {
        // IA
        private Type1IA ai;

        private iWeapon CurrentWeapon;
        private int PointsGiven = 100;
        private bool Special = false;
        private Random random = new Random();

        public Action OnEnemyDeath;

        public Mosquitoe(int newID, string newOwner = "Enemy") 
        { 
            this.objectOwner = newOwner; this.objectID = newID;
            this.objectTag = "Ship";
        }
        public void Awake(Vector2 spawnPos, bool newSpecial = false, MovementType movType = MovementType.Lineal, AxisY addAxisY = AxisY.None, float minX = -100, float maxX = 2020, float minY = -100, float maxY = 1180)
        {
            this.PointsGiven = 100;
            this.Special = newSpecial;
            if (this.Special)
            {
                this.Ship = ShipsProperties.SpecialMosquitoe;
                this.PointsGiven = 150;
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy3);
            }
            else
            {
                this.Ship = ShipsProperties.Mosquitoe;
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy1);
            }

            this.objectAnimation = this.Ship.ShipAnim;
            this.objectTransform = new Transform(spawnPos, new Vector2(0.7f, 0.7f));
            this.objectCollider = new Collider(this.Ship.Damage, this.Owner, this.Tag, this.ShipCollidersVectors, this.ID);
            this.objectCollider.OnCollision += OnHit;

            this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy1);
            mGameObject.AddGameObject(this);
            this.objectActive = true;

            ai = new Type1IA(this.Ship.MaxSpeed, this.Position, movType, -100, 2020, addAxisY);
        }
        private void OnHit(Collider instigator)
        {
            if (instigator.colliderOwner != "World")
            {
                var fx = fyPoolDay.Pool.CreateEffect(EffectsAnimations.Smoke1);
                fx.Awake(this.Position - new Vector2(45, 50), new Vector2(1.5f, 1.5f));

                if (this.Special)
                {
                    int randreward = random.Next(0, 100);
                    ItemType type = ItemType.Repair;

                    if (randreward > 50 && randreward < 90) type = ItemType.Shield;
                    else if (randreward > 90) type = ItemType.Special;
                    GameManager.SpawnItem(this.Position, type);
                }

                this.objectCollider.OnCollision -= OnHit;
                mGameObject.RemoveGameObject(this);
                this.objectCollider = null;
                this.OnEnemyDeath?.Invoke();
                GameManager.OnScoreUpdate?.Invoke(this.PointsGiven);
            }
        }
        public override void Update()
        {
            float t = Program.GetDeltaTime;
            this.ai.Update(t);

            this.objectTransform.UpdatePosition(this.ai.IAPosition);
            this.objectCollider.UpdateOwnerPosition(this.Position);
            this.objectAnimation.Update();
            this.Render();

            if (this.CurrentWeapon != null)
            {
                this.CurrentWeapon.Update(t);
                this.CurrentWeapon.Fire(this.Owner, this.Position - this.Ship.RailPosition);
            }
        }
        private void Render() { Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.objectTransform); }
        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
    }

}
