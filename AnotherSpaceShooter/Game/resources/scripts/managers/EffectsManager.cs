

using System;
using System.Collections.Generic;

namespace Game
{
    public class EffectsManager
    {
        public static bool debug = true;
        private static protected List<GenericEffect> CurrentEffects { get; } = new List<GenericEffect>();
        public List<GenericEffect> GetAllEffects => CurrentEffects;

        public void Update()
        {
            for (int i = 0; i < CurrentEffects.Count; i++)
            {
                if (CurrentEffects[i].isActive)
                {
                    GenericEffect cEffect = CurrentEffects[i];
                    cEffect.UpdateRender();
                }
            }
        }

        public static void AddEffect(GenericEffect newEffect)
        {
            // Check if exists
            if (CurrentEffects.Contains(newEffect)) return;
            CurrentEffects.Add(newEffect);
            if (debug) Console.WriteLine("EffectsManager --> '{0}' agregado en la posición {1}.", newEffect.name, CurrentEffects.IndexOf(newEffect));
        }

        public static void RemoveEffect(GenericEffect removeThis)
        {
            if (!CurrentEffects.Contains(removeThis)) return;
            if (debug) Console.WriteLine("EffectsManager --> '{0}' removido de la posición {1}.", removeThis.name, CurrentEffects.IndexOf(removeThis));
            CurrentEffects.Remove(removeThis);
        }

        public static void WipeEffects()
        {
            CurrentEffects.Clear();
            if (debug) Console.WriteLine("EffectsManager --> Array vaciado.");
        }

    }
}
