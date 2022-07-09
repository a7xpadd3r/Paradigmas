using System;
using System.Numerics;

namespace Game
{
    public class bOrbWeaver : GameObject
    {
        // Basic
        private string owner;
        private int objectid;
        private ProyectileData pdata = ProyectileProperties.OrbWeaverBomb;

        public Action OnSleep;

        public bOrbWeaver(string newOwner, int newID)
        {
            this.objectOwner = newOwner;
            this.objectID = newID;
        }

        public void Awake(Vector2 spawnPosition)
        {
            this.objectid = this.objectID;
            this.objectOwner = "Player";
            this.objectTag = "Proyectile";
            this.objectAnimation = Textures.GetEffectAnimation(EffectsAnimations.OrbWeaverImpact);
            this.objectTransform = new Transform(spawnPosition);
            this.objectCollider = new Collider(pdata.Damage, this.objectOwner, this.objectTag, spawnPosition, this.objectAnimation.TextureSize, this.pdata.colliderVectors, this.objectID);
            this.objectCollider.OnCollision += OnHit;
            this.objectAnimation.OnAnimationFinished += Sleep;

            mGameObject.AddGameObject(this);
        }

        public override void Update()
        {
            this.objectCollider.UpdateOwnerPosition(this.Position);
            this.objectCollider.CheckForCollisions();
            this.objectAnimation.Update();
            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.Position - new Vector2(60.5f,64), new Vector2(2,2), 0);
        }

        private void OnHit(Collider instigator) { }

        public override void Sleep()
        {
            mGameObject.RemoveGameObject(this);
            this.objectCollider.OnCollision -= OnHit;
            this.objectCollider = null;
            
            OnSleep?.Invoke();
        }

        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
    }
}
