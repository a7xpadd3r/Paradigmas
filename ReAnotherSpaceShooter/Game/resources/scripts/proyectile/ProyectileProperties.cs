﻿using System.Numerics;

namespace Game
{
    public class ProyectileProperties
    {
        // Usage:   RECOIL - SPEED - DAMAGE - ANIMATION - (COLLIDER OFFSET - COLLIDER SIZE) - (HIT EFFECT OFFSET - EFFECT SIZE)
        public static readonly ProyectileData BlueRail = new ProyectileData(0.50f, 1500, 1.2f, Textures.GetProyectileAnimation(Proyectile.BlueRail), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(3,10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData RedDiamond = new ProyectileData(0.65f, 1000, 2f, Textures.GetProyectileAnimation(Proyectile.RedTrail), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(10, 10), new Vector2(2.5f, 2.5f)));
        public static readonly ProyectileData RedDiamondBall = new ProyectileData(0.65f, 800, 1.5f, Textures.GetProyectileAnimation(Proyectile.RedTrailBall), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(10, 10), new Vector2(2.3f, 2.3f)));
        public static readonly ProyectileData GreenCrast = new ProyectileData(0.3f, 2300, 0.65f, Textures.GetProyectileAnimation(Proyectile.GreenCrast), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData HeatTrail = new ProyectileData(0.3f, 400, 1, Textures.GetProyectileAnimation(Proyectile.HeatTrail), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData OrbWeaver = new ProyectileData(0, 300, 3.5f, Textures.GetProyectileAnimation(Proyectile.OrbWeaver), new DoubleVector2(new Vector2(-16, -18), new Vector2(64, 64)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData OrbWeaverBomb = new ProyectileData(0, 0, 4.5f, Textures.GetEffectAnimation(EffectsAnimations.OrbWeaverImpact), new DoubleVector2(new Vector2(48, 55), new Vector2(230, 230)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData Gamma = new ProyectileData(0, 0, 4.5f, Textures.GetProyectileAnimation(Proyectile.GammaBeam), new DoubleVector2(new Vector2(13, 2000), new Vector2(0, 1000)));

        public static readonly ProyectileData EnemyBlueRail = new ProyectileData(0.75f, 750, 1, Textures.GetProyectileAnimation(Proyectile.Enemy1), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
        public static readonly ProyectileData EnemyRedDiamond = new ProyectileData(1, 350, 1.7f, Textures.GetProyectileAnimation(Proyectile.Enemy2), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(10, 10), new Vector2(2.5f, 2.5f)));
        public static readonly ProyectileData EnemyGreenCrast = new ProyectileData(0.55f, 1200, 0.5f, Textures.GetProyectileAnimation(Proyectile.Enemy3), new DoubleVector2(new Vector2(-10.95f, -8), new Vector2(10, 20)), new DoubleVector2(new Vector2(3, 10), new Vector2(1.5f, 1.5f)));
    }
}
