using System.Collections.Generic;

namespace Game
{
    public enum ShipsAnimations { ElCapitan }
    public enum ShipPropeller { Red }
    public enum ShieldColor { Green, Red, Blue, White }
    public enum Proyectile { BlueRail, RedTrail, RedTrailBall, GreenCrast, HeatTrail, OrbWeaver, GammaBeam }
    public enum EffectsAnimations { PurpleBeam, Smoke1, OrbWeaverCharge, ThunderLine, OrbWeaverImpact, ItemGlow }
    public enum Background { Stars, Planets }
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

        // Lists
        // Default
        private static List<Texture> notexturelist128 = new List<Texture>();
        private static List<Texture> notexturelist64 = new List<Texture>();
        private static List<Texture> notexturelist64x128 = new List<Texture>();
        private static List<Texture> notexturelist32 = new List<Texture>();

        // Ships
        private static List<Texture> elcapitan = new List<Texture>();

        // Propellers
        private static List<Texture> propellersset1 = new List<Texture>();

        // Shields
        private static List<Texture> shieldgreen = new List<Texture>();
        private static List<Texture> shieldred = new List<Texture>();

        // Proyectiles
        private static List<Texture> pbluerail = new List<Texture>();
        private static List<Texture> preddiamond = new List<Texture>();
        private static List<Texture> preddiamondball = new List<Texture>();
        private static List<Texture> pgreencrast = new List<Texture>();
        private static List<Texture> pheattrail = new List<Texture>();
        private static List<Texture> worbweaver = new List<Texture>();
        private static List<Texture> porbweaver = new List<Texture>();
        private static List<Texture> pgamma = new List<Texture>();

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

            // Propellers
            for (int i = 1; i < 5; i++) propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller" + i + ".png"));
            propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller3.png"));
            propellersset1.Add(new Texture("resources/gfx/propellers/set1/elcapitan_propeller2.png"));

            // Shields
            shieldgreen.Add(transparent);
            for (int i = 0; i != 6; i++) shieldgreen.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));
            for (int i = 5; i != 1; i--) shieldgreen.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));
            shieldred.Add(transparent);
            for (int i = 0; i != 6; i++) shieldred.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));
            for (int i = 5; i != 1; i--) shieldred.Add(new Texture("resources/gfx/shield/green/shieldgreen-" + i + ".png"));

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

            // Stars
            for (int i = 0; i < 4; i++) stars.Add(new Texture("resources/gfx/background/star/var1_star" + i + ".png"));
            planets.Add(new Texture("resources/gfx/background/deco1.png"));
        }

        public static Animation GetShipAnimation(ShipsAnimations whichone)
        {
            Animation value = noanim;
            switch (whichone)
            {
                case ShipsAnimations.ElCapitan:
                    value = new Animation("ElCapitanAnim", 0, elcapitan, false, 0, true);
                    break;

                default:
                    break;
            }
            return value;
        }

        public static Animation GetPropellerAnimation(ShipPropeller whichone)
        {
            Animation value = noanim;

            switch (whichone)
            {
                case ShipPropeller.Red:
                    value = new Animation("Propellers", 0.03f, propellersset1);
                    break;

                default:
                    break;
            }

            return value;
        }

        public static Animation GetShieldAnimation(ShieldColor whichcolor)
        {
            Animation value = noanim;

            switch (whichcolor)
            {
                case ShieldColor.Green:
                    value = new Animation("GreenShield", 0.03f, shieldgreen, true, 3, false, false);
                    break;
                case ShieldColor.Red:
                    value = new Animation("RedShield", 0.03f, shieldred, true, 1, false, false);
                    break;
                case ShieldColor.Blue:
                    break;
                case ShieldColor.White:
                    break;
                default:
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
