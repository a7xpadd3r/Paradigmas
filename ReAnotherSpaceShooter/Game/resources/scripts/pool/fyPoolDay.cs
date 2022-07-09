using System;

namespace Game
{
    public class fyPoolDay
    {
        public bool DBG = false;
        private static fyPoolDay pool;
        public static fyPoolDay Pool => pool ?? (pool = new fyPoolDay());

        private PoolGeneric<PoolProyectile> pProyectiles = new PoolGeneric<PoolProyectile>();     // Proyectiles pool
        private PoolGeneric<pOrbWeaver> pOrbWeaver = new PoolGeneric<pOrbWeaver>();               // Orb Weaver balls pool
        private PoolGeneric<bOrbWeaver> bOrbWeaver = new PoolGeneric<bOrbWeaver>();               // Orb Weaver bomb pool
        private PoolGeneric<Item> nItem = new PoolGeneric<Item>();                                 // Items pool
        private PoolGeneric<GenericEffect> pEffects = new PoolGeneric<GenericEffect>();           // Effects pool

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
            var item = nItem.GetOrCreate(owner);

            if (item.Value == null)
            {
                item.Value = new Item("World", objectID);
                item.Value.OnSleep += () =>
                {
                    if (DBG) Console.WriteLine("Pool -> Objecto con HASH:{0} durmiendo...", item.GetHashCode());
                    item.Value.SetActive(false);
                    nItem.InUseToAvailable(item);
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
    }
}
