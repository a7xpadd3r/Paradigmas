using System;
using System.Numerics;

namespace Game
{
    public class wOrbWeaver : iWeapon
    {
        // Weapon stats
        private int ammo = 15;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;
        private string weapowner = "Player";
        public WeaponTypes ThisType => WeaponTypes.OrbWeaver;
        public int AddAmmoAmount => 20;

        // Orb Weaver stuff
        private Vector2 offset = new Vector2(16, 45);
        private Vector2 playerPos = new Vector2();
        private bool isfiring = false;
        private bool ballcharged = false;
        private bool canthrowball = false;
        private Animation chargeanim = Textures.GetEffectAnimation(EffectsAnimations.OrbWeaverCharge);
        private Animation orbloop = Textures.GetProyectileAnimation(Proyectile.OrbWeaver);

        private Vector2 renderpos => playerPos - offset;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }
        public float AdditionalSpeed { get; set; }
        public iGetWeapon Owner { get; set; }

        public wOrbWeaver() 
        { 
            this.recoilTime = 2;
            this.chargeanim.OnAnimationFinished += OnOrbCharge;
            this.orbloop.OnAnimationLooped += OnOrbReady;
        }

        public void NewOwner(iGetWeapon owner) { Owner = owner; }

        public void AddAmmo() { ammo += AddAmmoAmount; }

        public void Fire(string owner, Vector2 currentPosition)
        {
            this.weapowner = owner;
            this.playerPos = currentPosition;
            if (canShoot && ammo > 0)
            {
                isfiring = true;
            }
        }

        public void Update(float delta)
        {
            //Console.WriteLine(isfiring);
            if (!this.ballcharged && isfiring)
            {
                this.chargeanim.Update();
                Renderer.DrawCenter(this.chargeanim.CurrentTexture, this.renderpos, new Vector2(1.5f, 1.5f));
                //GameManager.chargeorb.controls.play();
            }
            else if (this.ballcharged && isfiring)
            {
                this.orbloop.Update();
                Renderer.DrawCenter(this.orbloop.CurrentTexture, this.renderpos, new Vector2(1.5f, 1.5f));
            }

            if (!isfiring)
            {
                this.ballcharged = false;
                this.chargeanim.Play();
                this.orbloop.Play();

                if (canthrowball)
                {
                    ammo--;
                    int newID = mGameObject.GenerateObjectID();
                    var pOrbWeaver = fyPoolDay.Pool.CreateOrbWeaverProyectile(this.weapowner, newID);
                    pOrbWeaver.Awake(this.renderpos);
                    //GameManager.fireorb.controls.play();
                    canthrowball = false;
                }
            }

            isfiring = false;

            if (!canShoot) currentTime += delta;
            if (currentTime >= recoilTime && !canShoot) { currentTime = 0; canShoot = true; }
        }

        private void OnOrbCharge() { ballcharged = true; }
        private void OnOrbReady() { canthrowball = true; }
    }
}
