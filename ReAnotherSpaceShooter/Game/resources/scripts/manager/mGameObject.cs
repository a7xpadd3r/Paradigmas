using System;
using System.Collections.Generic;

namespace Game
{
    public class mGameObject
    {
        public static bool DBG = true;

        private static Random random = new Random();
        private static protected List<GameObject> CurrentGameObjects { get; } = new List<GameObject>();
        //private static protected List<Item> CurrentItems { get; } = new List<Item>();
        public static List<GameObject> GetAllGameObjects => CurrentGameObjects;
        //public static List<Item> GetAllItems => CurrentItems;
        
        public static void Update()
        {
            for (int i = 0; i < CurrentGameObjects.Count; i++)
            {
                if (CurrentGameObjects[i].Active)
                {
                    GameObject cGameObject = CurrentGameObjects[i];
                    if (cGameObject.Active) { cGameObject.Update(); }
                }
            }
        }

        public static int GenerateObjectID()
        {
            int value = 0;
            bool canReturnValue = false;
            List<int> temporalIDs = new List<int>();

            // Add every ID to a temporal list
            foreach (GameObject item in CurrentGameObjects)
            {
                temporalIDs.Add(item.ID);
            }

            // Generate a new one while there is a coincidence
            while (!canReturnValue)
            {
                value = random.Next(0, 9999);
                if (!temporalIDs.Contains(value)) canReturnValue = true;
            }
            return value;
        }
        
        public static void AddGameObject(GameObject newObject)
        {
            // Check if exists
            if (DBG) Console.WriteLine("Mánager+ Objecto '{0}' con ID '{1}' (dueño '{2}').", newObject.objectTag, newObject.ID, newObject.Owner);
            if (CurrentGameObjects.Contains(newObject)) return;
            CurrentGameObjects.Add(newObject);
            if (newObject.objectCollider != null) mCollisions.AddCollider(newObject.objectCollider);
        }

        public static void RemoveGameObject(GameObject reference)
        {
            if (!CurrentGameObjects.Contains(reference)) return;
            CurrentGameObjects.Remove(reference);
            if (reference.objectCollider != null) mCollisions.RemoveCollider(reference.Collider);
            if (DBG) Console.WriteLine("Mánager- Objecto '{0}' con ID '{1}' (dueño '{2}').", reference.objectTag, reference.ID, reference.Owner);
        }

        public static void WipeGameObjects()
        {
            CurrentGameObjects.Clear();
            mEffects.WipeEffects();
            if (DBG) Console.WriteLine("Mánager -> Limpiando juego...");
        }

        /*
        public static void AddItem(Item newItem)
        {
            CurrentItems.Add(newItem);
            Console.WriteLine("GameObjectsManager --> Nuevo ítem agregado del tipo '{0}' en la ubicación {1} con el ID {2}.", newItem.GetType, CurrentItems.IndexOf(newItem), newItem.id);
        }

        public static Item GrabItem(Collider reference)
        {
            int refID = reference.id;
            Item value = null;
            if (GetAllItems.Count > 0)
            {
                List<int> tempGameObjectsIDs = new List<int>();
                List<int> tempItemsIDs = new List<int>();

                // Add every ID to a temporal list
                foreach (GameObject item in GetAllGameObjects)
                {
                    tempGameObjectsIDs.Add(item.id);
                }

                foreach (Item item in GetAllItems)
                {
                    tempItemsIDs.Add(item.id);
                }

                if (tempGameObjectsIDs.Contains(refID) && tempItemsIDs.Contains(refID))
                {
                    for (int i = 0; i < GetAllItems.Count; i++)
                    {
                        if (GetAllItems[i].id == refID) value = GetAllItems[i];
                    }
                }
            }
            return value;
        }

        public static void RemoveItem(Item removeItem)
        {
            if (!CurrentItems.Contains(removeItem)) return;
            Console.WriteLine("GameObjectsManager + ItemsObjects) --> Removiendo ítem del tipo '{0}' de la ubicación {1} con el ID {2}.", removeItem.GetType, CurrentItems.IndexOf(removeItem), removeItem.id);
            removeItem.Destroy();
            CurrentItems.Remove(removeItem);
        }
    */
    }
}
