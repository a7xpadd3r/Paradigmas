using System;
using System.Numerics;

namespace Game
{
    public class wGamma : iWeapon
    {
        // Weapon stats
        private int ammo = 300;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;
        private bool drawbeam = false;
        private Animation animation = ProyectileProperties.Gamma.Animation;
        private Vector2 position = new Vector2(1,1);
        private Collider beamcollider;
        public WeaponTypes ThisType => WeaponTypes.Gamma;
        public int AddAmmoAmount => 450;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }
        public float AdditionalSpeed { get; set; }
        public iGetWeapon Owner { get; set; }

        public wGamma() 
        {
            this.beamcollider = new Collider(ProyectileProperties.Gamma.Damage, "Player", "Proyectile", position, ProyectileProperties.Gamma.colliderVectors.Position, ProyectileProperties.Gamma.colliderVectors.Scale, 0);
            this.recoilTime = ProyectileProperties.Gamma.Recoil;
            this.animation.OnAnimationFinished += StopBeam;
        }

        public void NewOwner(iGetWeapon owner) { Owner = owner; }

        public void AddAmmo() { ammo += AddAmmoAmount; }

        public void Fire(string owner, Vector2 currentPosition)
        {
            this.position = currentPosition;
            if (ammo > 0)
            {
                this.drawbeam = true;
                this.animation.ChangeFrame(0);
                ammo--;
            }
        }

        public void Update(float delta)
        {
            this.animation.Update();
            this.beamcollider.UpdateOwnerPosition(position);
            if (this.drawbeam)
            {
                this.beamcollider.CheckForCollisions();
                Renderer.Draw(this.animation.CurrentTexture, this.position, new Vector2(1, -100));
            }

            if (!this.canShoot) this.currentTime += delta;
            if (this.currentTime >= this.recoilTime && !this.canShoot) { this.currentTime = 0; this.canShoot = true; }
        }

        private void StopBeam() { this.drawbeam = false; }

    }
}
