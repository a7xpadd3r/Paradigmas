using System.Media;

namespace Game
{
    class ManagerLevel1
    {
        static Player p1;
        static DummyEnemy dummyE;
        public static void Gameplay()
        {
            p1 = new Player();
            p1.InitializePlayer(ShipsData.GetShipConfig(0));

            dummyE = new DummyEnemy();
            dummyE.InitializeDummy(ShipsData.GetShipConfig(3));

            SoundPlayer sfx = new SoundPlayer("resources/sfx/fbattery_loop.wav");
            sfx.PlayLooping();
            SFX.PlayMusic();

            GameplayManager.InitializeGameplayManager();
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
