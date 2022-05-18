using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyFast : GameObject, IShooter, IDamageable
    {
        private bool moveLeft;
        private bool moveRight;
        private MovementController movementController;
        private readonly Collision collision;
        public HealthController HealthController { get; }
        public float CurrentSpeed { get; private set; }
        public float Cooldwon { get; private set; }

        public float CurrentCooldown { get; private set; }

        public Bullet BulletType { get; private set; }

        public int MaxAmmo { get; private set; }

        public int CurrentAmo { get; private set; }

        public bool IsDestroyed { get; private set; }

        public float Cooldown { get; private set; }

        public int CurrentAmmo { get; private set; }

        public event Action OnReload;
        public event Action<GameObject> OnDestroy;
        public event Action OnShoot;
        public event Action OnEmptyAmmo;


        public EnemyFast(string name, float speed, Vector2 starPosition, Vector2 scale, float cooldown, Bullet bulletType, int maxLife, int maxAmmo, string tag = "Enemies", float angle = 0) : base(name, starPosition, scale, angle)
        {
            Tag = tag;
            Engine.Debug($"Se creo un EnemyRange de Tag: {this.Tag}");
            DisplayName = name;
            CurrentSpeed = speed;
            IsTrigger = true;
            moveLeft = true;
            this.HealthController = new HealthController(maxLife);
            movementController = new MovementController();
            Cooldwon = cooldown;
            BulletType = bulletType;
            this.HealthController.OnDie += Destroy;
            this.HealthController.OnGetDamage += OnGetDamageHandler;
            collision = new Collision();
            collision.OnCollision += OnCollisionHandler;
            collision.OffCollision += OffCollisionHandler;
            MaxAmmo = maxAmmo;
            ReloadMaxAmmo();
            CreateAnimations();
            currentAnimation = renderer.IdleAnimationEnemyFast;
        }




        public override void Update()
        {
            base.Update();
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.DeltaTime;
            }
            if (CurrentCooldown < 0)
            {
                CurrentCooldown = 0;

            }
            Movement();
            Shoot(new Vector2(0, 1));
            GetDamage(10);
        }
        public override void Render()
        {
            base.Render();
        }
        private void Movement()
        {
            if (this.PositionGameObject.X > Program.WIDTH_SCREEN - 55)
            {
                moveLeft = true;
            }
            if (this.PositionGameObject.X < 55)
            {
                moveLeft = false;
            }
            if (!moveLeft)
            {
                movementController.StartMovementX(this, CurrentSpeed, 1, -1, 1, 0, 0);

            }

            if (moveLeft)
            {
                movementController.StartMovementX(this, CurrentSpeed, -1, -1, 1, 0, 0);
            }
        }
        protected override void CreateAnimations()
        {
            renderer.CreateAnimationsEnemyFast();
        }

        public void ReloadMaxAmmo()
        {
            CurrentAmo = MaxAmmo;
            OnReload?.Invoke();
        }

        public void Reload(int ammo)
        {
            CurrentAmo += ammo;
            if (CurrentAmo > MaxAmmo)
            {
                CurrentAmo = MaxAmmo;
            }
            OnReload?.Invoke();
        }

        public void Shoot(Vector2 direction)
        {
            if (CurrentCooldown <= 0 && CurrentAmo > 0)
            {
                CurrentCooldown = Cooldwon;
                CurrentAmo -= 1;
                IBullet bullet = BulletFactory.CreateBullet(BulletType, transform.Position);
                bullet.Direction = direction;
            }
        }
        public void GetDamage(int damage)
        {
            var listGameObject = GameObjectManager.ActiveGameObjects;
            for (int i = 0; i < listGameObject.Count; i++)
            {
                if (collision.CheckCollision(this, listGameObject[i], "BulletPlayer"))
                {
                    Engine.Debug($"{this.DisplayName} Recibira un daño de {damage}");
                    this.HealthController.GetDamage(damage);
                }
            }
        }

        public void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }
        private void OnCollisionHandler(string thisNameGameObject, string nameGameObjectsToCollide)
        {
            Engine.Debug($"{thisNameGameObject} Collisiono contra {nameGameObjectsToCollide}");
        }
        private void OffCollisionHandler(string thisGameObject)
        {
            Engine.Debug($"{thisGameObject} no esta collisionando con nada");
        }
        private void OnGetDamageHandler(int currentLife, int damage)
        {
            Engine.Debug($"{this.DisplayName} recibio {damage} de daño, su vida actual es {currentLife}");
        }
    }
}
