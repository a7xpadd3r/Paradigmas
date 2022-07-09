using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class pOrbWeaver : GameObject
    {
        // Basic
        private string owner;
        private int objectid;
        private int iterations = 3;
        private ProyectileData pdata = ProyectileProperties.OrbWeaver;

        // Orb Weaver stuff
        private List<Collider> ignorelist = new List<Collider>();
        public Action OnSleep;

        public pOrbWeaver(string newOwner, int newID)
        {
            this.objectOwner = newOwner;
            this.objectID = newID;
        }

        public void Awake(Vector2 spawnPosition)
        {
            this.objectid = this.objectID;
            this.objectOwner = "Player";
            this.objectTag = "Proyectile";
            this.objectAnimation = Textures.GetProyectileAnimation(Proyectile.OrbWeaver);
            this.objectTransform = new Transform(spawnPosition);
            this.objectCollider = new Collider(pdata.Damage, this.objectOwner, this.objectTag, spawnPosition, this.objectAnimation.TextureSize, this.pdata.colliderVectors, this.objectID);
            this.objectCollider.OnCollision += OnHit;
            this.objectActive = true;
            this.iterations = 3;
            mGameObject.AddGameObject(this);
        }

        public void SetActive(bool newStatus) { this.objectActive = newStatus; }

        public override void Update()
        {     
            this.objectAnimation.Update();
            this.objectCollider.UpdateOwnerPosition(this.Position);
            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.Position, new Vector2(1.5f, 1.5f));
            this.objectCollider.CheckForCollisions();

            float posY = this.Position.Y;
            posY -= this.pdata.MaxSpeed * Program.GetDeltaTime;
            this.objectTransform.UpdatePosition(new Vector2(this.Position.X, posY));
            if (posY < -100 || posY > 1180) Sleep();
        }

        private void OnHit(Collider instigator)
        {
            if (this.ignorelist.Contains(instigator) || instigator.ColliderOwner == "Player" && instigator.ColliderOwner != "World") return;

            this.ignorelist.Add(instigator);
            int newID = mGameObject.GenerateObjectID();
            var bOrbWeaverB = fyPoolDay.Pool.CreateOrbWeaverBomb("Player", newID);
            bOrbWeaverB.Awake(this.Position);
            this.iterations--;

            if (this.iterations < 0) { Sleep(); }
        }

        public override void Sleep()
        {
            this.objectCollider.OnCollision -= OnHit;
            this.ignorelist.Clear();
            mGameObject.RemoveGameObject(this);
            this.objectCollider = null;
            OnSleep?.Invoke();
        }
    }
}
