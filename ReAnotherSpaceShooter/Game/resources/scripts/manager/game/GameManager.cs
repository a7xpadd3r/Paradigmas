using System;
using System.Media;
using System.Numerics;

namespace Game
{
    public class GameManager
    {
        public static Vector2 PlayerPosition => theplayer.Position;

        static Player theplayer = new Player(new Vector2(800,800), ShipsProperties.ElCapitan);
        static CollisionTester128 thetester = new CollisionTester128(new Vector2(900, 600));
        static Item itemtest = new Item("World", 0);

        public static void InitializeGame()
        {
            int newitemid = mGameObject.GenerateObjectID();
            var itemm = fyPoolDay.Pool.CreateItem("World", newitemid);
            itemm.Awake(new Vector2(600, 600), ItemType.Weapon, WeaponTypes.GreenCrast);


            //itemtest.Awake(new Vector2(500,500));
            SoundPlayer music = new SoundPlayer("resources/sfx/music/fbattery_loop.wav");
            //music.Play();
        }

        public static void Update()
        {
            mGameObject.Update();
            mEffects.Update(PlayerPosition);
        }

        public static void GiveItem(ItemType itemtype, WeaponTypes weaptype)
        {
            switch (itemtype)
            {
                case ItemType.Repair:
                    break;
                case ItemType.Shield:
                    break;
                case ItemType.Special:
                    break;
                case ItemType.Weapon:
                    switch (weaptype)
                    {
                        case WeaponTypes.BlueRail:  theplayer.GetWeapon(WeaponTypes.BlueRail);    break;
                        case WeaponTypes.RedDiamond:theplayer.GetWeapon(WeaponTypes.RedDiamond);  break;
                        case WeaponTypes.GreenCrast:theplayer.GetWeapon(WeaponTypes.GreenCrast);  break;
                        case WeaponTypes.HeatTrail: theplayer.GetWeapon(WeaponTypes.HeatTrail);   break;
                        case WeaponTypes.OrbWeaver: theplayer.GetWeapon(WeaponTypes.OrbWeaver);   break;
                        case WeaponTypes.Gamma:     theplayer.GetWeapon(WeaponTypes.Gamma);       break;
                    }   break;
            }
        }
    }
}
