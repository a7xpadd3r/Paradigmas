using Game.resources.scripts.collision;
using System.Collections.Generic;

namespace Game
{
    public class CollisionsManager
    {
        public static List<Collider> colliders { get; } = new List<Collider>();

        public static void AddCollider(Collider thiscollider)
        {
            // Check if exists
            if (colliders.Contains(thiscollider)) return;
            colliders.Add(thiscollider);
        }

        public static void RemoveCollider(Collider thiscollioder)
        {
            if (!colliders.Contains(thiscollioder)) return;
            colliders.Remove(thiscollioder);
        }

        public static void ClearColliders()
        {
            colliders.Clear();
        }
    }
}
