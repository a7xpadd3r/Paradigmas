using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    class DummyEnemy
    {
        private static Animation ShipPropellers;
        private static bool animCreated = false;
        private static Random rnd = new Random();
        private static float posXmodifier = 0;
        private static float xModifier = rnd.Next(2, 5);
        public static bool draw = true;
        static GameObject gObject;
        static Collider gCollider;

        public static float posX = 100;
        public static float posY = 200;
        public static float scaleX = 200f;
        public static float scaleY = 20f;
        public static float angle = 0;

        public static float speed = 400;

        static Vector2 size = new Vector2(scaleX, scaleY);
        static Vector2 colliderPosition = new Vector2(posX + 48, posY);
        static Vector2 colliderSize = new Vector2(scaleX - 75f, scaleY + 70);

        private static int lifesLeft = 10;

        static Transform transform => new Transform(UpdatedPos().position, size, angle);
        static Transform colliderTransform => new Transform(colliderPosition, colliderSize, angle);
        public static Transform railPosition => new Transform(new Vector2(UpdatedPos().position.X + 50, UpdatedPos().position.Y + 25), transform.scale, angle);


        public static Animation smokeFX = new Animation("smoke1", 0.5f, GFX.GetTextures("switch1"), true, true);
        private static List<Texture> t_smoke1 = new List<Texture>();

        private static void InitializeDummy()
        {
            xModifier = rnd.Next(2, 5);
            var PropellersTextures = PlayerTextures.PropellerSet();
            ShipPropellers = new Animation("shippropellers", 0.03f, PropellersTextures);

            gObject = new GameObject("Dummy Enemy", "Enemy", new Transform(new Vector2(posX, posY), new Vector2(10, 10), 0), ShipPropellers, ColliderType.Box);
            gCollider = new Collider(gObject, colliderTransform, ColliderType.Box);
            CollisionManager.AddCollider(gCollider);

            // Smoke animation
            for (int i = 0; i < 5; i++)
            {
                t_smoke1.Add(new Texture("resources/gfx/effect/smoke1-" + i + ".png"));
                Console.WriteLine("resources/gfx/effect/smoke1-" + i + ".png");
            }

            t_smoke1.Add(new Texture("resources/gfx/blank.png"));
            smokeFX = new Animation("smoke1", 0.1f, t_smoke1, false);
            smokeFX.Play();

            // Subscribe to collision
            gCollider.OnCollision += HitSomething;
        }

        static void HitSomething(Collider colliderHit)
        {
            if (colliderHit.gObject.GetOwner() == "Player")
            {
                Console.WriteLine(colliderHit.gObject.objTag);
                lifesLeft--;

                if (lifesLeft <= 0)
                {
                    CollisionManager.RemoveCollider(gCollider);
                    gCollider.OnCollision -= HitSomething;
                    draw = false;
                }
                smokeFX.Play();
            }
        }

        public static void Draw()
        {
            smokeFX.Update();
            Weapon.Update();
            Movement.UpdatePosition();

            if (draw)
            {
                if (!animCreated)
                {
                    InitializeDummy();
                    animCreated = true;
                }

                if (animCreated)
                {
                    posXmodifier = posX - PlayerConfig.GetPos() / xModifier;
                    Engine.Draw(ShipPropellers.CurrentTexture, transform.position.X - 48.1f, transform.position.Y - 109, 1, 1, 180f);
                    Engine.Draw("resources/gfx/ships/test/NoSeNoTieneNombre.png", UpdatedPos().position.X, transform.position.Y, 1, 1, 180f);

                    Engine.Draw(smokeFX.CurrentTexture, transform.position.X - 130, transform.position.Y - 160, 1, 1);
                    ShipPropellers.Update();
                }

                if (gCollider != null)
                    gCollider.UpdatePos(new Transform(new Vector2(UpdatedPos().position.X + 55, UpdatedPos().position.Y), colliderSize, angle));
            }
        }

        private static Transform UpdatedPos()
        {
            Vector2 updatedVector = new Vector2(posX, posY);
            Transform transform = new Transform(updatedVector, size, angle);
            return transform;
        }
    }

    class Weapon
    {
        static bool canShoot = true;
        static List<GenericBullet> bullets = new List<GenericBullet>();
        static float recoilTime = 0.2f;
        static float currentTime = 0;

        public static void Update()
        {
            RecoilManagment();

            for (int i = 0; i < bullets.Count; i++) bullets[i].Update();
            for (int i = 0; i < bullets.Count; i++) bullets[i].DrawBullet();

            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].Draw)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        private static void RecoilManagment()
        {
            if (DummyEnemy.draw)
            {
                if (canShoot) Shoot();

                if (!canShoot) currentTime += Program.GetDeltaTime();

                if (currentTime >= recoilTime && !canShoot)
                {
                    currentTime = 0;
                    canShoot = true;
                }
            }
        }

        private static void Shoot()
        {
            if (canShoot & DummyEnemy.draw)
            {
                canShoot = false;
                bullets.Add(new GenericBullet(DummyEnemy.railPosition.position.X, DummyEnemy.railPosition.position.Y, 0, 1, "Enemy", false));
            }
        }
    }

    class Movement
    {
        private static bool movingRight = true;
        public static void UpdatePosition()
        {
            if (DummyEnemy.posX > 1900)
                movingRight = true;

            else if (DummyEnemy.posX < 20)
                movingRight = false;

            if (!movingRight)
                DummyEnemy.posX += Program.GetDeltaTime() * DummyEnemy.speed;

            else if (movingRight)
                DummyEnemy.posX -= Program.GetDeltaTime() * DummyEnemy.speed;
        }
    }
}