using System;
using System.Collections.Generic;

namespace Game
{
    public enum ShipsAnimations { ElCapitan, SonicShip, SkullFlower, Mosquitoe, Slider, Tremor, sMosquitoe, sSlider, sTremor }
    public enum ShipPropeller { Red, Blue, White }
    public enum ShieldColor { Green, Red, }
    public enum Proyectile { BlueRail, RedTrail, RedTrailBall, GreenCrast, HeatTrail, OrbWeaver, GammaBeam, Enemy1, Enemy2, Enemy3 }
    public enum EffectsAnimations { PurpleBeam, Smoke1, OrbWeaverCharge, ThunderLine, OrbWeaverImpact, ItemGlow }
    public enum Background { Stars, Planets }
    public enum UITextures { PlayButton, ControlsButton, CreditsButton, ExitButton, ElCapitanShip, SonicShip, SkullFlowerShip, SlotBox, Numbers, WeaponList }
    public enum DebugAnimation { Box128, Box64, Box32, Box16, Box8, Box64x128 }
    public enum DebugTexture { GreenDot, RedDot }
    public class Textures
    {
        // Textures
        private static Texture transparent = new Texture("resources/gfx/blank.png");
        private static Texture notexture128 = new Texture("resources/gfx/notexture.png");
        private static Texture notexture64 = new Texture("resources/gfx/64x64.png");
        private static Texture notexture64x128 = new Texture("resources/gfx/128x64.png");
        private static Texture notexture32 = new Texture("resources/gfx/32x32.png");
        private static Texture greendot = new Texture("resources/gfx/dotgreen.png");
        private static Texture reddot = new Texture("resources/gfx/dotred.png");

        // Default
        private static List<Texture> notexturelist128 = new List<Texture>();
        private static List<Texture> notexturelist64 = new List<Texture>();
        private static List<Texture> notexturelist64x128 = new List<Texture>();
        private static List<Texture> notexturelist32 = new List<Texture>();

        // Ships
        private static List<Texture> elcapitan = new List<Texture>();
        private static List<Texture> sonicship = new List<Texture>();
        private static List<Texture> skullflower = new List<Texture>();
        private static List<Texture> mosquitoe = new List<Texture>();
        private static List<Texture> slider = new List<Texture>();
        private static List<Texture> tremor = new List<Texture>();
        private static List<Texture> specialmosquitoe = new List<Texture>();
        private static List<Texture> specialslider = new List<Texture>();
        private static List<Texture> specialtremor = new List<Texture>();

        public static Animation MosquitoeAnim => new Animation("MosquitoeAnim", 0.001f, mosquitoe);
        public static Animation SpecialMosquitoeAnim => new Animation("SpecialMosquitoeAnim", 0.001f, specialmosquitoe);

        public static List<Texture> SliderTextures => slider;
        public static List<Texture> SpecialSliderTextures => specialslider;
        public static List<Texture> TremorTextures => tremor;
        public static List<Texture> SpecialTremorTextures => specialtremor;


        // Propellers
        private static List<Texture> propellersset1 = new List<Texture>();
        private static List<Texture> propellersset2 = new List<Texture>();
        private static List<Texture> propellersset3 = new List<Texture>();
        public static Animation RedPropeller => new Animation("Propellers", 0.03f, propellersset1);
        public static Animation BluePropeller => new Animation("Blue Propeller", 0.05f, propellersset2);
        public static Animation WhitePropeller => new Animation("White Propeller", 0.021f, propellersset3);

        // Shields
        private static List<Texture> shieldgreen = new List<Texture>();
        private static List<Texture> shieldred = new List<Texture>();

        public static List<Texture> GreenShield => shieldgreen;
        public static List<Texture> RedShield => shieldred;

