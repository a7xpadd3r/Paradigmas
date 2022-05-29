using System;
using System.Collections.Generic;

namespace Game
{
    public class GameObjectManager
    {
        public static bool debug = true;
        private static protected List<GameObject> CurrentGameObjects { get; } = new List<GameObject>();
        public List<GameObject> GetAllProyectiles => CurrentGameObjects;

        public void Update()
        {
            for (int i = 0; i < CurrentGameObjects.Count; i++)
            {
                if (CurrentGameObjects[i].isActive)
                {
                    GameObject cGameObject = CurrentGameObjects[i];
                    if (cGameObject.isActive) { cGameObject.Update(); cGameObject.Render(); }
                }
            }
        }

        public static void AddGameObject(GameObject newObject)
        {
            // Check if exists
            if (CurrentGameObjects.Contains(newObject)) return;
            CurrentGameObjects.Add(newObject);
            if (debug) Console.WriteLine("GameObjectsManager --> '{0}' agregado en la posición {1}.", newObject.Owner, CurrentGameObjects.IndexOf(newObject));
        }

        public void RemoveGameObject(GameObject removeThis)
        {
            // Check if exists
            if (!CurrentGameObjects.Contains(removeThis)) return;
            if (debug) Console.WriteLine("GameObjectsManager --> '{0}' removido en la posición {1}.", removeThis.Owner, CurrentGameObjects.IndexOf(removeThis));
        }

        public void WipeGameObjects()
        {
            CurrentGameObjects.Clear();
            if (debug) Console.WriteLine("GameObjectsManager --> Array wiped.");
        }
    }
}
