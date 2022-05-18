using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyData
    {
        //Campos
        private TransformData transformData;
        private int maxHealth;
        private int currentHealth;
        private float speed;
        private float shootCooldwon;


        //Propiedades
        public TransformData TransformData { get => transformData; set => transformData = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public float Speed { get => speed; set => speed = value; }
        public float ShootCooldwon { get => shootCooldwon; set => shootCooldwon = value; }
    }
}
