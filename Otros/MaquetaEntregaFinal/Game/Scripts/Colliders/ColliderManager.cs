using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
   public class ColliderManager
    {
        public static List<Collider> Colliders { get; } = new List<Collider>();
        public static void ClearColliders()
        {
            for (int i = Colliders.Count - 1; i >= 0; i--)
            {
                Colliders.RemoveAt(i);
            }
        }

        public static void AddCollider(Collider collider)
        {
            if (Colliders.Contains(collider)) return;
            Colliders.Add(collider);
        }

        public static void RemoveCollider(Collider collider)
        {
            if (!Colliders.Contains(collider)) return;
            Colliders.Remove(collider);
        }
    }
}

