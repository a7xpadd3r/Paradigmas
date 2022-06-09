using System;
using System.Collections.Generic;

namespace Game
{
    public class CollisionManager
    {
        public static bool debug = false;
        private protected static List<Collider> CurrentColliders { get; } = new List<Collider>();
        public static List<Collider> GetAllColliders => CurrentColliders;

        public static void Update()
        {
            for (int i = 0; i < CurrentColliders.Count; i++)
            {
                if (CurrentColliders[i].active)
                {
                    Collider cCollider = CurrentColliders[i];
                    if (cCollider.active)
                        cCollider.CheckForCollisions();
                }
            }


            for (int i = 0; i < COLLV2.Count; i++)
            {
                if (COLLV2[i].active)
                {
                    ColliderRework col = COLLV2[i];
                    if (col.active) col.CheckCollision();
                }
            }
        }

        public static void AddCollider(Collider newCollider)
        {
            // Check if exists
            if (CurrentColliders.Contains(newCollider)) return;
            CurrentColliders.Add(newCollider);
            if (debug) Console.WriteLine("CollisionsManager --> '{0}' agregado en la posición {1}.", newCollider.owner, CurrentColliders.IndexOf(newCollider));
        }

        public static void RemoveCollider(Collider removeThis)
        {
            // Check if exists
            if (!CurrentColliders.Contains(removeThis)) return;
            if (debug) Console.WriteLine("CollisionsManager --> '{0}' removido en la posición {1}.", removeThis.owner, CurrentColliders.IndexOf(removeThis));
            CurrentColliders.Remove(removeThis);
        }

        public static void WipeColliders()
        {
            CurrentColliders.Clear();
        }





        private protected static List<ColliderRework> COLLV2 { get; } = new List<ColliderRework>();
        public static List<ColliderRework> NewColliders => COLLV2;

        public static void AddNew(ColliderRework nColl)
        {
            if (!COLLV2.Contains(nColl)) COLLV2.Add(nColl);
        }



    }
}
