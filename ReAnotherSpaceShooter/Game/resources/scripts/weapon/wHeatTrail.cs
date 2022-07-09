using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class wHeatTrail : iWeapon
    {
        // Weapon stats
        private int ammo = 500;
        private Animation FireHead = Textures.GetProyectileAnimation(Proyectile.HeatTrail);
        public WeaponTypes ThisType => WeaponTypes.HeatTrail;
        public int AddAmmoAmount => 650;

        // Heat Trail data
        private float toleranceRangeX = 5, toleranceRangeY = 5;
        private bool isDrawing = false;
        private float h1damage = 3, h2damage = 4.2f, h3damage = 5, h4damage = 6.7f, h5damage = 8;

        private Vector2 lastKnownPosition;
        private Vector2 firePositionH1 = new Vector2(-500, -500);
        private Vector2 firePositionH2 = new Vector2(-500, -500);
        private Vector2 firePositionH3 = new Vector2(-500, -500);
        private Vector2 firePositionH4 = new Vector2(-500, -500);
        private Vector2 firePositionH5 = new Vector2(-500, -500);
        private float h1X = 0, h1Y = 0, h1speed = 400;
        private float h2X = 0, h2Y = 0, h2speed = 360;
        private float h3X = 0, h3Y = 0, h3speed = 340;
        private float h4X = 0, h4Y = 0, h4speed = 320;
        private float h5X = 0, h5Y = 0, h5speed = 300;

        // Vectors for draw
        private Vector2 headsize => new Vector2(1.5f, 1.5f);
        private Vector2 head1 => new Vector2(h1X, h1Y);
        private Vector2 head2 => new Vector2(h2X, h2Y);
        private Vector2 head3 => new Vector2(h3X, h3Y);
        private Vector2 head4 => new Vector2(h4X, h4Y);
        private Vector2 head5 => new Vector2(h5X, h5Y);

        // Colliders
        private List<Collider> HeadsColliders = new List<Collider>();

        // Other
        float rotationH1 = 0;
        float rotationH2 = 0;
        float rotationH3 = 0;
        float rotationH4 = 0;
        float rotationH5 = 0;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }

        public iGetWeapon Owner { get; set; }

        public wHeatTrail()
        {
            HeadsColliders.Add(new Collider(h1damage, "Player", "Proyectile", lastKnownPosition, new Vector2(40, 20), 0));
            HeadsColliders.Add(new Collider(h2damage, "Player", "Proyectile", lastKnownPosition, new Vector2(50, 30), 0));
            HeadsColliders.Add(new Collider(h3damage, "Player", "Proyectile", lastKnownPosition, new Vector2(60, 38), 0));
            HeadsColliders.Add(new Collider(h4damage, "Player", "Proyectile", lastKnownPosition, new Vector2(75, 45), 0));
            HeadsColliders.Add(new Collider(h5damage, "Player", "Proyectile", lastKnownPosition, new Vector2(95, 60), 0));
        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void AddAmmo() { ammo += AddAmmoAmount; }

        public void Fire(string owner, Vector2 currentPosition)
        {
            if (ammo > 0)
            {
                isDrawing = true;
                firePositionH1 = new Vector2(lastKnownPosition.X - 8, lastKnownPosition.Y - 70);
                firePositionH2 = new Vector2(lastKnownPosition.X - 16, lastKnownPosition.Y - 120);
                firePositionH3 = new Vector2(lastKnownPosition.X - 26, lastKnownPosition.Y - 180);
                firePositionH4 = new Vector2(lastKnownPosition.X - 38, lastKnownPosition.Y - 250);
                firePositionH5 = new Vector2(lastKnownPosition.X - 50, lastKnownPosition.Y - 330);

                if (isDrawing)
                {
                    ammo--;
                    Renderer.DrawCenter(FireHead.CurrentTexture, head1, headsize * 1, rotationH1);
                    Renderer.DrawCenter(FireHead.CurrentTexture, head2, headsize * 1.35f, rotationH2);
                    Renderer.DrawCenter(FireHead.CurrentTexture, head3, headsize * 1.8f, rotationH3);
                    Renderer.DrawCenter(FireHead.CurrentTexture, head4, headsize * 2.25f, rotationH4);
                    Renderer.DrawCenter(FireHead.CurrentTexture, head5, headsize * 2.8f, rotationH5);
                }
            }
        }

        public void Update(float delta)
        {
            lastKnownPosition = GameManager.PlayerPosition;

            if (!isDrawing)
            {
                h1X = lastKnownPosition.X; h1Y = lastKnownPosition.Y - 50;
                h2X = lastKnownPosition.X - 10; h2Y = lastKnownPosition.Y - 80 ;
                h3X = lastKnownPosition.X - 30; h3Y = lastKnownPosition.Y - 90;
                h4X = lastKnownPosition.X - 35; h4Y = lastKnownPosition.Y - 90;
                h5X = lastKnownPosition.X - 40; h5Y = lastKnownPosition.Y - 120;
                rotationH1 = 0;
                rotationH2 = 0;
                rotationH3 = 0;
                rotationH4 = 0;
                rotationH5 = 0;
            }
            if (isDrawing)
            {
                //NeedRotation(); needs work..
                FireHead.Update();

                foreach (Collider collider in HeadsColliders)
                {
                    collider.CheckForCollisions();
                }

                HeadsColliders[0].UpdateOwnerPosition(head1 + new Vector2(8, 6));
                HeadsColliders[1].UpdateOwnerPosition(head2 + new Vector2(16, 20));
                HeadsColliders[2].UpdateOwnerPosition(head3 + new Vector2(26, 28));
                HeadsColliders[3].UpdateOwnerPosition(head4 + new Vector2(36, 38));
                HeadsColliders[4].UpdateOwnerPosition(head5 + new Vector2(50, 50));

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
        /*
        private void NeedRotation()
        {
            if (h1X > lastKnownPosition.X || h1X < lastKnownPosition.X)
            {
                if (h1X > lastKnownPosition.X && rotationH1 < 60) rotationH1 += Program.GetDeltaTime * 20;
                else if (h1X < lastKnownPosition.X && rotationH1 > -60) rotationH1 -= Program.GetDeltaTime * 20;
            }

            if (h5X > lastKnownPosition.X || h5X < lastKnownPosition.X)
            {
                if (h5X > lastKnownPosition.X) rotationH5 += Program.GetDeltaTime * 20;
                else if (h5X < lastKnownPosition.X) rotationH5 -= Program.GetDeltaTime * 20;
            }

            Console.WriteLine(rotationH5);
            if (!(h5X > lastKnownPosition.X) && !(h5X < lastKnownPosition.X))
            {
                if (rotationH5 > 0) rotationH5 += Program.GetDeltaTime * 20;
                else if (rotationH5 < 0) rotationH5 -= Program.GetDeltaTime * 20;
            }
        }*/
    }
}