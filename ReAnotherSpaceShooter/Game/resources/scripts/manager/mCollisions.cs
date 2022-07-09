using System;
using System.Collections.Generic;

namespace Game
{
    public class mCollisions
    {
        public static bool debug = false;
        private protected static List<Collider> CurrentColliders { get; } = new List<Collider>();
        public static List<Collider> GetAllColliders => CurrentColliders;

        public static void Update()
        {
            for (int i = 0; i < CurrentColliders.Count; i++)
            {
                if (CurrentColliders[i] != null)
                {
                    Collider cCollider = CurrentColliders[i];
                    cCollider.CheckForCollisions();
                }
            }
        }

        public static void AddCollider(Collider newCollider)
        {
            // Check if exists
            if (CurrentColliders.Contains(newCollider)) return;
            CurrentColliders.Add(newCollider);
            if (debug) Console.WriteLine("CollisionsManager --> '{0}' agregado en la posición {1}.", newCollider.ColliderOwner, CurrentColliders.IndexOf(newCollider));
        }

        public static void RemoveCollider(Collider removeThis)
        {
            // Check if exists
            if (!CurrentColliders.Contains(removeThis)) return;
            if (debug) Console.WriteLine("CollisionsManager --> '{0}' removido en la posición {1}.", removeThis.ColliderOwner, CurrentColliders.IndexOf(removeThis));
            CurrentColliders.Remove(removeThis);
        }

        public static void WipeColliders()
        {
            CurrentColliders.Clear();
        }
    }
}
