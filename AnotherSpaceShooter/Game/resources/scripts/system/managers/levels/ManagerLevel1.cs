using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    class ManagerLevel1
    {
        static Player p1;
        static DummyEnemy dummyE;
        List<DummyEnemy> dummys = new List<DummyEnemy>();

        public void Gameplay()
        {
            p1 = new Player();
            p1.spawnPosition = new Vector2(870,900);
            p1.InitializePlayer(ShipsData.GetShipConfig(0));

            //dummyE = new DummyEnemy();
            //dummyE.InitializeDummy(ShipsData.GetShipConfig(3));

            dummys.Add(new DummyEnemy(new Vector2(800,200)));
            dummys.Add(new DummyEnemy(new Vector2(500,400)));
            dummys.Add(new DummyEnemy(new Vector2(200,600)));
            //dummys.Add(new DummyEnemy());
            //dummys.Add(new DummyEnemy());

            for (int i = 0; i < dummys.Count; i++)
			{
                DummyEnemy TheDummy = dummys[i];
                TheDummy.InitializeDummy(ShipsData.GetShipConfig(3));
			}

            SoundPlayer sfx = new SoundPlayer("resources/sfx/fbattery_loop.wav");
            sfx.PlayLooping();
            SFX.PlayMusic();

            //GameplayManager.InitializeGameplayManager();
        }

        public static void Update() 
        { 
        if(MainMenu.ChangeScene == 1) 
            {
                //DummyEnemy.Update();
            }
        

        }


    }
}
