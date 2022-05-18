using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{  
    public class PlayerController : GameObject, IOwnWeapon, IDamageable, IItems
    {
        //Campos
        public Collider BoxCollider;
        private MovementController movementController;
        public Items item;
        public event Action<GameObject> OnDestroy;
      
        private int currentWeaponsIndex = -1;
        private float boostTimer;
        private bool boosting;
            
        //Propiedades
        public float CurrentSpeed { get; private set; }
        public HealthController HealthController { get; }
        public SpeedBoostItem SpeedBoostItem { get; }
        public bool IsDestroyed { get; private set; }
        public Vector2 OwnerPosition => transform.Position;
        public Vector2 OwnerDirection { get; private set; }
        public float OwnerOffsetX { get; private set; }
        public float OwnerOffsetY { get; private set; }
        public List<IWeapon> Weapons { get; private set; } = new List<IWeapon>();
        public IWeapon CurrentWeapon => Weapons[currentWeaponsIndex];
        public Item Type { get; }
        public int Heal { get; }
        public float Speed { get; }

        //Constructor
        public PlayerController(string name, Vector2 transformPosition, float speed,float maxSpeed ,Vector2 transformScale, int maxLife, List<Animation> animations, float transformAngle = 0f) : base(name, transformPosition, transformScale, transformAngle) // --Parametros del constructor-- //
        {
            Tag = "Player";
            Engine.Debug($"Se creo un PlayerController de Tag:{this.Tag}");
            IsTrigger = true;
            DisplayName = name;
            this.CurrentSpeed = speed;
            boostTimer = 0;
            boosting = false;
            BoxCollider = new Collider(this, ColliderType.Box, true);
            movementController = new MovementController();
            SpeedBoostItem = new SpeedBoostItem(maxSpeed);
            HealthController = new HealthController(maxLife);
            HealthController.OnDie += Destroy;       
            HealthController.OnGetDamage += OnGetDamageHandler;
            SpeedBoostItem.OnGetSpeed += OnGetCurrentSpeedHandler;
            HealthController.OnGetHeal += OnGetHealHandler;
            BoxCollider.OnCollision += OnCollision;
            BoxCollider.OnCollision += OnCollisionSpeed;
            BoxCollider.OnCollision += OnCollisionHealth;
            CreateAnimations();
            currentAnimation = animationFactory.IdleAnimationPlayer;
        }

        //Metodos
        public override void Update()
        {
            base.Update();
            if (boosting)
            {
                boostTimer += Time.DeltaTime;
                if (boostTimer >= 3)
                {
                    CurrentSpeed = 200;
                    GetSpeed(CurrentSpeed * 1);
                    boostTimer = 0;
                    boosting = false;             
                }
            }
           
            CanAttack();
            Move();
            BoxCollider.CheckCollision();

        }
        public void OnCollisionSpeed(GameObject gameObject)
        {
             if(gameObject is SpeedBoostItem)
             {
                var speedBoost = (SpeedBoostItem)gameObject;                             
                boosting = true;
                CurrentSpeed = 400;
                GetSpeed(CurrentSpeed * 1);
                //Engine.Debug("Obtuviste una velocidad de 400");
                speedBoost.Hit();                 
             }             
        }   
        public void OnCollisionHealth(GameObject gameObject)
        {
            if (gameObject is Items)
            {
                var healthItem = (Items)gameObject;
                GetHeal(healthItem.Heal);
                healthItem.Hit();
            }
        }
        public void OnCollision(GameObject gameObject)
        {
            if (gameObject is Enemy)
            {
                var enemy = (Enemy)gameObject;
                if (enemy.Tag == "Enemies")
                {
                    GetDamage(enemy.Damage);
                    enemy.Hit();
                }
            }

            if (gameObject is EnemyLow)
            {
                var enemyLow = (EnemyLow)gameObject;
                if (enemyLow.Tag == "Enemies")
                {
                    GetDamage(enemyLow.Damage);
                    enemyLow.Hit();
                }
            }

            if (gameObject is EnemyFast)
            {
                var enemyFast = (EnemyFast)gameObject;
                if (enemyFast.Tag == "Enemies")
                {
                    GetDamage(enemyFast.Damage);
                    enemyFast.Hit();
                }
            }

            if (gameObject is EnemyMini)
            {
                var enemyMini = (EnemyMini)gameObject;
                if (enemyMini.Tag == "Enemies")
                {
                    GetDamage(enemyMini.Damage);
                    enemyMini.Hit();
                }
            }

            if (gameObject is EnemyTank)
            {
                var enemyTank = (EnemyTank)gameObject;
                if (enemyTank.Tag == "Enemies")
                {
                    GetDamage(enemyTank.Damage);
                    enemyTank.Hit();
                }
            }

            if (gameObject is BulletController)
            {
                var bullet = (BulletController)gameObject;
                if (bullet.Tag == "BulletEnemy")
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

        public void AddWeapon(IWeapon weapon)
        {
            weapon.AssignOwner(this);
            Weapons.Add(weapon);

            if (Weapons.Count == 1)
            {
                SetNextWeapon();
            }
        }
        private void SetNextWeapon()
        {
            currentWeaponsIndex++;
            if (currentWeaponsIndex >= Weapons.Count)
            {
                currentWeaponsIndex = 0;
            }
        }     
        protected override void CreateAnimations()
        {
            animationFactory.CreateAnimationsPlayer();
        }
        private void CanAttack()
        {
            if (Weapons != null)
            {
                CurrentWeapon.Update();
            }
            if (Engine.GetKey(Keys.SPACE) && Weapons != null)
            {
                CurrentWeapon.StartAttack();
            }
        }
        private void Move()
        {
            if (Engine.GetKey(Keys.A))
            {
                OwnerDirection = new Vector2(0, -1);
                movementController.StartMovementX(this, CurrentSpeed, -1);
                InputAnimationController(animationFactory.IdleAnimationPlayer);             
            }

            if (Engine.GetKey(Keys.D))
            {
                OwnerDirection = new Vector2(0, -1);
                movementController.StartMovementX(this, CurrentSpeed,1);
                InputAnimationController(animationFactory.IdleAnimationPlayer);          
            }

            if (Engine.GetKey(Keys.W))
            {
                OwnerDirection = new Vector2(0, -1);             
                movementController.StartMovementY(this, CurrentSpeed, -1);
                InputAnimationController(animationFactory.IdleAnimationPlayer);
            }

            if (Engine.GetKey(Keys.S))
            {
                OwnerDirection = new Vector2(0, -1);
                movementController.StartMovementY(this, CurrentSpeed, 1);
                InputAnimationController(animationFactory.IdleAnimationPlayer);            
            }

            if (Engine.GetKey(Keys.LCONTROL))
            {
                SetNextWeapon();
            }
        }
        private void InputAnimationController(Animation animation)
        {
            currentAnimation = animation;
        }
        public void GetDamage(int damage)
        {
            HealthController.GetDamage(damage);
        }
        public void GetHeal(int heal)
        {
            HealthController.GetHeal(heal);
        }      
        public void GetSpeed(float speed)
        {
            SpeedBoostItem.GetSpeed(speed);
        }  
        public void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }

        private void OnGetDamageHandler(int currentLife, int damage)
        {
            Engine.Debug($"\nEl {this.DisplayName} recibio {damage} de daño, su vida actual es {currentLife}");
        }
         
        private void OnGetHealHandler(int currentLife, int heal)
        {
            Engine.Debug($"\nEl {this.DisplayName} recibio {heal} de curación, su vida actual es {currentLife}");
        }
       
        private void OnGetCurrentSpeedHandler(float currentSpeed, float speed)
        {
            SpeedBoostItem.GetCurrentSpeed(speed);
            Engine.Debug($"\nEl {this.DisplayName} recibio {speed} de velocidad");
          
        }

        public override void Render()
        {
            base.Render();
        }
    }
}