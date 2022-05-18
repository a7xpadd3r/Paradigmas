using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class GameObject
    {
        protected float scale;
        protected float angle;
        protected Animation currentAnimation;
        protected List<Animation> animations;

        public bool DontDestroyOnLoad { get; set; } 
        public bool IsActive { get; private set; }

        public float RealHeight => currentAnimation.CurrentFrame.Height * scale;
        public float RealWidth => currentAnimation.CurrentFrame.Width * scale;
        public Vector2 Position { get; set; }
        public Vector2 Size => new Vector2(RealWidth, RealHeight);
        public float EstimatedRadius => RealHeight > RealWidth ? RealHeight / 2 : RealWidth / 2;
        public float Radius { get; set; }


    
        public GameObject(Vector2 startPosition, float scale, float angle, List<Animation> animations)
        {
            this.animations = animations;
            Position = startPosition;
            this.scale = scale;
            this.angle = angle;
            GameObjectManager.AddGameObject(this);
            SetActive(true);

            Engine.Debug("Se llama al constructor del gameObject");
        }

        public GameObject() 
        {
            
        }

    public virtual void Update()
        {
            currentAnimation.Update();
        }

        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentFrame, Position.X, Position.Y, scale, scale, angle, RealWidth / 2, RealHeight / 2);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        protected Animation GetAnimationPlayer(string id)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].Id == id)
                {
                    return animations[i];
                }
            }
            Engine.Debug($"No se encontró la animación con el id: {id}");
            return null;
        }


        //GetAnimation
        protected Animation GetAnimationEnemy(string id)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].Id == id)
                {
                    return animations[i];
                }
            }
            Engine.Debug($"No se encontró la animación con el id: {id}");
            return null;
        }

        //GetAnimation
        protected Animation GetBulletAnimation(string id)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].Id == id)
                {
                    return animations[i];
                }
            }
            Engine.Debug($"No se encontró la animación con el id: {id}");
            return null;
        }
    }
}