        // Proyectiles
        private static List<Texture> pbluerail = new List<Texture>();
        private static List<Texture> preddiamond = new List<Texture>();
        private static List<Texture> preddiamondball = new List<Texture>();
        private static List<Texture> pgreencrast = new List<Texture>();
        private static List<Texture> pheattrail = new List<Texture>();
        private static List<Texture> worbweaver = new List<Texture>();
        private static List<Texture> porbweaver = new List<Texture>();
        private static List<Texture> pgamma = new List<Texture>();
        private static List<Texture> enemy1 = new List<Texture>();
        private static List<Texture> enemy2 = new List<Texture>();
        private static List<Texture> enemy3 = new List<Texture>();

        // Effects
        private static List<Texture> purplebeam = new List<Texture>();
        private static List<Texture> smoke1 = new List<Texture>();
        private static List<Texture> thunderline = new List<Texture>();
        private static List<Texture> orbweaverimpact = new List<Texture>();
        private static List<Texture> itemglow = new List<Texture>();

        // Decoration
        private static List<Texture> stars = new List<Texture>();
        private static List<Texture> planets = new List<Texture>();

        // Items
        public static List<Texture> ItemsList => itemlist;
        private static List<Texture> itemlist = new List<Texture>();

        // Interface
        private static List<Texture> numbers = new List<Texture>();
        private static List<Texture> playbutton = new List<Texture>();
        private static List<Texture> controlsbutton = new List<Texture>();
        private static List<Texture> creditsbuttons = new List<Texture>();
        private static List<Texture> exitbutton = new List<Texture>();
        private static List<Texture> ielcapitanship = new List<Texture>();
        private static List<Texture> isonicship = new List<Texture>();
        private static List<Texture> iskullflower = new List<Texture>();
        private static List<Texture> slotbox = new List<Texture>();
        private static List<Texture> weaponlist = new List<Texture>();

        // Spash
        public static readonly Texture splashmainmenu = new Texture("resources/gfx/ui/scenes/MainMenu.png");
        public static readonly Texture splashcontrols = new Texture("resources/gfx/ui/scenes/Controls.png");
        public static readonly Texture splashcredits = new Texture("resources/gfx/ui/scenes/Credits.png");
        public static readonly Texture splashwin = new Texture("resources/gfx/ui/scenes/YouWin.png");
        public static readonly Texture splashlose = new Texture("resources/gfx/ui/scenes/GameOver.png");

        // Default
        private static Animation noanim = new Animation("null", 0, notexturelist128, false, 0, true);

