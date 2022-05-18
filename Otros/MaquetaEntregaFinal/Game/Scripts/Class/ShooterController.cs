using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ShooterController
    {
        //Campos
        private float currentShootCooldown;
        private float shootCooldown;
        private PoolBulletsDynamic poolBullets;


        //Propiedades
        public bool CanShoot => currentShootCooldown <= 0;


        //Constructor
        public ShooterController(float shootCooldown)
        {
            this.shootCooldown = shootCooldown;
        }


        //Metodos

        //Update
        public void Update()
        {
            currentShootCooldown -= Time.DeltaTime;
        }

        public void ResetCooldown()
        {
            currentShootCooldown = shootCooldown;
        }
    

        //Shoot
        public BulletController Shoot(Vector2 position, Vector2 direction, float angle)
        {
            ResetCooldown();
           
            var bullet = new BulletController(position, 2, angle, 10, 50, GetBulletAnimation());
            bullet.SetDirection(direction);
            return bullet;
        }

        //Bullet Animation
        private static List<Animation> GetBulletAnimation()
        {
            List<Animation> animationsBullet = new List<Animation>();

            List<Texture> bulletFrames = new List<Texture>();

            for (int i = 0; i < 1; i++)
            {
                bulletFrames.Add(Engine.GetTexture($"Textures/Animations/Bullet/{i}.png"));
            }
            Animation bulletAnimation = new Animation("Bullet", bulletFrames, 0.2f, true);
            animationsBullet.Add(bulletAnimation);

            return animationsBullet;
        }
    }
}
