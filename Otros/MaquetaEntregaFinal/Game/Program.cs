using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public class Program
    {
        public const int WIDTH_SCREEN = 1000;
        public const int HEIGHT_SCREEN = 600;

        private static SoundPlayer music;

        static void Main(string[] args)
        {
            Initialization();

            while (true)
            {
                Time.Update(); //Llamamos al Update de la clase Time que calcula el delta time dentro de un metodo. Y es necesario que este en el bucle
                Update();
                Render();
            }
        }
        private static void Initialization() //Es lo primero que se ejecuta
        {
           Engine.Initialize("Paradigmas de Programacion", WIDTH_SCREEN, HEIGHT_SCREEN);

           Time.Awake(); // Llamamos al Awake de la clase Time (ya que esto necesitamos que se ejecute primero)

           GameManager.Instance.Initialize();

           music = new SoundPlayer("Audio/Starfire (PC-98) - Station.wav");
           music.PlayLooping();
        }
        public static void Update()
        {

            GameManager.Instance.Update();
        }
        public static void Render()
        {
            Engine.Clear();
            GameManager.Instance.Render();
            Engine.Show();
        }
    }
}
