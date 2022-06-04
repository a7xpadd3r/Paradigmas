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
        private Vector2 head1 => new Vector2(h1X, h1Y);
        private Vector2 head1size => new Vector2(1.5f, 1.5f);
        private Vector2 head2 => new Vector2(h2X, h2Y);
        private Vector2 head2size => new Vector2(1.8f, 1.8f);
        private Vector2 head3 => new Vector2(h3X, h3Y);
        private Vector2 head3size => new Vector2(2.1f, 2.1f);
        private Vector2 head4 => new Vector2(h4X, h4Y);
        private Vector2 head4size => new Vector2(2.4f, 2.4f);
        private Vector2 head5 => new Vector2(h5X, h5Y);
        private Vector2 head5size => new Vector2(2.7f, 2.7f);
        private Vector2 head6 => new Vector2(h6X, h6Y);
        private Vector2 head6size => new Vector2(3f, 3f);

        // Colliders
        private List<Collider> HeadsColliders = new List<Collider>();
        private Vector2 colliderOffset = new Vector2(170, 90);

        public wHeatTrail()
        {
            HeadsColliders.Add(new Collider(head1, head1size * 1.2f, "Player", "Proyectile", h1damage));
            HeadsColliders.Add(new Collider(head2, head2size * 1.2f, "Player", "Proyectile", h2damage));
            HeadsColliders.Add(new Collider(head3, head3size * 1.2f, "Player", "Proyectile", h3damage));
            HeadsColliders.Add(new Collider(head4, head4size * 1.2f, "Player", "Proyectile", h4damage));
            HeadsColliders.Add(new Collider(head5, head5size * 1.2f, "Player", "Proyectile", h5damage));
            HeadsColliders.Add(new Collider(head6, head6size * 1.2f, "Player", "Proyectile", h6damage));
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
                    Engine.Draw(FireHead.CurrentTexture, head1.X, head1.Y, head1size.X, head1size.Y);
                    Engine.Draw(FireHead.CurrentTexture, head2.X, head2.Y, head2size.X, head2size.Y);
                    Engine.Draw(FireHead.CurrentTexture, head3.X, head3.Y, head3size.X, head3size.Y);
                    Engine.Draw(FireHead.CurrentTexture, head4.X, head4.Y, head4size.X, head4size.Y);
                    Engine.Draw(FireHead.CurrentTexture, head5.X, head5.Y, head5size.X, head5size.Y);
                    Engine.Draw(FireHead.CurrentTexture, head6.X, head6.Y, head6size.X, head6size.Y);
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
                HeadsColliders[0].UpdatePos(head1 + colliderOffset);
                HeadsColliders[1].UpdatePos(head2 + colliderOffset);
                HeadsColliders[2].UpdatePos(head3 + colliderOffset);
                HeadsColliders[3].UpdatePos(head4 + colliderOffset);
                HeadsColliders[4].UpdatePos(head5 + colliderOffset);
                HeadsColliders[5].UpdatePos(head6 + colliderOffset);

                foreach (Collider collider in HeadsColliders)
                {
                    collider.CheckForCollisions();
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
