using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class GameObject
    {
        //Campos
        protected Animation currentAnimation;
        protected Renderer renderer;
        protected Transform transform;
        public AnimationFactory animationFactory;
        public event Action<GameObject> OnActivate;
        public event Action<GameObject> OnDeactivate;


        //Propiedades
        public string DisplayName { get; set; }
        public bool IsActive { get; private set; }
        public bool IsDontDestroyOnLoadEnabled { get; set; }
        public string Tag { get; set; }
        public bool IsTrigger { get; set; }
        public float RealHeight => this.currentAnimation.CurrentFrame.Height * transform.Scale.Y;
        public float RealWidth => this.currentAnimation.CurrentFrame.Width * transform.Scale.X;
        public Vector2 Position { get => transform.Position; set => transform.Position = value; }
        public float Radius => RealHeight > RealWidth ? RealHeight / 2 : RealWidth / 2;
        public Vector2 Size => new Vector2(RealWidth, RealHeight);

        //Constructor
        public GameObject()
        {
        }
        public GameObject(string name, Vector2 starPosition, Vector2 scale, float angle = 0f)
        {
            IsTrigger = false;
            DisplayName = name;
            transform = new Transform(starPosition, scale, angle);
            renderer = new Renderer();
            animationFactory = new AnimationFactory();
            GameObjectManager.AddGameObject(this);
            SetActive(true);

        }

        //Metodos
        public void SetActive(bool isActive)
        {
            if (isActive == IsActive) return;
            
            IsActive = isActive;
            
            if (IsActive)
            {
                OnActivate?.Invoke(this);
            }
            else
            {
                OnDeactivate?.Invoke(this);
            }
        }
        public virtual void Update()
        {
            currentAnimation.Update();
        }
        public virtual void Render()
        {
            renderer.Render(currentAnimation.CurrentFrame, transform, Radius);
        }
        protected abstract void CreateAnimations();
    }
}

