using System;
using System.Collections.Generic;

namespace Game
{
    public class EnemyController : GameObject
    {
        //Campos       
        private float speed;
        private int maxHealth;
        private float shootCooldown;



        //Events
        public Action OnWin;
        public Action onDie;


        //Propiedades
        public HealthController HealthController { get; private set; }
        public ShooterController ShooterController { get; private set; }
  


        //Constructor
        public EnemyController(Vector2 initialPosition, float scale, float angle, float speed, int maxHealth, float shootCooldown, List<Animation> animations)
            : base (initialPosition, scale, angle, animations)
        {

            this.speed = speed;
            this.maxHealth = maxHealth;
            this.shootCooldown = shootCooldown;
            HealthController = new HealthController(this.maxHealth);
            ShooterController = new ShooterController(this.shootCooldown);
            

            currentAnimation = GetAnimationEnemy("IdleEnemy");
            currentAnimation.Reset();
           
        }


        //Metodos

        //Update
        public override void Update()
        {
           
            //Position
            Position += new Vector2(1, 0) * speed * Time.DeltaTime;


            //isAlive
            if (HealthController.isAlive)
            {
                CheckCollisions();
                ShooterController.Update();
            }

            //Shoot
            if (ShooterController.CanShoot)
            {
               var direction = new Vector2(0, 1);
               BulletController bullet = ShooterController.Shoot(Position, direction, 180);
               LevelScene.EnemyBullets.Add(bullet);
               ShooterController.Update();
            }
            base.Update();
        }

        //Health
        public int CurrentHealth
        {
            get
            {
                return CurrentHealth;
            }
            set
            {
                CurrentHealth = value;
                if (CurrentHealth <= 0)
                {
                    Kill();                    
                }
            }
        }


        //Collision
        private void CheckCollisions()
        {
            for (int i = 0; i < LevelScene.PlayerBullets.Count; i++)
            {
                BulletController bullet = LevelScene.PlayerBullets[i];


                if (CollisionUtilities.IsBoxColliding(Position, Size, bullet.Position, bullet.Size))
                {
                    HealthController.GetDamage(bullet.Damage);

                    if (currentAnimation.Id != "ExplosionEnemy")
                    {
                        //currentAnimation = GetAnimationEnemy("ExplosionEnemy");
                        currentAnimation.Reset();
                        GameManager.Instance.ChangeScene(Scenes.VictoryScene);                       
                        Engine.Debug("Enemy is dead");
                    }
                }
            }
        }

        //Kill
        private void Kill()
        {
            CurrentHealth = 0;
            Engine.Debug("Enemy is dead");
            
        }
    }
}
