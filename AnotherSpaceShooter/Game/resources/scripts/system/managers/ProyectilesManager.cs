using System;
using System.Collections.Generic;

namespace Game
{
    public class ProyectilesManager
    {
        public static bool debug = false;
        private protected static List<Proyectile> CurrentProyectiles { get; } = new List<Proyectile>();
        public static List<Proyectile> GetAllProyectiles => CurrentProyectiles;

        public static void Update()
        {
            for (int i = 0; i < CurrentProyectiles.Count; i++)
            {
                if (CurrentProyectiles[i].Active)
                {
                    Proyectile cProyectile = CurrentProyectiles[i];
                    if (cProyectile.Active)
                        cProyectile.Update();
                }
                else
                    CurrentProyectiles[i].AfterHit();
            }
        }

        public static void AddProyectile(Proyectile newProyectile)
        {
            // Check if exists
            if (CurrentProyectiles.Contains(newProyectile)) return;
            CurrentProyectiles.Add(newProyectile);
            if (debug) Console.WriteLine("CurrentProyectiles --> '{0}' agregado en la posición {1}.", newProyectile.owner, CurrentProyectiles.IndexOf(newProyectile));
        }

        public static void RemoveProyectile(Proyectile removeThis)
        {
            // Check if exists
            if (!CurrentProyectiles.Contains(removeThis)) return;
            if (debug) Console.WriteLine("CurrentProyectiles --> '{0}' removido en la posición {1}.", removeThis.owner, CurrentProyectiles.IndexOf(removeThis));
            CurrentProyectiles.Remove(removeThis);
        }

        public static void WipeProyectiles()
        {
            CurrentProyectiles.Clear();
        }
    }
}
