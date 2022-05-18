using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyMini : GameObject, IShooter, IDamageable, IEnemy
    {
        //Campos
        private bool moveLeft;
        private bool moveRight;
        private int damage;
        private MovementController movementController;
        private Collider BoxCollider;
        public event Action OnReload;
        public event Action<GameObject> OnDestroy;
        public event Action OnShoot;
        public event Action OnEmptyAmmo;

        //Propiedades
        public int Damage { get; private set; }
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

        //Constructor
        public EnemyMini(string name, int damage, float speed, Vector2 starPosition, Vector2 scale, float cooldown, Bullet bulletType, int maxLife, int maxAmmo, List<Animation> animations, float angle = 0, string tag = "Enemies") : base(name, starPosition, scale, angle)
        {
            Tag = tag;
            //Engine.Debug($"Se creo un EnemyMini de Tag: {this.Tag}");
            DisplayName = name;
            CurrentSpeed = speed;
            IsTrigger = true;
            moveLeft = true;
            Damage = damage;
            this.HealthController = new HealthController(maxLife);
            movementController = new MovementController();
            Cooldwon = cooldown;
            BulletType = bulletType;
            this.HealthController.OnDie += Destroy;
            this.HealthController.OnGetDamage += OnGetDamageHandler;
            BoxCollider = new Collider(this, ColliderType.Box, true);
            BoxCollider.OnCollision += OnCollision;
            MaxAmmo = maxAmmo;
            ReloadMaxAmmo();
            CreateAnimations();
            currentAnimation = animationFactory.IdleAnimationEnemyMini;
        }

        //Metodos
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
            BoxCollider.CheckCollision();
          
        }
        public override void Render()
        {
            base.Render();
        }

        private void Movement()
        {
            if (this.Position.X > Program.WIDTH_SCREEN - 100)
            {
                moveLeft = true;
            }
            if (this.Position.X < 20)
            {
                OnDestroy?.Invoke(this);
            }
            if (!moveLeft)
            {
                movementController.StartMovementX(this, CurrentSpeed, 0);
                movementController.StartMovementY(this, CurrentSpeed, 0.5f);
            }

            if (moveLeft)
            {
                movementController.StartMovementX(this, CurrentSpeed, -1);
                movementController.StartMovementY(this, CurrentSpeed, 0.5f);
            }

            if (this.Position.Y > Program.HEIGHT_SCREEN - 5)
            {
                OnDestroy?.Invoke(this);
            }
        }

        protected override void CreateAnimations()
        {
            animationFactory.CreateAnimationsEnemyMini();
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
        public void OnCollision(GameObject gameObject)
        {          
            if (gameObject is BulletController2)
            {
                var bullet = (BulletController2)gameObject;
                if (bullet.Tag == "BulletPlayer")
                {
                    GetDamage(bullet.Damage);
                    bullet.Hit();
                }
            }
        }

        public void Hit()
        {
            ColliderManager.RemoveCollider(BoxCollider);
            GameObjectManager.RemoveGameObject(this);
        }

        public void GetDamage(int Damage)
        {
            HealthController.GetDamage(Damage);
        }

        public void Destroy()
        {
            Killing();
            if (IsDestroyed) return;
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }

   
        private void OnGetDamageHandler(int currentLife, int damage)
        {
            Engine.Debug($"{this.DisplayName} recibio {damage} de daño, su vida actual es {currentLife}");
        }

        public void GetHeal(int heal)
        {
            
        }

        public void Killing()
        {
            Points.Instance.AddScore();
            Points.Instance.ShowScore();
            ColliderManager.RemoveCollider(BoxCollider);
            OnDestroy?.Invoke(this);
        }
    }
}
