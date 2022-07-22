using System;
using System.Numerics;

namespace Game
{
    public class mStageSpawner
    {
        // Status
        private int maxY = 150;
        private static float currentY = 0;
        private static int currentIndex = 0;
        private int OnScreenLeft => (int)(maxY - currentY);

        // Event
        private Action UpdateCurrentIndex;
        public Action Win;

        // Timers
        private bool usetimer = false;
        private float currentCD = 0;
        private float maxCD = 0;

        // Show long left
        private NumbersToSprites left = new NumbersToSprites();

        // Others
        private Random random = new Random();

        public mStageSpawner() 
        { 
            currentY = 0; currentIndex = 0;
            UpdateCurrentIndex -= IndexNext;
            UpdateCurrentIndex += IndexNext;
        }
        public void Update(float delta)
        {
            left.RenderNumbers(OnScreenLeft, new Vector2(900, 1050), new Vector2(1.5f, 1.5f));
            if (usetimer && currentCD < maxCD) currentCD += delta;
            else if (usetimer && currentCD > maxCD) { currentCD = 0; maxCD = 0; UpdateCurrentIndex?.Invoke(); }

            currentY += delta;
            AllMosquitoes();
            AllSliders();
            AllTremors();

            // Waits
            if (currentIndex == 24) { Countdown(12); UpdateCurrentIndex?.Invoke(); }
            if (currentIndex == 24 || currentIndex == 25) currentY = 10;

            if (currentIndex == 30) { Countdown(15); UpdateCurrentIndex?.Invoke(); }
            if (currentIndex == 30 || currentIndex == 31) currentY = 20;

            if (currentIndex == 36) { Countdown(15); UpdateCurrentIndex?.Invoke(); }
            if (currentIndex == 36 || currentIndex == 37) currentY = 41;

            if (currentY >= maxY) Win?.Invoke();

            if (Engine.GetKey(Keys.BACK)) Win?.Invoke();
        }
        private void AllMosquitoes()
        {
            // Wave 1
            if (CanSpawn(2, 0)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(2.25f, 1)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(2.5f, 2)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(2.75f, 3)) SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY);
            if (CanSpawn(3, 4)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(3.25f, 5)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);

            if (CanSpawn(5, 6)) SpawnMosquitoe(new Vector2(-50, 250));
            if (CanSpawn(5.25f, 7)) SpawnMosquitoe(new Vector2(1920, 250), false, MovementType.WaveY);
            if (CanSpawn(5.5f, 8)) SpawnMosquitoe(new Vector2(-50, 250));
            if (CanSpawn(5.75f, 9)) SpawnMosquitoe(new Vector2(1920, 280), false, MovementType.WaveY);
            if (CanSpawn(6, 10)) SpawnMosquitoe(new Vector2(-50, 280));
            if (CanSpawn(6.25f, 11)) SpawnMosquitoe(new Vector2(1920, 280), false, MovementType.WaveY);

            if (CanSpawn(7, 12)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(7.25f, 13)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(7.5f, 14)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(7.75f, 15)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(8, 16)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(8.25f, 17)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);

            if (CanSpawn(8, 18)) SpawnMosquitoe(new Vector2(-50, 350), false, MovementType.WaveY);
            if (CanSpawn(8.25f, 19)) SpawnMosquitoe(new Vector2(1920, 500));
            if (CanSpawn(8.5f, 20)) { SpawnMosquitoe(new Vector2(-50, 350), true, MovementType.WaveY); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Shield); }
            if (CanSpawn(8.75f, 21)) SpawnMosquitoe(new Vector2(1920, 500));
            if (CanSpawn(9, 22)) SpawnMosquitoe(new Vector2(-50, 350), false, MovementType.WaveY);
            if (CanSpawn(9.25f, 23)) SpawnMosquitoe(new Vector2(1920, 500));

