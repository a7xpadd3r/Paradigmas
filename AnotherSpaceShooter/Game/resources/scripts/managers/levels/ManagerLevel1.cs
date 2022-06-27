using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    class ManagerLevel1
    {
        public static Action<int> OnPlayerDeath; // Use this for player respawn, needs to be moved to a general manager?

        // Temporal - testing enemies
        public static Action<Vector2> OnEnemyDeath; 
        static List<MosquitoeBatch> newBatch = new List<MosquitoeBatch>();
        private static Vector2 lastEnemyDeathPos;

        public static float delay = 0.2f;
        public static float currentDelay = 0;
        public static bool allowInput = true;

        public static void WinLoseScreen()
        {

            if (!allowInput) currentDelay += Program.GetDeltaTime();
            if (currentDelay >= delay && !allowInput)
            {
                currentDelay = 0;
                allowInput = true;
            }

            if (Engine.GetKey(Keys.P) && allowInput)
            {
                allowInput = false;
                GameplayManager.OnPlayerDeath();
                UI.score = 0;
                UI.ammo = 0;
                UI.shippys = 3;
            }

            if (Engine.GetKey(Keys.G) && allowInput)
            {
                allowInput = false;
                GameplayManager.OnPlayerWin();
                UI.score = 0;
                UI.ammo = 0;
                UI.shippys = 3;
            }

        }

        public void Gameplay()
        {
            OnPlayerDeath += RespawnPlayer;
            OnEnemyDeath += SetLastDeathPosition;

            // Enemy generation is here
            newBatch.Add(new MosquitoeBatch(300, new Vector2(1300, 300), 5, 0.08f, 2f));
            newBatch.Add(new MosquitoeBatch(1800, new Vector2(200, 600), 5, 0.12f, 0.6f));
            newBatch.Add(new MosquitoeBatch(400, new Vector2(500, 900), 5, 0.1f, 0.1f));

            newBatch.Add(new MosquitoeBatch(400, new Vector2(200, 250), 5, 0.11f, 0.3f));
            newBatch.Add(new MosquitoeBatch(400, new Vector2(500, 250), 5, 0.11f, 0.3f));

            new Player(ShipsData.GetShipConfig(0), new Vector2(900, 900), "Player", 20);

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

        private static void SetLastDeathPosition(Vector2 position) { lastEnemyDeathPos = position; }

        public static void Update() 
        {
            if (MainMenu.ChangeScene == 1)
            {
                UI.Update();
                WinLoseScreen();

                // Testing enemy batches
                if (newBatch.Count != 0)
                {
                    var currentBatch = new MosquitoeBatch();
                    if (UI.EnemyDeathToll < 5 && UI.EnemyDeathToll >= 0) currentBatch = newBatch[0];
                    if (UI.EnemyDeathToll < 10 && UI.EnemyDeathToll >= 5) currentBatch = newBatch[1];
                    if (UI.EnemyDeathToll < 15 && UI.EnemyDeathToll >= 10) { currentBatch = newBatch[2]; }
                    if (UI.EnemyDeathToll < 20 && UI.EnemyDeathToll >= 15) currentBatch = newBatch[3];
                    if (UI.EnemyDeathToll < 25 && UI.EnemyDeathToll >= 20) currentBatch = newBatch[4];

                    if (currentBatch != null) { currentBatch.UpdateSpawner(); }
                }

            }
        }

        
    }
}
