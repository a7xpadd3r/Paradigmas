using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class GameObjectManager
    {
        public static List<GameObject> ActiveGameObjects { get; } = new List<GameObject>();

        public static void ClearGameObjects() 
        {
            for (int i  =  ActiveGameObjects.Count - 1; i >= 0; i--)
            {
                GameObject gameObject = ActiveGameObjects[i];
                if (!gameObject.IsDontDestroyOnLoadEnabled)
                {
                    ActiveGameObjects.RemoveAt(i);
                }
            }
        }
        public static void AddGameObject(GameObject gameObject)
        {
            if (ActiveGameObjects.Contains(gameObject)) return;
            ActiveGameObjects.Add(gameObject);
        }
        public static void RemoveGameObject(GameObject gameObject)
        {
            if (!ActiveGameObjects.Contains(gameObject)) return;
            ActiveGameObjects.Remove(gameObject);
        }
        public static void Update()
        {
            for (int i = 0; i < ActiveGameObjects.Count; i++)
            {
                GameObject item = ActiveGameObjects[i];
                if (item.IsActive)
                {
                    item.Update();
                }
            }
        }
        public static void Render()
        {
            for (int i = 0; i < ActiveGameObjects.Count; i++)
            {
                GameObject item = ActiveGameObjects[i];
                if (item.IsActive)
                {
                    item.Render();
                }
            }
        }
    }
}

