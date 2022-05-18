using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Item
    {
        HealthBoost,
        SpeedBoost
    }
    public static class ItemsFactory
    {
        public static IItems CreateItem(Item item, Vector2 startPosition)
        {
            switch (item)
            {
                case Item.HealthBoost:
                    return new Items("HealthItemBoost", Item.HealthBoost, "Textures/Animations/Items/Health/icon-health.png", 50 ,startPosition, Vector2.One);
                case Item.SpeedBoost:
                    return new SpeedBoostItem("SpeedItemBoost", Item.SpeedBoost, "Textures/Animations/Items/Speed/icon-speed.png", 10 ,startPosition, Vector2.One);
                default:
                    return null;
            }
        }
    }
}
