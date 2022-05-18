using System;
using System.Collections.Generic;

namespace Game
{
    public class CollisionManager
    {
        public static List<Collider> CurrentColliders { get; } = new List<Collider>();

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
        }

        public static void AddCollider(Collider newCollider)
        {
            // Check if exists
            if (CurrentColliders.Contains(newCollider)) return;
            CurrentColliders.Add(newCollider);
            //Console.WriteLine("CollisionsManager --> nueva colisión '{0}' agregado en la posición {1}.", newCollider, CurrentColliders.IndexOf(newCollider));
        }

        public static void RemoveCollider(Collider removeThis)
        {
            // Check if exists
            if (!CurrentColliders.Contains(removeThis)) return;
            //Console.WriteLine("CollisionsManager --> colisión '{0}' removido en la posición {1}.", removeThis, CurrentColliders.IndexOf(removeThis));
            CurrentColliders.Remove(removeThis);
        }

        public static void WipeColliders()
        {
            CurrentColliders.Clear();
        }
    }
}
