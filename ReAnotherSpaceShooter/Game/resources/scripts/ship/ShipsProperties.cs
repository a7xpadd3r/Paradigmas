using System.Numerics;

namespace Game
{
    public class ShipsProperties // This file contains all the ships configurations.
    {
        // El Capitan
        private static readonly ShipShieldData capitanshielddata = new ShipShieldData(Textures.GetShieldAnimation(ShieldColor.Green), new Vector2(48, 40), new Vector2(4, 4));
        private static readonly ShipPropellerData capitanpropeller = new ShipPropellerData(Textures.RedPropeller, new Vector2(0, -55), new Vector2(1,1));
        private static readonly DoubleVector2 capitancollidervector = new DoubleVector2(new Vector2(-14.5f, -55), new Vector2(100, 50));
        private static readonly Vector2 capitanrailpos = new Vector2(0, 45);
        private static readonly float capitanspeed = 380, capitandamage = 1;
        // Sonic Ship
        private static readonly ShipShieldData sonicshipshielddata = new ShipShieldData(Textures.GetShieldAnimation(ShieldColor.Green), new Vector2(48, 40), new Vector2(4, 4));
        private static readonly ShipPropellerData sonicshippropeller1 = new ShipPropellerData(Textures.GetPropellerAnimation(ShipPropeller.Blue), new Vector2(-34, -55), new Vector2(0.6f, 0.8f));
        private static readonly ShipPropellerData sonicshippropeller2 = new ShipPropellerData(Textures.GetPropellerAnimation(ShipPropeller.Blue), new Vector2(23, -55), new Vector2(0.6f, 0.8f));
        private static readonly DoubleVector2 sonicshipcollidervector = new DoubleVector2(new Vector2(-25, -65), new Vector2(80, 40));
        private static readonly Vector2 sonicshiprailpos = new Vector2(0, 35);
        private static readonly float sonicshipspeed = 500, sonicshipdamage = 0.65f;
        
        // Skull Flower
        private static readonly ShipShieldData skullflowershielddata = new ShipShieldData(Textures.GetShieldAnimation(ShieldColor.Green), new Vector2(48, 40), new Vector2(4, 4));
        private static readonly ShipPropellerData skullflowerpropeller1 = new ShipPropellerData(Textures.BluePropeller, new Vector2(38.5f, -58), new Vector2(1, 1));
        private static readonly ShipPropellerData skullflowerpropeller2 = new ShipPropellerData(Textures.RedPropeller, new Vector2(0, -58), new Vector2(1, 1));
        private static readonly ShipPropellerData skullflowerpropeller3 = new ShipPropellerData(Textures.BluePropeller, new Vector2(-38, -58), new Vector2(1, 1));
        private static readonly DoubleVector2 skullflowercollidervector = new DoubleVector2(new Vector2(-10, -55), new Vector2(110, 50));
        private static readonly Vector2 skullflowerrailpos = new Vector2(0, 45);
        private static readonly float skullflowerspeed = 280, skullflowerdamage = 1.5f;

        // Mosquitoe
        private static readonly DoubleVector2 mosquitoecollidervector = new DoubleVector2(new Vector2(0, -10), new Vector2(35, 35));
        private static readonly Vector2 mosquitoerail = new Vector2(10, -23);
        private static readonly float mosquitoespeed = 500, mosquitoedamage = 1;

        // Slider
        private static readonly ShipShieldData slidershielddata = new ShipShieldData(Textures.GetShieldAnimation(ShieldColor.Red), new Vector2(55, 60), new Vector2(4.3f, 4.3f));
        private static readonly ShipPropellerData sliderpropellerdata = new ShipPropellerData(Textures.WhitePropeller, new Vector2(-18, 18), new Vector2(2.2f, 2.2f));
        private static readonly DoubleVector2 slidercollidervector = new DoubleVector2(new Vector2(-21, -28), new Vector2(85, 80));
        private static readonly Vector2 sliderrail = new Vector2(1.2f, -78);
        private static readonly float sliderspeed = 400, slideredamage = 1.65f;

        // Tremor
        private static readonly ShipShieldData tremorshielddata = new ShipShieldData(Textures.GetShieldAnimation(ShieldColor.Red), new Vector2(55, 60), new Vector2(4.3f, 4.3f));
        private static readonly ShipPropellerData tremorpropellerdata = new ShipPropellerData(Textures.RedPropeller, new Vector2(-15.4f, 52), new Vector2(1.9f, 1.9f));
        private static readonly DoubleVector2 tremorcollidervector = new DoubleVector2(new Vector2(-20, -24), new Vector2(88, 80));
        private static readonly Vector2 tremorrail = new Vector2(0, -60);
        private static readonly float tremorspeed = 350, tremordamage = 3;

        // To spawn
        public static readonly ShipData ElCapitan = new ShipData(capitanspeed, capitandamage, capitanrailpos, Textures.GetShipAnimation(ShipsAnimations.ElCapitan), capitanshielddata, capitanpropeller, capitancollidervector);
        public static readonly ShipData SonicShip = new ShipData(sonicshipspeed, sonicshipdamage, sonicshiprailpos, Textures.GetShipAnimation(ShipsAnimations.SonicShip), sonicshipshielddata, sonicshippropeller1, sonicshippropeller2, sonicshipcollidervector);
        public static readonly ShipData SkullFlower = new ShipData(skullflowerspeed, skullflowerdamage, skullflowerrailpos, Textures.GetShipAnimation(ShipsAnimations.SkullFlower), skullflowershielddata, skullflowerpropeller1, skullflowerpropeller2, skullflowerpropeller3, skullflowercollidervector);

        public static readonly ShipData Mosquitoe = new ShipData(mosquitoespeed, mosquitoedamage, mosquitoerail, Textures.MosquitoeAnim, mosquitoecollidervector);
        public static readonly ShipData SpecialMosquitoe = new ShipData(mosquitoespeed, mosquitoedamage, mosquitoerail, Textures.SpecialMosquitoeAnim, mosquitoecollidervector);

        public static ShipData Slider => new ShipData(sliderspeed, slideredamage, sliderrail, Textures.GetShipAnimation(ShipsAnimations.Slider), slidershielddata, sliderpropellerdata, slidercollidervector);
        public static ShipData SpecialSlider => new ShipData(sliderspeed, slideredamage, sliderrail, Textures.GetShipAnimation(ShipsAnimations.sSlider), slidershielddata, sliderpropellerdata, slidercollidervector);
        public static ShipData Tremor => new ShipData(tremorspeed, tremordamage, tremorrail, Textures.GetShipAnimation(ShipsAnimations.Tremor), tremorshielddata, tremorpropellerdata, tremorcollidervector);
        public static ShipData SpecialTremor => new ShipData(tremorspeed, tremordamage, tremorrail, Textures.GetShipAnimation(ShipsAnimations.sTremor), tremorshielddata, tremorpropellerdata, tremorcollidervector);

    }
}
