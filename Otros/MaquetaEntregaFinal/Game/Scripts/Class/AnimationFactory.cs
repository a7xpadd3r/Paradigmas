using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class AnimationFactory
    {
        //Propiedades
        public Animation IdleAnimationEnemy { get; private set; }
        public Animation IdleAnimationEnemyFast { get; private set; }
        public Animation IdleAnimationEnemyMini { get; private set; }
        public Animation BulletPlayer { get; private set; }
        public Animation BulletEnemy { get; private set; }
        public Animation bulletAnimation { get; private set; }
        public Animation IdleAnimationPlayer { get; private set; }
        public Animation IdleAnimationEnemyLow { get; private set; }
        public Animation ItemAnimation { get; private set; }
        public Animation IdleAnimationEnemyBoss { get; private set; }

        public Animation IdleAnimationEnemyTank { get; private set; }

        public void CreateAnimationsPlayer()
        {
            List<Texture> idlePlayerTextures = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/Player/Idle/{i}.png");
                idlePlayerTextures.Add(texture);
            }
            IdleAnimationPlayer = new Animation("idlePlayer", idlePlayerTextures, 0.05f);
        }
        public void CreateAnimationEnemy()
        {
            List<Texture> idleEnemyTexture = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/Enemy/Idle/{i}.png");
                idleEnemyTexture.Add(texture);
            }
            IdleAnimationEnemy = new Animation("idleEnemy", idleEnemyTexture, 0.05f);
        }

        public void CreateAnimationBullet(string texturePath)
        {
            List<Texture> bulletTexture = new List<Texture>();
            Texture texture = Engine.GetTexture(texturePath);
            bulletTexture.Add(texture);
            bulletAnimation = new Animation("bulletTexure", bulletTexture, 0.05f);
        }

        public void CreateAnimationsEnemyFast()
        {
            List<Texture> idleEnemyFastTexture = new List<Texture>();
            for (int i = 0; i < 5; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/EnemyFast/Idle/{i}.png");
                idleEnemyFastTexture.Add(texture);
            }
            IdleAnimationEnemyFast = new Animation("idleEnemyFast", idleEnemyFastTexture, 0.05f);
        }

        public void CreateAnimationsEnemyMini()
        {
            List<Texture> idleEnemyMiniTexture = new List<Texture>();
            for (int i = 1; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/EnemyMini/Idle/{i}.png");
                idleEnemyMiniTexture.Add(texture);
            }
            IdleAnimationEnemyMini = new Animation("idleEnemyMini", idleEnemyMiniTexture, 0.05f);
        }

        public void CreateAnimationsEnemyLow()
        {
            List<Texture> idleEnemyLowTexture = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/EnemyLow/Idle/{i}.png");
                idleEnemyLowTexture.Add(texture);
            }
            IdleAnimationEnemyLow = new Animation("idleEnemyLow", idleEnemyLowTexture, 0.05f);
        }

        public void CreateAnimationsEnemyBoss()
        {
            List<Texture> idleEnemyLowTexture = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/EnemyBoss/Idle/{i}.png");
                idleEnemyLowTexture.Add(texture);
            }
            IdleAnimationEnemyBoss = new Animation("idleEnemyBoss", idleEnemyLowTexture, 0.05f);
        }

        public void CreateAnimationsEnemyTank()
        {
            List<Texture> idleEnemyLowTexture = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                Texture texture = Engine.GetTexture($"Textures/Animations/EnemyTank/Idle/{i}.png");
                idleEnemyLowTexture.Add(texture);
            }
            IdleAnimationEnemyTank = new Animation("idleEnemyTank", idleEnemyLowTexture, 0.05f);
        }
    }
}
