using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class mEffects
    {
        public static bool DBG = false;
        private static protected List<GenericEffect> CurrentEffects { get; } = new List<GenericEffect>();
        public List<GenericEffect> GetAllEffects => CurrentEffects;

        public static void Update(Vector2 playerPosition)
        {
            for (int i = 0; i < CurrentEffects.Count; i++)
            {
                var effect = CurrentEffects[i];
                if (!effect.Active) return;
                effect.Update(playerPosition);
            }
        }

        public static void AddEffect(GenericEffect newEffect)
        {
            if (CurrentEffects.Contains(newEffect)) return;
            CurrentEffects.Add(newEffect);
        }

        public static void RemoveEffect(GenericEffect whichone)
        {
            if (!CurrentEffects.Contains(whichone)) return;
            CurrentEffects.Remove(whichone);
        }

        public static void WipeEffects()
        {
            CurrentEffects.Clear();
            Console.WriteLine("Manager -> Lista de efectos vaciada.");
        }
    }
}
