﻿using System;

namespace Game
{
    public class fyPoolDay
    {
        public bool DBG = false;
        private static fyPoolDay pool;
        public static fyPoolDay Pool => pool ?? (pool = new fyPoolDay());

        private PoolGeneric<PoolProyectile> pProyectiles = new PoolGeneric<PoolProyectile>();  // Proyectiles pool
        private PoolGeneric<pOrbWeaver> pOrbWeaver = new PoolGeneric<pOrbWeaver>();            // Orb Weaver balls pool
        private PoolGeneric<bOrbWeaver> bOrbWeaver = new PoolGeneric<bOrbWeaver>();            // Orb Weaver bomb pool
        private PoolGeneric<Item> pItem = new PoolGeneric<Item>();                             // Items pool
        private PoolGeneric<GenericEffect> pEffects = new PoolGeneric<GenericEffect>();        // Effects pool

        private PoolGeneric<Player> pPlayer = new PoolGeneric<Player>();            // Player pool
        private PoolGeneric<Mosquitoe> pMosquitoe = new PoolGeneric<Mosquitoe>();   // Mosquitoe pool
        private PoolGeneric<Slider> pSlider = new PoolGeneric<Slider>();            // Slider pool
        private PoolGeneric<Tremor> pTremor = new PoolGeneric<Tremor>();            // Tremor pool

        public PoolProyectile CreateProyectile(string owner, int objectID)
        {
            var proyectile = pProyectiles.GetOrCreate(owner);

            if (proyectile.Value == null)
            {
                proyectile.Value = new PoolProyectile(owner, objectID);
                proyectile.Value.OnSleep += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} durmiendo...", proyectile.GetHashCode());
                    proyectile.Value.SetActive(false);
                    pProyectiles.InUseToAvailable(proyectile);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} despertando...", proyectile.GetHashCode());
            proyectile.Value.SetActive(true);
            return proyectile.Value;
        }
        public pOrbWeaver CreateOrbWeaverProyectile(string owner, int objectID)
        {
            var proyectile = pOrbWeaver.GetOrCreate(owner);

            if (proyectile.Value == null)
            {
                proyectile.Value = new pOrbWeaver(owner, objectID);
                proyectile.Value.OnSleep += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} durmiendo...", proyectile.GetHashCode());
                    proyectile.Value.SetActive(false);
                    pOrbWeaver.InUseToAvailable(proyectile);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} despertando...", proyectile.GetHashCode());
            proyectile.Value.SetActive(true);
            return proyectile.Value;

        }
        public bOrbWeaver CreateOrbWeaverBomb(string owner, int objectID)
        {
            var bomb = bOrbWeaver.GetOrCreate(owner);

            if (bomb.Value == null)
            {
                bomb.Value = new bOrbWeaver(owner, objectID);
                bomb.Value.OnSleep += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} durmiendo...", bomb.GetHashCode());
                    bomb.Value.SetActive(false);
                    bOrbWeaver.InUseToAvailable(bomb);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} despertando...", bomb.GetHashCode());
            bomb.Value.SetActive(true);
            return bomb.Value;
        }
        public Item CreateItem(string owner, int objectID)
        {
            var item = pItem.GetOrCreate(owner);

            if (item.Value == null)
            {
                item.Value = new Item("World", objectID);
                item.Value.OnSleep += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} durmiendo...", item.GetHashCode());
                    item.Value.SetActive(false);
                    pItem.InUseToAvailable(item);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} despertando...", item.GetHashCode());
            item.Value.SetActive(true);
            return item.Value;
        }
        public GenericEffect CreateEffect(EffectsAnimations type)
        {
            var effect = pEffects.GetOrCreate(type.ToString());

            if (effect.Value == null)
            {
                effect.Value = new GenericEffect(type);
                effect.Value.OnEffectFinished += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Efecto con HASH:{0} durmiendo...", effect.GetHashCode());
                    pEffects.InUseToAvailable(effect);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Efecto con HASH:{0} despertando...", effect.GetHashCode());
            return effect.Value;
        }

        public Player CreatePlayer(int newobjID, string owner = "Player")
        {
            var player = pPlayer.GetOrCreate(owner);
            
            if (player.Value == null)
            {
                player.Value = new Player(newobjID, owner);
                player.Value.OnPlayerDeath += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Jugador con HASH:{0} durmiendo...", player.GetHashCode());
                    player.Value.SetActive(false);
                    pPlayer.InUseToAvailable(player);
                };
            }

            if (DBG) Console.WriteLine("Pool -> Jugador con HASH:{0} despertando...", player.GetHashCode());
            player.Value.SetActive(true);
            return player.Value;
        }
        public Mosquitoe CreateMosquitoe(int newObjectID, string owner = "Enemy")
        {
            var mosquitoe = pMosquitoe.GetOrCreate(owner);

            if (mosquitoe.Value == null)
            {
                mosquitoe.Value = new Mosquitoe(newObjectID, owner);
                mosquitoe.Value.OnEnemyDeath += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Mosquitoe con HASH:{0} durmiendo...", mosquitoe.GetHashCode());
                    mosquitoe.Value.SetActive(false);
                    pMosquitoe.InUseToAvailable(mosquitoe);
                };
            }
            if (DBG) Console.WriteLine("Pool -> Mosquitoe con HASH:{0} despertando...", mosquitoe.GetHashCode());
            mosquitoe.Value.SetActive(true);
            return mosquitoe.Value;
        }
        public Slider CreateSlider(int newObjectID, string owner = "Enemy")
        {
            var slider = pSlider.GetOrCreate(owner);

            if (slider.Value == null)
            {
                slider.Value = new Slider(newObjectID, owner);
                slider.Value.OnEnemyDeath += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Slider con HASH:{0} durmiendo...", slider.GetHashCode());
                    slider.Value.SetActive(false);
                };
            }
            if (DBG) Console.WriteLine("Pool -> Mosquitoe con HASH:{0} despertando...", slider.GetHashCode());
            slider.Value.SetActive(true);
            return slider.Value;
        }
        public Tremor CreateTremor(int newObjectID, string owner = "Enemy")
        {
            var tremor = pTremor.GetOrCreate(owner);

            if (tremor.Value == null)
            {
                tremor.Value = new Tremor(newObjectID, owner);
                tremor.Value.OnEnemyDeath += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Tremor con HASH:{0} durmiendo...", tremor.GetHashCode());
                    tremor.Value.SetActive(true);
                };
            }
            if (DBG) Console.WriteLine("Pool -> Tremor con HASH:{0} despertando...", tremor.GetHashCode());
            tremor.Value.SetActive(true);
            return tremor.Value;
        }
    }
}
