using System;
using System.Media;
using System.Numerics;

namespace Game
{
    public class GenericBullet
    {
        public static SoundPlayer sfx = new SoundPlayer("resources/sfx/gulp.wav");
        Texture gfx = new Texture("resources/gfx/proyectiles/p_genericproyectile.png"); // Default texture
        float _speed = 800;
        Transform _transform => new Transform(new Vector2(_posX, _posY), new Vector2(2f, 2f), _rot);
        Transform _collidersize => new Transform(new Vector2(_posX, _posY), new Vector2(1, 1), _rot);
        float _posX = 0;
        float _posY = 0;
        float _rot = 0;
        int _type = 1;
        float lifeTime = 20f;
        float timer = 0;
        bool goesUp = true;
        GameObject gObject;
        Animation nullAnim;
        Collider bCollider;

        bool draw = true;

        public bool Draw => draw;

        public GenericBullet(float posX, float posY, float rot, int type, string owner, bool upwardsDirection = true)
        {
            _posX = posX;
            _posY = posY;
            _rot = rot;
            _type = type;

            switch (_type)
            {
                case 1:
                    _speed = 1500;
                    gfx = new Texture("resources/gfx/proyectiles/p_proyectile1.png");
                    break;
                case 2:
                    _speed = 1100;
                    gfx = new Texture("resources/gfx/proyectiles/p_proyectile2.png");
                    break;
                case 3:
                    _speed = 2000;
                    gfx = new Texture("resources/gfx/proyectiles/p_proyectile3.png");
                    break;
            }
            gObject = new GameObject("Bullet", owner, _transform, nullAnim, ColliderType.Box);
            //Console.WriteLine("Bullet of type {0} spawned at x-{1} y-{2}.", _type, _posX, _posY);
            bCollider = new Collider(gObject, _collidersize, ColliderType.Box);
            goesUp = upwardsDirection;
            CollisionManager.AddCollider(this.bCollider);

            bCollider.OnCollision += HitSomething;
        }

        void HitSomething(Collider colliderHit)
        {
            if (colliderHit.gObject.objTag != "Bullet")
            {
                bCollider.SetActive(false);
                gObject.SetActive(false);
                draw = false;
            }
        }
        public void Update()
        {
            if (draw)
            {
                if (goesUp)
                    _posY -= _speed * Program.GetDeltaTime();
                else
                    _posY += _speed * Program.GetDeltaTime();

                gObject.testPos = new Vector2(_posX, _posY);
                timer += Program.GetDeltaTime();

                if (bCollider != null)
                    bCollider.UpdatePos(_collidersize);

                if (timer >= lifeTime || _posY < -100)
                {
                    draw = false;
                    gObject.SetActive(false);
                    bCollider.SetActive(false);
                    GameObjectManager.RemoveGameObject(this.gObject);
                    CollisionManager.RemoveCollider(this.bCollider);
                }
            }
        }

        public void DrawBullet()
        {
            if (draw)
                Engine.Draw(gfx, _transform.position.X, _transform.position.Y, _transform.scale.X, _transform.scale.Y, _rot, 145.5f, 86.5f);
        }

    }
}
