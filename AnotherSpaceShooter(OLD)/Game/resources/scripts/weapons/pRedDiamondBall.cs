﻿using System;
using System.Numerics;

namespace Game
{
    class pRedDiamondBall : GameObject, iProyectile
    {
        // iProyectile basics
        public Transform transform { get; set; }
        public Animation animation { get; set; }
        public float speed { get; set; }
        public float damage { get; set; }

        // How many times this proyectile can spawn
        public int currentIterations { get; private set; }
        private int maxIterations = 2;
        private string direction = "up";
        int lastHit = -1;    // When a enemy is hit, this value will prevent any further collision for the iterations.

        public pRedDiamondBall(Transform newTransform, string newDirection = "up", int newLastHit = -1, int iterationsCount = 0)
        {
            this.transform = newTransform;
            this.transform.UpdateTransform(newTransform); // Just in case.
            this.animation = ProyectilesTextures.GetProyectileAnim(2);
            this.lastHit = newLastHit;
            this.direction = newDirection;
            this.currentIterations = iterationsCount;

            switch (this.currentIterations)
            {
                default:
                    this.damage = 2.9f;
                    this.speed = 640;
                    break;
                case 1:
                    this.damage = 2.9f;
                    this.speed = 640;
                    this.transform.UpdateScale(new Vector2(0.92f, 0.92f));
                    break;
                case 2:
                    this.damage = 1.8f;
                    this.speed = 520;
                    this.transform.UpdateScale(new Vector2(0.8f, 0.8f));
                    break;
            }

            this.objectCollider = new Collider(transform.Position, transform.Scale, "Player", "Proyectile", damage);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            if (instigator.tag != "Item" && lastHit != instigator.id)
            {
                lastHit = instigator.id;
                new GenericEffect(transform.Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
                Destroy();
            }
        }

        public override void Update()
        {
            if (isActive)
            {
                float delta = Program.GetDeltaTime();
                float posX = transform.Position.X;
                float posY = transform.Position.Y;

                switch (direction)
                {
                    case "up":
                        posY -= speed * delta;
                        break;
                    case "down":
                        posY += speed * delta;
                        break;
                    case "left":
                        posX -= speed * delta;
                        break;
                    case "right":
                        posX += speed * delta;
                        break;
                }

                transform.UpdatePosition(new Vector2(posX, posY)); // Set new position
                objectCollider.UpdatePos(new Vector2(posX + 149, transform.Position.Y + 90));
                animation.Update();
                Render();

                if (transform.Position.Y < 0 || transform.Position.Y > 1080 || transform.Position.X < 0 || transform.Position.X > 1920)
                {
                    currentIterations = maxIterations;
                    Destroy();
                }
            }
        }
        public override void Render()
        {
            Engine.DrawTransform(animation.CurrentTexture, transform, new Vector2(animation.CurrentTexture.Width /2, animation.CurrentTexture.Height /2));
        }

        public override void Destroy()
        {
            if (currentIterations < maxIterations)
            {
                currentIterations++;
                if (direction == "up" || direction == "down")
                {
                    new pRedDiamondBall(new Transform(new Vector2(transform.Position.X + 10, transform.Position.Y), transform.Scale), "left", lastHit,currentIterations);
                    new pRedDiamondBall(new Transform(new Vector2(transform.Position.X - 10, transform.Position.Y), transform.Scale), "right", lastHit, currentIterations);
                }

                else if (direction == "left" || direction == "right")
                {
                    new pRedDiamondBall(new Transform(new Vector2(transform.Position.X, transform.Position.Y + 10), transform.Scale), "up", lastHit, currentIterations);
                    new pRedDiamondBall(new Transform(new Vector2(transform.Position.X, transform.Position.Y - 10), transform.Scale), "down", lastHit, currentIterations);
                }
            }

            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            GameObjectManager.RemoveGameObject(this);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
