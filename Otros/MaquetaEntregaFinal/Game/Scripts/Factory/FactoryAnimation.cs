using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class FactoryAnimation
    {
        public static List<Animation> CreateAnimation(string id)
        {
            switch (id)
            {
                case "PlayerAnimation":
                    return CreatePlayerAnimation();
                case "EnemyAnimation":
                    return CreateEnemyAnimation();
                case "EnemyFast":
                    return CreateEnemyFastAnimation();
                case "EnemyLow":
                    return CreateEnemyLowAnimation();
                case "EnemyMini":
                    return CreateEnemyMiniAnimation();
                case "EnemyBoss":
                    return CreateEnemyBossAnimation();
                case "EnemyTank":
                    return CreateEnemyTankAnimation();
                default:
                    return null;
            }
        }


        private static List<Animation> CreatePlayerAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/Player/Idle/{i}.png"));
            }

            Animation idleAnimations = new Animation("Idle", frames, 0.2f, false);
            animations.Add(idleAnimations);

            return animations;
        }

        private static List<Animation> CreateEnemyAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 1; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/Enemy/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateEnemyFastAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 5; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/EnemyFast/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateEnemyLowAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/EnemyLow/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateEnemyMiniAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 1; i < 3; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/EnemyMini/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateEnemyBossAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/EnemyBoss/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateEnemyTankAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 3; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/Animations/EnemyTank/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("Idle", frames, 0.2f, false);

            animations.Add(idleAnimation);
            return animations;
        }

        private static List<Animation> CreateBulletAnimation()
        {
            List<Animation> animations = new List<Animation>();

            List<Texture> frames = new List<Texture>();
            for (int i = 0; i < 1; i++)
            {
                frames.Add(Engine.GetTexture($"Textures/BlueBullet.png"));
            }

            Animation idleAnimations = new Animation("Idle", frames, 0.2f, false);
            animations.Add(idleAnimations);

            return animations;
        }
    }
}
