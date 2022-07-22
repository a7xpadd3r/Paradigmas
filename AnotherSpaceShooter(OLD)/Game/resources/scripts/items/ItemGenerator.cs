using System;
using System.Numerics;

namespace Game
{
    public class ItemGenerator
    {
        private static Random random = new Random();
        public static void GenerateRandomItem(Vector2 possibleItemSpawnPos)
        {
            float ItemProbability = random.Next(0, 200);

            if (ItemProbability > 50 && ItemProbability < 70) new Item(ItemType.Repair, possibleItemSpawnPos);
            if (ItemProbability > 80 && ItemProbability < 100) new Item(ItemType.Shield, possibleItemSpawnPos);
            if (ItemProbability > 0 && ItemProbability < 1) new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.BlueRail);
            if (ItemProbability > 110 && ItemProbability < 130) new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.RedDiamond);
            if (ItemProbability > 131 && ItemProbability < 160) new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.GreenCrast);
            if (ItemProbability > 190 && ItemProbability < 199) new Item(ItemType.Weapon, possibleItemSpawnPos, WeaponTypes.HeatTrail);
    }
}
}
