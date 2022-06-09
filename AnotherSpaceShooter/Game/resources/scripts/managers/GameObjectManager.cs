using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class GameObjectManager
    {
        public static bool debug = true;
        private static Random random = new Random();
        private static protected List<GameObject> CurrentGameObjects { get; } = new List<GameObject>();
        private static protected List<Item> CurrentItems { get; } = new List<Item>();
        public static List<GameObject> GetAllGameObjects => CurrentGameObjects;
        public static List<Item> GetAllItems => CurrentItems;

        public void Update()
        {
            for (int i = 0; i < CurrentGameObjects.Count; i++)
            {
                if (CurrentGameObjects[i].active)
                {
                    GameObject cGameObject = CurrentGameObjects[i];
                    if (cGameObject.active) 
                    { 
                        cGameObject.Update();
                        cGameObject.Render();
                        if (cGameObject.owner == "Player" && cGameObject.tag == "Ship") StarsManager.PlayerPos = cGameObject.Position;
                    }
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
                temporalIDs.Add(item.Id);
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
            if (CurrentGameObjects.Contains(newObject)) return;
            CurrentGameObjects.Add(newObject);
            if (newObject.collider != null) CollisionManager.AddCollider(newObject.collider);
            if (debug) Console.WriteLine("GameObjectsManager --> Nuevo objeto: '{0}' (dueño '{1}') agregado en la posición {2} con el ID {3}.", newObject.tag, newObject.owner, CurrentGameObjects.IndexOf(newObject), newObject.Id);
        }

        public static void RemoveGameObject(GameObject removeThis)
        {
            // Check if exists
            if (!CurrentGameObjects.Contains(removeThis)) return;
            if (debug) Console.WriteLine("GameObjectsManager --> Objeto eliminado: '{0}' (dueño '{1}' removido de la posición {2} con el ID {3}.", removeThis.tag, removeThis.owner, CurrentGameObjects.IndexOf(removeThis), removeThis.Id);
            if (removeThis.collider != null) CollisionManager.RemoveCollider(removeThis.collider);
            removeThis.active = false;
            CurrentGameObjects.Remove(removeThis);
        }

        public void WipeGameObjects()
        {
            CurrentGameObjects.Clear();
            CurrentItems.Clear();
            CollisionManager.WipeColliders();
            EffectsManager.WipeEffects();
            if (debug) Console.WriteLine("GameObjectsManager --> Todo limpiado.");
        }

        public static void AddItem(Item newItem)
        {
            CurrentItems.Add(newItem);
            Console.WriteLine("GameObjectsManager --> Nuevo ítem agregado del tipo '{0}' en la ubicación {1} con el ID {2}.", newItem.GetType, CurrentItems.IndexOf(newItem), newItem.Id);
        }

        public static Item GrabItem(Collider reference)
        {
            int refID = reference.Id;
            Item value = null;
            if (GetAllItems.Count > 0 )
            {
                List<int> tempGameObjectsIDs = new List<int>();
                List<int> tempItemsIDs = new List<int>();

                // Add every ID to a temporal list
                foreach (GameObject item in GetAllGameObjects)
                {
                    tempGameObjectsIDs.Add(item.Id);
                }

                foreach (Item item in GetAllItems)
                {
                    tempItemsIDs.Add(item.Id);
                }

                if (tempGameObjectsIDs.Contains(refID) && tempItemsIDs.Contains(refID))
                {
                    for (int i = 0; i < GetAllItems.Count; i++)
                    {
                        if (GetAllItems[i].Id == refID) value = GetAllItems[i];
                    }
                }
            }
            return value;
        }

        public static void RemoveItem(Item removeItem)
        {
            if (!CurrentItems.Contains(removeItem)) return;
            Console.WriteLine("GameObjectsManager + ItemsObjects) --> Removiendo ítem del tipo '{0}' de la ubicación {1} con el ID {2}.", removeItem.GetType, CurrentItems.IndexOf(removeItem), removeItem.Id);
            removeItem.Destroy();
            CurrentItems.Remove(removeItem);
        }
    }
}
