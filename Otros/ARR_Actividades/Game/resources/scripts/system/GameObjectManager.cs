using System;
using System.Collections.Generic;

namespace Game
{
    public class GameObjectManager
    {
        public static List<GameObject> CurrentGameObjects { get; } = new List<GameObject>();

        public static void Update()
        {
            for (int i = 0; i < CurrentGameObjects.Count; i++)
            {
                // Check first if is active
                if (!CurrentGameObjects[i].objIsActive) return;

                CurrentGameObjects[i].Update();
                CurrentGameObjects[i].Render();
            }
        }

        public static void AddGameObject(GameObject newObject)
        {
            // Check if exists
            if (CurrentGameObjects.Contains(newObject)) return;
            CurrentGameObjects.Add(newObject);
            //Console.WriteLine("GameObjectManager --> Nuevo objeto con el tag '{0}' agregado: {1} en la ubicación {2}.", newObject.objTag, newObject, CurrentGameObjects.IndexOf(newObject));
        }

        public static void RemoveGameObject(GameObject removeMe)
        {
            // Check if exits
            if (!CurrentGameObjects.Contains(removeMe)) return;
            //Console.WriteLine("GameObjectManager --> Se ha removido el objeto '{0}' con el tag '{1}' en la ubicación {2}.", removeMe, removeMe.objTag,CurrentGameObjects.IndexOf(removeMe));
            CurrentGameObjects.Remove(removeMe);
        }

        public static void ClearGameObjects()
        {
            CurrentGameObjects.Clear();
            Console.WriteLine("Se la limpiado el contenedor de GameObjects.");
        }
    }
}
