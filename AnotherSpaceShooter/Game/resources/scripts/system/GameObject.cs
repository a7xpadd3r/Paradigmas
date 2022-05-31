﻿using System.Numerics;
using System;

namespace Game
{
    public abstract class GameObject
    {
        public Collider objectCollider = null;
        public bool isActive = true;
        public int life = 1;
        public string Owner => owner;
        public string owner = "null";
        public Vector2 spawnPosition = new Vector2(0,0);
        public Action<int> AnyDamage;

        public void Awake()
        {
            GameObjectManager.AddGameObject(this);
            objectCollider.OnCollision += OnCollision;
            AnyDamage += Damage;
        }

        public void BeginPlay()
        {
        }

        public virtual void Render()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void OnCollision(Collider instigator)
        {
            Console.WriteLine("'{0}' -> Colisión por parte de '{1}', instigado por '{2}'", Owner, instigator, instigator.GetOwner());
        }
        
        public virtual void Damage(int amount)
        {
            life -= amount;
            if (life <= 0) Destroy();
        }

        public virtual void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);
        }

        private void UpdateOwner(string newOwner)
        {
            owner = newOwner;
        }
    }
}