            // Wave 2
            if (CanSpawn(45, 38)) SpawnMosquitoe(new Vector2(-50, 100), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(45.25f, 39)) SpawnMosquitoe(new Vector2(1920, 200), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(45.5f, 40)) { SpawnMosquitoe(new Vector2(-50, 300), true, MovementType.Lineal, AxisY.Up); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Repair); }
            if (CanSpawn(45.75f, 41)) SpawnMosquitoe(new Vector2(1920, 400), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(45, 42)) SpawnMosquitoe(new Vector2(-50, 500), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(45.25f, 43)) SpawnMosquitoe(new Vector2(1920, 600), true, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(45.5f, 44)) SpawnMosquitoe(new Vector2(1920, 600), false, MovementType.Lineal, AxisY.Up);

            // Wave 3
            if (CanSpawn(50, 45)) SpawnMosquitoe(new Vector2(-50, 650), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(50.5f, 47)) SpawnMosquitoe(new Vector2(1920, 250), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(51f, 48)) SpawnMosquitoe(new Vector2(-50, 850), true, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(51.5f, 49)) SpawnMosquitoe(new Vector2(1920, 150), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(52, 50)) SpawnMosquitoe(new Vector2(-50, 950), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(52.5f, 51)) SpawnMosquitoe(new Vector2(1920, 180), true, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(53f, 52)) SpawnMosquitoe(new Vector2(-50, 650), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(53.5f, 53)) { SpawnMosquitoe(new Vector2(1920, 590), false, MovementType.Lineal, AxisY.Up); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Weapon, WeaponTypes.OrbWeaver); }

            // Wave 4
            if (CanSpawn(62, 58)) SpawnMosquitoe(new Vector2(-50, 100), false, MovementType.WaveY);
            if (CanSpawn(63, 59)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(64, 60)) { SpawnMosquitoe(new Vector2(-50, 100), false, MovementType.WaveY); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Repair); }
            if (CanSpawn(65, 61)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(66, 62)) SpawnMosquitoe(new Vector2(-50, 100), true, MovementType.WaveY);
            if (CanSpawn(67.25f, 63)) SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY);

            // Wave 5
            if (CanSpawn(110, 70)) { SpawnMosquitoe(new Vector2(-50, 100)); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Repair); }
            if (CanSpawn(110.25f, 71)) SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY);
            if (CanSpawn(110.5f, 72)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(110.75f, 73)) SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY);
            if (CanSpawn(111, 74)) SpawnMosquitoe(new Vector2(-50, 100));
            if (CanSpawn(113, 75)) SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY);
            if (CanSpawn(115, 76)) SpawnMosquitoe(new Vector2(-50, 650), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(116, 77)) SpawnMosquitoe(new Vector2(1920, 250), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(117, 78)) SpawnMosquitoe(new Vector2(-50, 850), true, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(118, 79)) SpawnMosquitoe(new Vector2(1920, 150), false, MovementType.Lineal, AxisY.Down);
            if (CanSpawn(119, 80)) SpawnMosquitoe(new Vector2(-50, 950), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(120, 81)) SpawnMosquitoe(new Vector2(1920, 180), true, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(121, 82)) SpawnMosquitoe(new Vector2(-50, 650), false, MovementType.Lineal, AxisY.Up);
            if (CanSpawn(122, 83)) SpawnMosquitoe(new Vector2(1920, 590), false, MovementType.Lineal, AxisY.Up);

            // Wave 6
            if (CanSpawn(110, 92)) SpawnMosquitoe(new Vector2(-50, 600));
            if (CanSpawn(111, 93)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
            if (CanSpawn(112, 94)) SpawnMosquitoe(new Vector2(-50, 600));
            if (CanSpawn(113, 95)) { SpawnMosquitoe(new Vector2(1920, 130), true, MovementType.WaveY); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Special); }
            if (CanSpawn(114, 96)) SpawnMosquitoe(new Vector2(-50, 600));
            if (CanSpawn(115, 97)) SpawnMosquitoe(new Vector2(1920, 130), false, MovementType.WaveY);
        }
        private void AllSliders()
        {
            // Wave 1
            if (CanSpawn(11, 26)) SpawnSlider(new Vector2(-50, 200), true);
            if (CanSpawn(13, 27)) SpawnSlider(new Vector2(-50, 400));
            if (CanSpawn(15, 28)) SpawnSlider(new Vector2(-50, 600));
            if (CanSpawn(18, 29)) SpawnSlider(new Vector2(-50, 800), true);

            // Wave 2
            if (CanSpawn(25, 32)) SpawnSlider(new Vector2(-50, 700), true);
            if (CanSpawn(30, 33)) SpawnSlider(new Vector2(-50, 800));
            if (CanSpawn(35, 34)) SpawnSlider(new Vector2(-50, 900));
            if (CanSpawn(40, 35)) { SpawnSlider(new Vector2(-50, 1000), true); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Weapon, WeaponTypes.Gamma); }

            // Wave 3
            if (CanSpawn(50.15f, 46)) SpawnSlider(new Vector2(-50, 700), true);
            if (CanSpawn(54, 54)) SpawnSlider(new Vector2(-50, 800), true);

            // Wave 4
            if (CanSpawn(60.5f, 56)) SpawnSlider(new Vector2(-50, 200), true);
            if (CanSpawn(61.5f, 57)) SpawnSlider(new Vector2(-50, 800), true);

            // Wave 5
            if (CanSpawn(90, 68)) SpawnSlider(new Vector2(-50, 200), false);
            if (CanSpawn(100, 69)) SpawnSlider(new Vector2(-50, 800), false);

            // Wave 6
            if (CanSpawn(90, 84)) SpawnSlider(new Vector2(-50, 200), false);
            if (CanSpawn(100, 85)) SpawnSlider(new Vector2(-50, 800), false);
            if (CanSpawn(90, 86)) SpawnSlider(new Vector2(-50, 200), false);
            if (CanSpawn(100, 87)) { SpawnSlider(new Vector2(-50, 800), false); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Special); }
        }
        private void AllTremors()
        {
            // Wave 1
            if (CanSpawn(60, 55)) SpawnTremor(new Vector2(80, 0), true);
            if (CanSpawn(61, 56)) SpawnTremor(new Vector2(1900, 0), true);

            // Wave 2
            if (CanSpawn(65, 64)) { SpawnTremor(new Vector2(100, 0), false); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Weapon, WeaponTypes.RedDiamond); }
            if (CanSpawn(70, 65)) SpawnTremor(new Vector2(200, 0), false);
            if (CanSpawn(75, 66)) SpawnTremor(new Vector2(300, 0), false);
            if (CanSpawn(80, 67)) SpawnTremor(new Vector2(400, 0), false);

            // Wave 3
            if (CanSpawn(101, 88)) SpawnTremor(new Vector2(-50, 100), true);
            if (CanSpawn(102, 89)) SpawnTremor(new Vector2(-50, 400), false);
            if (CanSpawn(103, 90)) { SpawnTremor(new Vector2(-50, 600), false); GameManager.SpawnItem(new Vector2(random.Next(100, 1800), 0), ItemType.Weapon, WeaponTypes.Gamma); }
            if (CanSpawn(104, 91)) SpawnTremor(new Vector2(-50, 900), true);
        }

        private void SpawnMosquitoe(Vector2 spawnPos, bool special = false, MovementType movtype = MovementType.Lineal, AxisY yMovement = AxisY.None)
        {
            int newid = mGameObject.GenerateObjectID();
            var newmosquitoe = fyPoolDay.Pool.CreateMosquitoe(newid);
            newmosquitoe.Awake(spawnPos, special, movtype, yMovement);
            currentIndex++;
        }
        private void SpawnSlider(Vector2 spawnPos, bool special = false)
        {
            int newid = mGameObject.GenerateObjectID();
            var newslider = fyPoolDay.Pool.CreateSlider(newid);
            newslider.Awake(spawnPos, special);
            currentIndex++;
        }
        private void SpawnTremor(Vector2 spawnPos, bool special = false)
        {
            int newid = mGameObject.GenerateObjectID();
            var newtremor = fyPoolDay.Pool.CreateTremor(newid);
            newtremor.Awake(spawnPos, special);
            currentIndex++;
        }
        private bool CanSpawn(float neededY, int neededIndex)
        { return neededY < currentY && currentIndex == neededIndex; }
        private void IndexNext() { currentIndex++; }
        private void Countdown(float maxtime)
        {
            this.maxCD = maxtime;
            this.currentCD = 0;
            this.usetimer = true;
        }
    }
}
