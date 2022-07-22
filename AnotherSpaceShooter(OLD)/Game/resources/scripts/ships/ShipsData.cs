using System.Numerics;

namespace Game
{
    class ShipsData
    {
        //public static float[] position = { 960, 1000 } default spawn points

        // Define players ship stats here

        // Player ships           USAGE:                                  SPEED,     RAIL POSITION,      PROPELLER POSITION,       COLLISION SIZE,          COL. POSITION OFFSET,     SHIP ANIMATION,                                                                PROPELLER ANIMATION                                                              DRAW OFFSET (IF NEEDED)
        private readonly static ShipConfig ElCapitanStats = new ShipConfig(400, new Vector2(48, 0), new Vector2(47.5f, 110f), new Vector2(109.5f, 60), new Vector2(193.6f, 150), new Animation("PlayerAnim", 0, ShipsTextures.GetShipTextures(0), false, true), new Animation("Player", 0.03f, PropellersTextures.GetPropellerTextures(0)));

        // Enemy ships
        private readonly static ShipConfig DummyEnemy1Stats = new ShipConfig(350, new Vector2(80.2f, 22f), new Vector2(-48.75f, -105.7f), new Vector2(75f, 60f), new Vector2(67, -10), new Animation("EnemyAnim", 0, ShipsTextures.GetShipTextures(4), false, true), new Animation("EnemyAnim", 0.038f, PropellersTextures.GetPropellerTextures(0)), new Vector2(10,10));
        private readonly static ShipConfig Mosquitoe = new ShipConfig(600, new Vector2(0, 0), new Vector2(-48.75f, -105.7f), new Vector2(50, 35), new Vector2(-155, -100), ShipsTextures.GetShipAnimation(0), new Animation("EnemyAnim", 0.038f, PropellersTextures.GetPropellerTextures(0)), new Vector2(10, 10));

        public static ShipConfig GetShipConfig(int selection)
        {
            ShipConfig selectedShip = new ShipConfig(0, new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Animation("", 0), new Animation("", 0));

            switch (selection)
            {
                // Player ships
                case 0:
                    selectedShip = ElCapitanStats;
                    break;
                case 1:
                    selectedShip = ElCapitanStats;
                    break;
                case 2:
                    selectedShip = ElCapitanStats;
                    break;

                // Enemy ships
                case 3:
                    selectedShip = DummyEnemy1Stats;
                    break;
                case 4:
                    selectedShip = Mosquitoe;
                    break;
            }

            return selectedShip;
        }

        public static float GetWeaponRecoil(int selection)
        {
            float time = 1f;

            switch (selection)
            {
                // Player times
                case 0:
                    time = 0.6f;
                    break;
                case 1:
                    time = 1f;
                    break;
                case 2:
                    time = 0.32f;
                    break;

                // Enemy times
                case 3:
                    time = 0.25f;
                    break;
                case 4:
                    time = 1.15f;
                    break;
                case 5:
                    time = 1.28f;
                    break;
            }

            return time;
        }
    }
}