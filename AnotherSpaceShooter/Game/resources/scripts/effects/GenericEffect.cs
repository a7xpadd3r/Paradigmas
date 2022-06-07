using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GenericEffect
    {
        public Animation anim;
        public float speed = 200f;
        private float posX;
        private float posY;
        public Vector2 renderPos => new Vector2(posX, posY);
        public Vector2 size;
        public Vector2 offset;
        public float rotation;
        public bool isActive = true;
        public string name = "none";
        private Vector2 PlayerPos => new Vector2(Player.Position.X, Player.Position.Y);

        // Timer for X/Y position
        private float playerX = 0;
        private float newPlayerX = 0;
        private float playerY = 0;
        private float newPlayerY = 0;
        private float time = 0.1f;
        private float currentTime = 0.1f;

        private float fullsize => (size.X + size.Y) / 2;

        public GenericEffect(Vector2 newRenderPos, Vector2 newSize, Vector2 newOffset, float newRotation,  string newName, List<Texture> newTextures, float newSpeed, bool newLoop = true, bool newManualFrames = false, bool newPlayOnStart = true)
        {
            this.anim = new Animation(newName, newSpeed, newTextures, newLoop, newManualFrames);
            this.posX = newRenderPos.X;
            this.posY = newRenderPos.Y;
            this.size = newSize;
            this.offset = newOffset;
            this.rotation = newRotation;
            this.name = newName;
            anim.OnAnimationFinished += RemoveEffect;
            EffectsManager.AddEffect(this);
        }

        public void UpdateRender()
        {
            if (anim != null)
            {
                posY += speed * Program.GetDeltaTime();

                Engine.Draw(anim.CurrentTexture, renderPos.X, renderPos.Y, size.X, size.Y, rotation, offset.X, offset.Y);
                anim.Update();

                if (currentTime <= time)
                {
                    playerX = PlayerPos.X;
                    playerY = PlayerPos.Y;
                    currentTime += Program.GetDeltaTime();
                }

                if (currentTime > time) 
                {
                    newPlayerY = PlayerPos.Y;
                    newPlayerX = PlayerPos.X;
                    currentTime = 0;
                }

                // Update position following the player movements
                if (newPlayerX < playerX) posX -= (speed * fullsize) * Program.GetDeltaTime();
                else if (newPlayerX > playerX) posX += (speed * fullsize) * Program.GetDeltaTime();
                if (newPlayerY < playerY) posY -= (speed * fullsize) / 25 * Program.GetDeltaTime();
                else if (newPlayerY > playerY) posY += (speed * fullsize) * Program.GetDeltaTime();

            }
        }

        private void RemoveEffect()
        {
            anim.OnAnimationFinished -= RemoveEffect;
            EffectsManager.RemoveEffect(this);
        }
    
    }
}
