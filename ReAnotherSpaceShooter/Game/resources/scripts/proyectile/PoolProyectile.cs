using System;
using System.Numerics;

namespace Game
{
    public enum Direction { Up, Down, Left, Right }
    public class PoolProyectile : GameObject
    {
        // Basic
        private string owner;
        private int objectid;
        private ProyectileData pdata;
        private Direction proyectiledirection = Direction.Up;
        private WeaponTypes type;

        // Other stats
        private bool iteration = false;
        private int maxhits = 1;
        private int currenthits = 0;
        private Collider lasthit;

        // Events
        public Action OnSleep;

        public PoolProyectile(string newOwner, int newID)
        {
            this.owner = newOwner;
            this.objectID = newID;
        }
        public void Awake(Vector2 spawnPosition, WeaponTypes newType, Direction newDirection = Direction.Up, int newMaxHits = 1, bool newIterates = false)
        {
            this.iteration = newIterates;
            this.objectid = this.objectID;
            this.objectOwner = this.owner;
            this.objectTag = "Proyectile";
            this.objectTransform = new Transform(spawnPosition);
            this.type = newType;

            float dmg = 0;
            switch (type)
            {
                case WeaponTypes.BlueRail:          
                    this.pdata = ProyectileProperties.BlueRail;                                  break;
                case WeaponTypes.RedDiamond:        
                    this.pdata = ProyectileProperties.RedDiamond;                                break;
                case WeaponTypes.RedDiamondBall:    
                    this.pdata = ProyectileProperties.RedDiamondBall;                            break;
                case WeaponTypes.GreenCrast:        
                    this.pdata = ProyectileProperties.GreenCrast; this.maxhits = newMaxHits; this.objectTransform.UpdateScale(new Vector2(2, 2)); break;
                case WeaponTypes.HeatTrail:
                    break;
                case WeaponTypes.OrbWeaver:
                    break;
                case WeaponTypes.Gamma:
                    break;
                case WeaponTypes.Enemy1:
                    break;
                case WeaponTypes.Enemy2:
                    break;
                case WeaponTypes.Enemy3:
                    break;
                default:
                    break;
            }

            this.proyectiledirection = newDirection;
            this.objectAnimation = this.pdata.Animation;
            this.objectCollider = new Collider(dmg, this.objectOwner, this.objectTag, spawnPosition, this.objectAnimation.TextureSize, this.pdata.colliderVectors, this.objectID);
            this.objectCollider.OnCollision += OnHit;
            mGameObject.AddGameObject(this);
            this.objectActive = true;
        }
        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
        public override void Update()
        {
            this.objectAnimation.Update();
            this.objectCollider.UpdateOwnerPosition(this.Position);
            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.Position);
            this.objectCollider.CheckForCollisions();

            float posX = this.Position.X;
            float posY = this.Position.Y;

            switch (proyectiledirection)
            {
                case Direction.Up:      posY -= this.pdata.MaxSpeed * Program.GetDeltaTime; break;
                case Direction.Down:    posY += this.pdata.MaxSpeed * Program.GetDeltaTime; break;
                case Direction.Left:    posX -= this.pdata.MaxSpeed * Program.GetDeltaTime; break;
                case Direction.Right:   posX += this.pdata.MaxSpeed * Program.GetDeltaTime; break;
            }

            Vector2 finalposition = new Vector2(posX, posY);
            this.objectTransform.UpdatePosition(finalposition);

            if (posY < -100 || posY > 1180 || posX < -100 || posX > 2020) Sleep();
        }
        private void OnHit(Collider instigator)
        {
            if (currenthits < maxhits && this.lasthit != instigator && instigator.ColliderOwner != "World")
            {
                currenthits++;
                var fx = fyPoolDay.Pool.CreateEffect(EffectsAnimations.PurpleBeam);
                fx.Awake(this.Position - this.pdata.beamStats.Position, this.pdata.beamStats.Scale);

                if (this.iteration) // If this proyectile makes something else than damage after hit..
                {
                    int newID = mGameObject.GenerateObjectID();
                    lasthit = instigator;
                    switch (type)
                    {
                        case WeaponTypes.BlueRail:
                            break;
                        case WeaponTypes.RedDiamond:    // Red Diamond spawns balls
                            var pRedDiamondBallLeft = fyPoolDay.Pool.CreateProyectile(owner, newID);
                            var pRedDiamondBallRight = fyPoolDay.Pool.CreateProyectile(owner, newID);
                            pRedDiamondBallLeft.Awake(this.Position + new Vector2(0,10), WeaponTypes.RedDiamondBall, Direction.Left);
                            pRedDiamondBallRight.Awake(this.Position + new Vector2(0, 10), WeaponTypes.RedDiamondBall, Direction.Right);
                            break;
                        case WeaponTypes.RedDiamondBall:
                            break;
                        case WeaponTypes.GreenCrast:
                            break;
                        case WeaponTypes.HeatTrail:
                            break;
                        case WeaponTypes.OrbWeaver:
                            break;
                        case WeaponTypes.Gamma:
                            break;
                        case WeaponTypes.Enemy1:
                            break;
                        case WeaponTypes.Enemy2:
                            break;
                        case WeaponTypes.Enemy3:
                            break;
                        default:
                            break;
                    }
                }
            }

            if (currenthits >= maxhits) Sleep();
        }
        public override void Sleep()
        {
            mGameObject.RemoveGameObject(this);
            this.objectCollider.OnCollision -= OnHit;
            this.objectCollider = null;
            this.lasthit = this.objectCollider;
            this.currenthits = 0;
            this.iteration = false;

            OnSleep?.Invoke();
        }
    }
}
