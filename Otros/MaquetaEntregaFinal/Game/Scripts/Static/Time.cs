using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Time
    {
        public static float DeltaTime { get; private set; } 
        private static float lastFrameTime;
        private static DateTime starDate;

        public static void Awake()
        {
            starDate = DateTime.Now;
        }

        public static void Update()
        {
            CalculateDeltaTime();
        }

        private static void CalculateDeltaTime()
        {
            float currentTime = (float)(DateTime.Now - starDate).TotalSeconds;
            DeltaTime = currentTime - lastFrameTime;
            lastFrameTime = currentTime;
        }
    }
}