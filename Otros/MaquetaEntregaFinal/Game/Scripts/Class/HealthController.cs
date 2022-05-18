using System;
using System.Collections.Generic;

namespace Game
{
    public class HealthController
    {
        //Campos
        public event Action OnDie;
        public event Action<int, int> OnGetDamage;
        public event Action<int, int> OnGetHeal;
        public event Action<int, int> OnGetCurrentLife;
        //Propiedades
        public int CurrentHealth { get; private set; }
        public int MaxLife { get; private set; }


        //Constructor
        public HealthController(int maxLife)
        {
            MaxLife = maxLife;
            CurrentHealth = maxLife;
        }


        //Metodos
        public void GetDamage(int damage)
        {
            CurrentHealth -= damage;

            OnGetDamage?.Invoke(CurrentHealth, damage);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDie?.Invoke();
            }
        }

        public void GetCurrentLife(int damage)
        {
            OnGetCurrentLife?.Invoke(this.CurrentHealth, damage);
        }

        public void GetCurrentLifeHeal(int heal)
        {
            OnGetCurrentLife?.Invoke(this.CurrentHealth, heal);
        }
        public void GetHeal(int heal)
        {
            CurrentHealth += heal;
       
            if (CurrentHealth > MaxLife)
            {
                CurrentHealth = MaxLife;
            }

            OnGetHeal?.Invoke(CurrentHealth, heal);
        }
        public void Kill()
        {
            CurrentHealth = 0;
            OnDie?.Invoke();
        }
    }
}

