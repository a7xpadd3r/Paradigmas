using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    class ManagerLevel1
    {
        List<DummyEnemy> dummys = new List<DummyEnemy>();
        static List<Player> players = new List<Player>();
        List<Item> items = new List<Item>();
        public static Action<int> OnPlayerDeath; // Use this for player respawn, needs to be moved to a general manager?

        public void Gameplay()
        {
            OnPlayerDeath += RespawnPlayer;

            // New method of spawing stuff
            dummys.Add(new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(800,200)));
            dummys.Add(new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(500,400)));
            dummys.Add(new DummyEnemy(ShipsData.GetShipConfig(3), new Vector2(200,600)));

            //new Item(ItemType.Repair, new Vector2(200, 0), 10);
            //new Item(ItemType.Special, new Vector2(300, 0), 10);
            //new Item(ItemType.Shield, new Vector2(400, 0), 10);
            new Item(ItemType.Weapon, new Vector2(500, 0), 30, WeaponTypes.BlueRail);
            new Item(ItemType.Weapon, new Vector2(600, 0), 30, WeaponTypes.RedDiamond);
            new Item(ItemType.Weapon, new Vector2(700, 0), 30, WeaponTypes.GreenCrast);

            players.Add(new Player(ShipsData.GetShipConfig(0), new Vector2(900, 900), "Player", 20));



            SoundPlayer sfx = new SoundPlayer("resources/sfx/fbattery_loop.wav");
            sfx.PlayLooping();
            SFX.PlayMusic();

            //GameplayManager.InitializeGameplayManager();  Needs to be changed.
        }

        private static void RespawnPlayer(int lifeleft) // Needs to be moved to a general manager?
        {
            if (lifeleft > 0)
            {
                players.Add(new Player(ShipsData.GetShipConfig(0), new Vector2(900, 900), "Player", 20));
                UI.UpdateLifesLeft();
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
