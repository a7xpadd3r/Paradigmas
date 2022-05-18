using System;
using System.Media;


namespace Game
{
    class ManagerLevel1
    {

        public static void Gameplay()
        {

            Player.InitializePlayer(ShipsData.GetShipConfig(0));
            DummyEnemy.InitializeDummy(ShipsData.GetShipConfig(3));
            SoundPlayer sfx = new SoundPlayer("resources/sfx/fbattery_loop.wav");
            sfx.PlayLooping();
            SFX.PlayMusic();

            GameplayManager.InitializeGameplayManager();

            
        }

        public static void Update() 
        { 
        if(MainMenu.ChangeScene == 1) 
            {
                DummyEnemy.Update();
                Player.Update();

            }
        

        }


    }
}