        public static void InitializeTextures()
        {
            // Create textures list
            notexturelist128.Add(notexture128);
            notexturelist64.Add(notexture64);
            notexturelist64x128.Add(notexture64x128);
            notexturelist32.Add(notexture32);

            // Ships
            //elcapitan.Add(notexture128);
            elcapitan.Add(new Texture("resources/gfx/ship/elcapitan/ElCapitan-3.png"));
            elcapitan.Add(new Texture("resources/gfx/ship/elcapitan/ElCapitan-2.png"));
            elcapitan.Add(new Texture("resources/gfx/ship/elcapitan/ElCapitan-1.png"));
            elcapitan.Add(new Texture("resources/gfx/ship/elcapitan/ElCapitan-0.png"));
            sonicship.Add(new Texture("resources/gfx/ship/sonicship/sonicship_3.png"));
            sonicship.Add(new Texture("resources/gfx/ship/sonicship/sonicship_2.png"));
            sonicship.Add(new Texture("resources/gfx/ship/sonicship/sonicship_1.png"));
            sonicship.Add(new Texture("resources/gfx/ship/sonicship/sonicship_0.png"));
            skullflower.Add(new Texture("resources/gfx/ship/skullflower/skullflower_3.png"));
            skullflower.Add(new Texture("resources/gfx/ship/skullflower/skullflower_2.png"));
            skullflower.Add(new Texture("resources/gfx/ship/skullflower/skullflower_1.png"));
            skullflower.Add(new Texture("resources/gfx/ship/skullflower/skullflower_0.png"));
            for (int i = 0; i < 6; i++) mosquitoe.Add(new Texture("resources/gfx/ship/enemies/mosquitoe/mosquitoe-" + i + ".png"));
            for (int i = 5; i > 0; i--) mosquitoe.Add(new Texture("resources/gfx/ship/enemies/mosquitoe/mosquitoe-" + i + ".png"));
            for (int i = 0; i < 6; i++) specialmosquitoe.Add(new Texture("resources/gfx/ship/enemies/mosquitoe/mosquitoe-special_" + i + ".png"));
            for (int i = 6; i < 0; i--) specialmosquitoe.Add(new Texture("resources/gfx/ship/enemies/mosquitoe/mosquitoe-special_" + i + ".png"));
            for (int i = 0; i < 4; i++) slider.Add(new Texture("resources/gfx/ship/enemies/slider/slider" + i + ".png"));
            for (int i = 0; i < 4; i++) specialslider.Add(new Texture("resources/gfx/ship/enemies/slider/slider_special" + i + ".png"));
            for (int i = 0; i < 4; i++) tremor.Add(new Texture("resources/gfx/ship/enemies/tremor/tremper" + i + ".png"));
            for (int i = 0; i < 4; i++) specialtremor.Add(new Texture("resources/gfx/ship/enemies/tremor/tremper_special" + i + ".png"));

            // Propellers
            for (int i = 1; i < 5; i++) propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller" + i + ".png"));
            propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller3.png"));
            propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller2.png"));
            for (int i = 1; i < 5; i++) propellersset2.Add(new Texture("resources/gfx/propellers/blue/blue_propeller" + i + ".png"));
            propellersset2.Add(new Texture("resources/gfx/propellers/blue/blue_propeller3.png"));
            propellersset2.Add(new Texture("resources/gfx/propellers/blue/blue_propeller2.png"));
            for (int i = 1; i < 5; i++) propellersset3.Add(new Texture("resources/gfx/propellers/white/white_propeller" + i + ".png"));
            propellersset3.Add(new Texture("resources/gfx/propellers/white/white_propeller3.png"));
            propellersset3.Add(new Texture("resources/gfx/propellers/white/white_propeller3.png"));

            // Shields
            shieldgreen.Add(transparent);
            for (int i = 0; i != 6; i++) shieldgreen.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));
            for (int i = 5; i != 1; i--) shieldgreen.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));
            shieldred.Add(transparent);
            for (int i = 0; i != 6; i++) shieldred.Add(new Texture("resources/gfx/shield/red/shieldred-" + i + ".png"));
            for (int i = 5; i != 1; i--) shieldred.Add(new Texture("resources/gfx/shield/red/shieldred-" + i + ".png"));

            // Proyectiles
            for (int i = 0; i < 5; i++) pbluerail.Add(new Texture("resources/gfx/proyectiles/bluerail/pbluerail-" + i + ".png"));
            for (int i = 0; i < 7; i++) preddiamond.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_" + i + ".png"));
            for (int i = 0; i < 7; i++) preddiamondball.Add(new Texture("resources/gfx/proyectiles/reddiamond/ball/preddiamondball_" + i + ".png"));
            for (int i = 0; i < 6; i++) pgreencrast.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_" + i + ".png"));
            for (int i = 5; i < 0; i--) pgreencrast.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_" + i + ".png"));
            for (int i = 0; i < 5; i++) pheattrail.Add(new Texture("resources/gfx/proyectiles/heattrail/fire1-" + i + ".png"));
            for (int i = 0; i < 12; i++) worbweaver.Add(new Texture("resources/gfx/proyectiles/orbweaver/wOrbWeaver_0" + i + ".png"));
            for (int i = 0; i < 3; i++) porbweaver.Add(new Texture("resources/gfx/proyectiles/orbweaver/orbweaverball/wOrbWeaverLoop_" + i + ".png"));
            for (int i = 0; i < 5; i++) pgamma.Add(new Texture("resources/gfx/proyectiles/gamma/gamma_" + i + ".png"));

            for (int i = 0; i < 5; i++) enemy1.Add(new Texture("resources/gfx/proyectiles/enemybluerail/pEnemyBlueRail-" + i + ".png"));
            for (int i = 0; i < 7; i++) enemy2.Add(new Texture("resources/gfx/proyectiles/enemyreddiamond/pEnemyRedDiamond-" + i + ".png"));
            for (int i = 0; i < 6; i++) enemy3.Add(new Texture("resources/gfx/proyectiles/enemygreencrast/pEnemyGreenCrast-" + i + ".png"));
            for (int i = 5; i < 0; i--) enemy3.Add(new Texture("resources/gfx/proyectiles/enemygreencrast/pEnemyGreenCrast-" + i + ".png"));

            // Effects
            for (int i = 5; i != 0; i--) purplebeam.Add(new Texture("resources/gfx/effects/beam1/proyectilebeam-" + i + ".png"));
            purplebeam.Add(transparent);
            for (int i = 0; i < 5; i++) smoke1.Add(new Texture("resources/gfx/effects/smoke1/smoke1-" + i + ".png"));
            smoke1.Add(transparent);
            for (int i = 0; i < 4; i++) thunderline.Add(new Texture("resources/gfx/effects/thunderline/thunderline_" + i + ".png"));
            for (int i = 0; i < 8; i++) orbweaverimpact.Add(new Texture("resources/gfx/effects/orbweaverimpact/orbweaverimpact_" + i + ".png"));
            for (int i = 0; i < 6; i++) itemglow.Add(new Texture("resources/gfx/effects/itemglow/itemeffect-white-" + i + ".png"));
            for (int i = 4; i > 0; i--) itemglow.Add(new Texture("resources/gfx/effects/itemglow/itemeffect-white-" + i + ".png"));
            for (int i = 0; i < 5; i++) itemglow.Add(new Texture("resources/gfx/effects/itemglow/itemeffect-white-" + i + ".png"));

            // Item list
            itemlist.Add(new Texture("resources/gfx/items/wrench.png"));
            itemlist.Add(new Texture("resources/gfx/items/shield.png"));
            itemlist.Add(new Texture("resources/gfx/items/special.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/bluerail/pbluerail-0.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_0.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_0.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/heattrail/fire1-0.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/orbweaver/wOrbWeaver_012.png"));
            itemlist.Add(new Texture("resources/gfx/proyectiles/gamma/gamma_0.png"));

            // UI
            for (int i = 0; i < 10; i++) numbers.Add(new Texture("resources/gfx/ui/numbers/" + i + ".png"));
            numbers.Add(new Texture("resources/gfx/ui/numbers/dot.png"));
            playbutton.Add(new Texture("resources/gfx/ui/PlayButton.png"));
            playbutton.Add(new Texture("resources/gfx/ui/PlayButtonSelect.png"));
            controlsbutton.Add(new Texture("resources/gfx/ui/ControlsButton.jpg"));
            controlsbutton.Add(new Texture("resources/gfx/ui/ControlsButtonSelect.jpg"));
            creditsbuttons.Add(new Texture("resources/gfx/ui/CreditsButton.png"));
            creditsbuttons.Add(new Texture("resources/gfx/ui/CreditsButtonSelect.jpg"));
            exitbutton.Add(new Texture("resources/gfx/ui/ExitButton.jpg"));
            exitbutton.Add(new Texture("resources/gfx/ui/ExitButtonSelect.jpg"));
            ielcapitanship.Add(new Texture("resources/gfx/ui/ElCapitanShip.png"));
            ielcapitanship.Add(new Texture("resources/gfx/ui/ElCapitanShipSelected.png"));
            isonicship.Add(new Texture("resources/gfx/ui/SonicShip.png"));
            isonicship.Add(new Texture("resources/gfx/ui/SonicShipSelected.png"));
            iskullflower.Add(new Texture("resources/gfx/ui/SkullFlowerShip.png"));
            iskullflower.Add(new Texture("resources/gfx/ui/SkullFlowerShipSelect.png"));
            slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-0.png"));
            slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-0.png"));
            slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-0.png"));
            slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-0.png"));
            slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-0.png"));
            for (int i = 0; i < 11; i++) slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-" + i + ".png"));
            for (int i = 10; i > 0; i--) slotbox.Add(new Texture("resources/gfx/ui/WeaponBox/weapbox-" + i + ".png"));

            weaponlist.Add(new Texture("resources/gfx/proyectiles/bluerail/pbluerail-0.png"));
            weaponlist.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_0.png"));
            weaponlist.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_0.png"));
            weaponlist.Add(new Texture("resources/gfx/proyectiles/heattrail/fire1-0.png"));
            weaponlist.Add(new Texture("resources/gfx/proyectiles/orbweaver/orbweaverball/wOrbWeaverLoop_3.png"));
            weaponlist.Add(new Texture("resources/gfx/proyectiles/gamma/gamma_0.png"));

            // Stars
            for (int i = 0; i < 4; i++) stars.Add(new Texture("resources/gfx/background/star/var1_star" + i + ".png"));
            planets.Add(new Texture("resources/gfx/background/deco1.png"));
        }
        public static Animation GetShipAnimation(ShipsAnimations whichone)
        {
            Animation value = noanim;
            switch (whichone)
            {
                case ShipsAnimations.ElCapitan: value = new Animation("ElCapitanAnim", 0, elcapitan, false, 0, true); break;
                case ShipsAnimations.SonicShip: value = new Animation("SonicShipAnim", 0, sonicship, false, 0, true); break;
                case ShipsAnimations.SkullFlower: value = new Animation("SkullFlowerAnim", 0, skullflower, false, 0, true); break;
                case ShipsAnimations.Mosquitoe: value = new Animation("MosquitoeAnim", 0.001f, mosquitoe); break;
                case ShipsAnimations.sMosquitoe: value = new Animation("SpecialMosquitoeAnim", 0.001f, specialmosquitoe); break;
                case ShipsAnimations.Slider: value = new Animation("SliderAnim", 0, slider, false, 0, true); break;
                case ShipsAnimations.sSlider: value = new Animation("SpecialSliderAnim", 0, specialslider, false, 0, true); break;
                case ShipsAnimations.Tremor: value = new Animation("TremorAnim", 0, tremor, false, 0, true); break;
                case ShipsAnimations.sTremor: value = new Animation("SpecialTremorAnim", 0, specialtremor, false, 0, true); break;
            }
            return value;
        }
        public static Animation GetPropellerAnimation(ShipPropeller whichone)
        {
            Animation value = noanim;
            switch (whichone)
            {
                case ShipPropeller.Red: value = new Animation("Propellers", 0.03f, propellersset1); break;
                case ShipPropeller.Blue: value = new Animation("Blue Propeller", 0.05f, propellersset2); break;
                case ShipPropeller.White: value = new Animation("White Propeller", 0.021f, propellersset3); break;
            }
            return value;
        }
        public static Animation GetShieldAnimation(ShieldColor whichcolor)
        {
            Animation value = noanim;

            switch (whichcolor)
            {
                case ShieldColor.Green:
                    value = new Animation("GreenShield", 0.03f, shieldgreen, true, 2, false, false);
                    break;
                case ShieldColor.Red:
                    value = new Animation("RedShield", 0.027f, shieldred, true, 1, false, false);
                    break;
            }

            return value;
        }
        public static Animation GetProyectileAnimation(Proyectile whichone)
        {
            Animation value = noanim;
            switch (whichone)
            {
                case Proyectile.BlueRail:     value = new Animation("Blue Rail Anim", 0.04f, pbluerail);              break;
                case Proyectile.RedTrail:     value = new Animation("Red Trail Anim", 0.09f, preddiamond);            break;
                case Proyectile.RedTrailBall: value = new Animation("Red Trail Ball Anim", 0.15f, preddiamondball);   break;
                case Proyectile.GreenCrast:   value = new Animation("Green Crast Anim", 0.15f, pgreencrast);          break;
                case Proyectile.HeatTrail:    value = new Animation("Heat Trail Anim", 0.05f, pheattrail);            break;
                case Proyectile.OrbWeaver:    value = new Animation("Orb Weaver Anim", 0.05f, porbweaver);            break;
                case Proyectile.GammaBeam:    value = new Animation("Gamma Anim", 0.05f, pgamma, false, 1);           break;
                case Proyectile.Enemy1:       value = new Animation("Enemy Blue Rail Anim", 0.08f, enemy1);           break;
                case Proyectile.Enemy2:       value = new Animation("Enemy Red Trail Anim", 0.15f, enemy2);           break;
                case Proyectile.Enemy3:       value = new Animation("Enemy Green Crast Anim", 0.30f, enemy3);         break;
            }
            return value;
        }
        public static Animation GetEffectAnimation(EffectsAnimations whichone)
        {
            Animation value = noanim;
            switch (whichone)
            {
                case EffectsAnimations.PurpleBeam:
                    value = new Animation("Purple Beam", 0.03f, purplebeam, false);
                    break;
                case EffectsAnimations.Smoke1:
                    value = new Animation("Smoke1", 0.07f, smoke1, false);
                    break;
                case EffectsAnimations.OrbWeaverCharge:
                    value = new Animation("Orb Weaver Charge", 0.05f, worbweaver, false, 1, false, true);
                    break;
                case EffectsAnimations.ThunderLine:
                    value = new Animation("Thunder Line", 0.05f, thunderline);
                    break;
                case EffectsAnimations.OrbWeaverImpact:
                    value = new Animation("Orb Weaver Impact", 0.06f, orbweaverimpact, false, 1);
                    break;
                case EffectsAnimations.ItemGlow:
                    value = new Animation("Orb Weaver Impact", 0.1f, itemglow);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static List<Texture> GetBackgroundSprite(Background wichone)
        {
            List<Texture> value = notexturelist128;
            switch (wichone)
            {
                case Background.Stars:
                    value = stars;
                    break;
                case Background.Planets:
                    value = planets;
                    break;

                default:
                    break;
            }
            return value;
        }
        public static List<Texture> GetUITextures(UITextures whichone)
        {
            List<Texture> value = notexturelist128;
            switch (whichone)
            {
                case UITextures.PlayButton:      value = playbutton;     break;
                case UITextures.ControlsButton:  value = controlsbutton; break;
                case UITextures.CreditsButton:   value = creditsbuttons; break;
                case UITextures.ExitButton:      value = exitbutton;     break;
                case UITextures.ElCapitanShip:   value = ielcapitanship; break;
                case UITextures.SonicShip:       value = isonicship;     break;
                case UITextures.SkullFlowerShip: value = iskullflower;   break;
                case UITextures.SlotBox:         value = slotbox;        break;
                case UITextures.Numbers:         value = numbers;        break;
                case UITextures.WeaponList:      value = weaponlist;     break;
            }
            return value;
        }
        public static Animation GetDebugAnimation(DebugAnimation whichone)
        {
            Animation value = noanim;

            switch (whichone)
            {
                case DebugAnimation.Box128:
                    value = new Animation("Debug", 0, notexturelist128);
                    break;
                case DebugAnimation.Box64:
                    value = new Animation("Debug", 0, notexturelist64);
                    break;
                case DebugAnimation.Box32:
                    value = new Animation("Debug", 0, notexturelist32);
                    break;
                case DebugAnimation.Box16:
                    break;
                case DebugAnimation.Box8:
                    break;
                case DebugAnimation.Box64x128:
                    value = new Animation("Debug", 0, notexturelist64x128);
                    break;
                default:
                    break;
            }

            return value;
        }
        public static Texture GetDebugTexture(DebugTexture whattexture)
        {
            Texture value = notexture128;

            switch (whattexture)
            {
                case DebugTexture.GreenDot:
                    value = greendot;
                    break;
                case DebugTexture.RedDot:
                    value = reddot;
                    break;
            }

            return value;
        }
    }
}
