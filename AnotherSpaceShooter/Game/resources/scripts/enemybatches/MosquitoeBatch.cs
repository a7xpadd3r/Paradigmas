using System.Numerics;

namespace Game
{
    public class MosquitoeBatch
    {
        // Enemies
        private int currentSpawned = 0;
        private int howMany = 1;

        // Time
        private float currentTime = 0;
        private float maxTime = 1;

        // Countdown
        private float currentCountdownTime = 0;
        private float maxCountdown = 1;

        // Vectors
        private Vector2 spawnPos;
        private Vector2 boundaries;

        // Status
        private bool StartSpawing = false;
        public bool Finished = false;

        public MosquitoeBatch(float posX = 0, Vector2 newBoundaries = new Vector2(), int newQuantity = 0, float newSpawnTime = 0, float newCountdown = 0)
        {
            this.howMany = newQuantity;
            this.maxTime = newSpawnTime;
            this.maxCountdown = newCountdown;
            this.spawnPos = new Vector2(posX, -20);
            this.boundaries = newBoundaries;
        }

        public void UpdateSpawner()
        {
            float delta = 0;
            if (!Finished) delta = Program.GetDeltaTime();

            if (!Finished && StartSpawing)
            {
                
                if (currentSpawned == howMany) Finished = true;

                if (currentTime >= maxTime)
                {
                    SpawnEnemy();
                    currentSpawned++;
                    currentTime = 0;
                }

                else if (currentTime < maxTime) currentTime += delta;

            }

            else if (!StartSpawing)
            {
                if (currentCountdownTime >= maxCountdown) StartSpawing = true;
                else currentCountdownTime += delta;
            }
        }

        private void SpawnEnemy()
        {
            new eMosquitoe(spawnPos, boundaries);
        }


        public void UpdateX(float newX)
        {
            float posY = spawnPos.Y;
            spawnPos = new Vector2(newX, posY);
        }

    }
}
