using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    class wHeatTrail : iWeapon
    {
        // Basic stuff
        public iGetWeapon Owner { get; private set; }
        WeaponTypes iWeapon.Type => WeaponTypes.HeatTrail;
        public Transform bulletTransform => new Transform();
        private Animation FireHead = new Animation("FireHead", 0.016f, Effects.GetEffectTextures(7));
        private float toleranceRangeX = 1f, toleranceRangeY = 1f; // A firehead will stop moving when enters this ranges.
        private bool isDrawing = false; // Used to check when the fireheads needs to follow player movement or needs to be reseted.

        // Ammo stuff
        private int ammo = 500;
        private readonly int ammoAdd = 500;
        public int CurrentAmmo => ammo;

        // Damages
        private float h1damage = 3f, h2damage = 4.2f, h3damage = 5f, h4damage = 6.7f, h5damage = 8f, h6damage = 10f;

        // Positions
        private Vector2 lastKnownPosition;
        private Vector2 firePositionH1 = new Vector2(-500, -500);
        private Vector2 firePositionH2 = new Vector2(-500, -500);
        private Vector2 firePositionH3 = new Vector2(-500, -500);
        private Vector2 firePositionH4 = new Vector2(-500, -500);
        private Vector2 firePositionH5 = new Vector2(-500, -500);
        private Vector2 firePositionH6 = new Vector2(-500, -500);
        private float h1X = 0, h1Y = 0, h1speed = 400f;
        private float h2X = 0, h2Y = 0, h2speed = 380f;
        private float h3X = 0, h3Y = 0, h3speed = 360f;
        private float h4X = 0, h4Y = 0, h4speed = 340f;
        private float h5X = 0, h5Y = 0, h5speed = 320f;
        private float h6X = 0, h6Y = 0, h6speed = 300f;

        // Vectors for draw

        private Transform tHead1 => new Transform(new Vector2(h1X + 10, h1Y + 20), new Vector2(1.5f, 1.5f));
        private Transform tHead2 => new Transform(new Vector2(h2X + 17, h2Y + 20), new Vector2(1.8f, 1.8f));
        private Transform tHead3 => new Transform(new Vector2(h3X + 20, h3Y + 20), new Vector2(2.1f, 2.1f));
        private Transform tHead4 => new Transform(new Vector2(h4X + 25, h4Y + 20), new Vector2(2.4f, 2.4f));
        private Transform tHead5 => new Transform(new Vector2(h5X + 29, h5Y + 20), new Vector2(2.7f, 2.7f));
        private Transform tHead6 => new Transform(new Vector2(h6X + 33, h6Y + 20), new Vector2(3f, 3f));

        // Colliders
        private List<Collider> HeadsColliders = new List<Collider>();
        private Vector2 colliderOffset = new Vector2(170, 90);
        private Vector2 imagesize => new Vector2(FireHead.CurrentTexture.Width, FireHead.CurrentTexture.Height);

        public wHeatTrail()
        {
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead1.Position, tHead1.Scale * 25), "Player", "Proyectile", 0, h1damage));
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead2.Position, tHead2.Scale * 25), "Player", "Proyectile", 0, h2damage));
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead3.Position, tHead3.Scale * 25), "Player", "Proyectile", 0, h3damage));
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead4.Position, tHead4.Scale * 25), "Player", "Proyectile", 0, h4damage));
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead5.Position, tHead5.Scale * 25), "Player", "Proyectile", 0, h5damage));
            HeadsColliders.Add(new Collider(new ColliderProperties(tHead6.Position, tHead6.Scale * 25), "Player", "Proyectile", 0, h6damage));
        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void AddAmmo()
        {
            ammo += ammoAdd;
        }

        public void Fire()
        {
            if (ammo > 0)
            {
                isDrawing = true;
                firePositionH1 = new Vector2(lastKnownPosition.X - 8, lastKnownPosition.Y - 23);
                firePositionH2 = new Vector2(lastKnownPosition.X - 13, lastKnownPosition.Y - 65);
                firePositionH3 = new Vector2(lastKnownPosition.X - 17, lastKnownPosition.Y - 110);
                firePositionH4 = new Vector2(lastKnownPosition.X - 22, lastKnownPosition.Y - 160);
                firePositionH5 = new Vector2(lastKnownPosition.X - 26, lastKnownPosition.Y - 220);
                firePositionH6 = new Vector2(lastKnownPosition.X - 31, lastKnownPosition.Y - 280);

                if (isDrawing)
                {
                    ammo -= 1;
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead1);
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead2);
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead3);
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead4);
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead5);
                    Engine.DrawTransform(FireHead.CurrentTexture, tHead6);
                }
            }
        }

        public void Update(float delta, Vector2 currentPosition)
        {
            lastKnownPosition = currentPosition;

            if (!isDrawing)
            {
                h1X = lastKnownPosition.X - 8; h1Y = lastKnownPosition.Y;
                h2X = lastKnownPosition.X - 13; h2Y = lastKnownPosition.Y - 10;
                h3X = lastKnownPosition.X - 17; h3Y = lastKnownPosition.Y - 20;
                h4X = lastKnownPosition.X - 22; h4Y = lastKnownPosition.Y - 30;
                h5X = lastKnownPosition.X - 26; h5Y = lastKnownPosition.Y - 60;
                h6X = lastKnownPosition.X - 31; h6Y = lastKnownPosition.Y - 80;
            }

            if (isDrawing)
            {
                FireHead.Update();
                HeadsColliders[0].UpdateColliderPosition(tHead1.Position - new Vector2(18, 10));
                HeadsColliders[1].UpdateColliderPosition(tHead2.Position - new Vector2(22, 11));
                HeadsColliders[2].UpdateColliderPosition(tHead3.Position - new Vector2(25, 12));
                HeadsColliders[3].UpdateColliderPosition(tHead4.Position - new Vector2(30, 13));
                HeadsColliders[4].UpdateColliderPosition(tHead5.Position - new Vector2(36, 14));
                HeadsColliders[5].UpdateColliderPosition(tHead6.Position - new Vector2(39, 15));

                foreach (Collider collider in HeadsColliders)
                {
                    collider.CheckCollision();
                }


                for (int i = 1; i < 7; i++)
                {
                    var tempX = h1X;
                    var tempY = h1Y;
                    var tempVector = firePositionH1;
                    var tempSpeed = h1speed;

                    switch (i)
                    {
                        case 2:
                            tempX = h2X;
                            tempY = h2Y;
                            tempVector = firePositionH2;
                            tempSpeed = h2speed;
                            break;
                        case 3:
                            tempX = h3X;
                            tempY = h3Y;
                            tempVector = firePositionH3;
                            tempSpeed = h3speed;
                            break;
                        case 4:
                            tempX = h4X;
                            tempY = h4Y;
                            tempVector = firePositionH4;
                            tempSpeed = h4speed;
                            break;
                        case 5:
                            tempX = h5X;
                            tempY = h5Y;
                            tempVector = firePositionH5;
                            tempSpeed = h5speed;
                            break;
                        case 6:
                            tempX = h6X;
                            tempY = h6Y;
                            tempVector = firePositionH6;
                            tempSpeed = h6speed;
                            break;
                    }

                    if (!InRange(tempX, tempVector.X - toleranceRangeX, tempVector.X + toleranceRangeX) || !InRange(tempY, tempVector.Y - toleranceRangeY, tempVector.Y + toleranceRangeY))
                    {
                        if (tempX > tempVector.X) tempX -= tempSpeed * delta;
                        else if (tempX < tempVector.X) tempX += tempSpeed * delta;

                        if (tempY > tempVector.Y) tempY -= tempSpeed * delta;
                        else if (tempY < tempVector.Y) tempY += tempSpeed * delta;
                    }

                    switch (i)
                    {
                        case 1:
                            h1X = tempX;
                            h1Y = tempY;
                            break;
                        case 2:
                            h2X = tempX;
                            h2Y = tempY;
                            break;
                        case 3:
                            h3X = tempX;
                            h3Y = tempY;
                            break;
                        case 4:
                            h4X = tempX;
                            h4Y = tempY;
                            break;
                        case 5:
                            h5X = tempX;
                            h5Y = tempY;
                            break;
                        case 6:
                            h6X = tempX;
                            h6Y = tempY;
                            break;
                    }
                }
            }

            isDrawing = false;
        }

        private bool InRange(float input1, float min1, float max1)
        {
            bool value = false;
            if (input1 > min1 && input1 < max1) value = true;
            return value;
        }
    }
}
