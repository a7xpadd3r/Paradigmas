using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    class ShipsData
    {
        // Define players ship stats here

        // Player ships           USAGE:                                  SPEED,     RAIL POSITION,      PROPELLER POSITION,       COLLISION SIZE,          COL. POSITION OFFSET,     SHIP ANIMATION,                                                                PROPELLER ANIMATION                                                              DRAW OFFSET (IF NEEDED)
        private readonly static ShipConfig ElCapitanStats = new ShipConfig(400f, new Vector2(61.6f, 0), new Vector2(0, -60), new Vector2(109.5f, 60), new Vector2(-10, -50), new Animation("PlayerAnim", 0, ShipsTextures.GetShipTextures(0), false, true), new Animation("Player", 0.03f, PropellersTextures.GetPropellerTextures(0)));

        // Enemy ships
        private readonly static ShipConfig DummyEnemy1Stats = new ShipConfig(350f, new Vector2(80.2f, 22f), new Vector2(-48.75f, -105.7f), new Vector2(128, 128), new Vector2(64, 64), ShipsTextures.GetShipAnimation(0),                                     OtherTextures.GetOtherAnimation(999));

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
            }

            return selectedShip;
        }
    }
}