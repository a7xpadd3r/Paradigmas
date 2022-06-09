using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    class ManagerLevel1
    {
        static List<DummyEnemy> dummys = new List<DummyEnemy>();
        List<Item> items = new List<Item>();
        public static Action<int> OnPlayerDeath; // Use this for player respawn, needs to be moved to a general manager?

        // Temporal - testing enemies
        public static Action<Vector2> OnEnemyDeath; 
        private static Random random = new Random();

        public void Gameplay()
        {
            OnPlayerDeath += RespawnPlayer;
            OnEnemyDeath += SpawnEnemy;

            new eDummy(new Vector2(372, 500),90);
            //new eDummy(new Vector2(500, 500));
           // new eDummy(new Vector2(628, 500));

            //new eDummy(new Vector2(500, 372));
           // new eDummy(new Vector2(500, 628));

            // New method of spawing stuff
            //new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(200,200));
            //new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(600, 200));
            //new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(600, 600));
            //new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(800, 200));

            //new Item(ItemType.Repair, new Vector2(200, 0), 10);
            //new Item(ItemType.Special, new Vector2(300, 0), 10);
            //new Item(ItemType.Shield, new Vector2(400, 0), 10);
            //new Item(ItemType.Weapon, new Vector2(500, 0), 30, WeaponTypes.BlueRail);
            //new Item(ItemType.Weapon, new Vector2(600, 0), 30, WeaponTypes.RedDiamond);
            //new Item(ItemType.Weapon, new Vector2(700, 0), 30, WeaponTypes.GreenCrast);
            //new Item(ItemType.Weapon, new Vector2(900, 0), 30, WeaponTypes.HeatTrail);

            new Player(ShipsData.GetShipConfig(0), new Vector2(900, 900), "Player", 20);


            for (int i = 0; i < 5; i++)
            {
                new Item(ItemType.Weapon, new Vector2(900, 0), WeaponTypes.HeatTrail);
            }


            SoundPlayer sfx = new SoundPlayer("resources/sfx/fbattery_loop.wav");
            sfx.PlayLooping();
            SFX.PlayMusic();

            //GameplayManager.InitializeGameplayManager();  Needs to be changed.
        }

        private static void RespawnPlayer(int lifeleft) // Needs to be moved to a general manager?
        {
            if (lifeleft > 0)
            {
                new Player(ShipsData.GetShipConfig(0), new Vector2(900, 900), "Player", 20);
                UI.UpdateLifesLeft();
            }
            else if (lifeleft <= 0) GameplayManager.OnPlayerDeath?.Invoke();
        }

        private static void SpawnEnemy(Vector2 possibleItemSpawnPos)
        {
            dummys.Add(new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(random.Next(200, 800), random.Next(100, 300))));

            float ItemProbability = random.Next(0, 200);
            
            if (ItemProbability > 50 && ItemProbability < 70)
            {
                new Item(ItemType.Repair, possibleItemSpawnPos);
            }
            if (ItemProbability > 80 && ItemProbability < 100)
            {
                new Item(ItemType.Shield, possibleItemSpawnPos);
            }

            if (ItemProbability > 0 && ItemProbability < 1)
            {
                new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.BlueRail);
            }

            if (ItemProbability > 110 && ItemProbability < 130)
            {
                new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.RedDiamond);
            }

            if (ItemProbability > 131 && ItemProbability < 160)
            {
                new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.GreenCrast);
            }

            if (ItemProbability > 190 && ItemProbability < 199)
            {
                new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.HeatTrail);
            }
        }

        public static void Update() 
        { 
            if(MainMenu.ChangeScene == 1) 
            {
                UI.Update();
                //DummyEnemy.Update();  Not needed anymore.
            }
        }
    }
}
