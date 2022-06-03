using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject, iGetWeapon
    {
        public new bool debug = false;
        // Special stuff
        private ShipConfig ship = null;

        // Position stuff
        private static float posX = 500;
        private static float posY = 900;
        public static Vector2 Position => new Vector2(posX, posY);
        private Vector2 RailPosition => new Vector2(ship.ShipRailPosition().X, ship.ShipRailPosition().Y);
        private bool ready = false;

        // Weapons stuff - iGetWeapon
        private int currentWeapIndex = -1;
        public Vector2 OwnerRailPosition => RailPosition;
        public List<iWeapon> AllWeapons { get; private set; } = new List<iWeapon>();
        public iWeapon CurrentWeapon => AllWeapons[currentWeapIndex];

        private int currentWeapon = 1;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;

        // Damage stuff
        private float originalLifes;
        private readonly float shieldTime = 0.8f;
        private float currentShieldTime = 0;

        // Events
        public Action OnShipDestroyed;

        public Player(ShipConfig withThisShip, Vector2 newPos, string newOwner = "Player", int newLifes = 10)
        {
            // Basic stuff
            this.spawnPosition = newPos;
            this.life = newLifes;
            originalLifes = this.life;
            posX = spawnPosition.X;
            posY = spawnPosition.Y;

            // Tags
            this.owner = newOwner;
            this.tag = "Ship";

            // Ship configs
            this.ship = withThisShip; // Needs to be deprecated...
            this.ShipConfiguration = ship;

            // ShipObject references
            this.ShipAnim = ShipConfiguration.ShipAnim();
            this.ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            this.SmokeDamageAnim = new Animation("Smoke", 0.15f, Effects.GetEffectTextures(1), false, false);
            this.ShieldAnim = new Animation("PlayerShield", 0.03f, Effects.GetEffectTextures(2));
            this.ShipAnim.ChangeFrame(4); // Intact ship texture
            this.SmokeDamageAnim.OnAnimationFinished += OnSmokeEnded;

            // Weapon
            /*this.ShipInventory = new Inventory();
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.BlueRail, true, 1, 0.6f));
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.RedDiamond, false, 100, 0.2f));
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.GreenCrast, false, 30, 1f));
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.HeatTrail, false, 40, 0.78f));
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.OrbWeaver, false, 40, 0.78f));
            this.ShipInventory.GetWeapon(new Weapon(WeaponClasses.Gamma, false, 40, 0.78f));*/

            // Update UI
            UI.UpdateUIShippy(ShipAnim.GetFrameTexture(4));

            // Collision
            this.objectCollider = new Collider(Position, this.ship.ShipSize(), this.owner, this.tag, 3);
            //this.objectCollider.OnCollision += AnyDamage; // Needs to be changed?

            // Final set
            Awake(); Console.WriteLine("Player --> Jugador creado con el ID {0}", this.id); this.ready = true;
        }

        public override void OnCollision(Collider instigator)
        {
            if (instigator != null)
            {
                switch (instigator.owner)
                {
                    case "Enemy":
                        if (callsDamageOnCollision) AnyDamage?.Invoke(instigator.damage);
                        break;

                    case "Item":
                        Item theItem = GameObjectManager.GrabItem(instigator);
                        if (theItem != null && this.owner == "Player")
                        {
                            switch (theItem.GetType)
                            {
                                case ItemType.Repair:
                                    RepairShip();
                                    break;
                                case ItemType.Shield:
                                    Shield();
                                    break;
                                case ItemType.Special:
                                    Console.WriteLine("Special");
                                    break;
                                case ItemType.Weapon:
                                    switch (theItem.WeaponType)
                                    {
                                        case WeaponTypes.BlueRail:
                                            var BlueRail = fWeapon.CreateWeapon(WeaponTypes.BlueRail);
                                            GetWeapon(BlueRail);
                                            Console.Write("new weapon: " + BlueRail + "\n");
                                            break;
                                        case WeaponTypes.RedDiamond:
                                            var RedDiamond = fWeapon.CreateWeapon(WeaponTypes.RedDiamond);
                                            GetWeapon(RedDiamond);
                                            Console.Write("new weapon: " + RedDiamond + "\n");
                                            break;
                                        case WeaponTypes.GreenCrast:
                                            var GreenCrast = fWeapon.CreateWeapon(WeaponTypes.GreenCrast);
                                            GetWeapon(GreenCrast);
                                            Console.Write("new weapon: " + GreenCrast + "\n");
                                            break;
                                        case WeaponTypes.HeatTrail:
                                            break;
                                        case WeaponTypes.OrbWeaver:
                                            break;
                                        case WeaponTypes.Gamma:
                                            break;
                                        case WeaponTypes.Enemy1:
                                            break;
                                        case WeaponTypes.Enemy2:
                                            break;
                                        case WeaponTypes.Enemy3:
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                            GameObjectManager.RemoveItem(theItem);
                        }
                        break;
                }
            }
        }

        public override void Damage(float amount)
        {
            if (!IsShielding && BlinkingEnded)
            {
                this.IsShielding = true;
                this.ShieldAnim.ChangeFrame(0);
                this.SmokeDamageAnim.Play();
                this.currentShieldTime = 0;
                this.life -= amount;
                this.RenderSmoke = true;
                if (life <= 0) Destroy();
                UpdateShipTexture();
            }
        }

        /*
        private void Fire()
        {
            canShoot = false;
            //bullets.Add(new Proyectile(Position + RailPosition, currentWeapon));
            //ProyectilesManager.AddProyectile(new Proyectile(Position + RailPosition, currentWeapon, owner));
            new Proyectile(Position + RailPosition, currentWeapon, owner);
        }
        */
        public override void Update()
        {
            if (ready)
            {
                // Update stuff
                UpdateShipPosition(Position);
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset());
                callsDamageOnCollision = !IsShielding;

                // Movement controls
                if (Engine.GetKey(Keys.A) && posX > -55) posX -= ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.D) && posX < 1840) posX += ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.W) && posY > 0) posY -= (ShipConfiguration.ShipSpeed() / 1.1f) * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.S) && posY < 1040) posY += (ShipConfiguration.ShipSpeed() / 1.1f) * Program.GetDeltaTime();

                // Weapons managment
                //if (Engine.GetKey(Keys.SPACE) && canShoot) { Fire(); }
                if (Engine.GetKey(Keys.Num1)) { currentWeapon = 1; recoilTime = 0.5f; }
                if (Engine.GetKey(Keys.Num2)) { currentWeapon = 2; recoilTime = 0.8f; }
                if (Engine.GetKey(Keys.Num3)) { currentWeapon = 3; recoilTime = 0.25f; }

                if (!canShoot) currentTime += Program.GetDeltaTime();

                if (currentTime >= recoilTime && !canShoot)
                {
                    currentTime = 0;
                    canShoot = true;
                }

                if (IsShielding) { currentShieldTime += Program.GetDeltaTime(); }
                if (currentShieldTime >= shieldTime && IsShielding) IsShielding = false;

                // Weapon managment
                if (AllWeapons.Count != 0)
                {
                    CurrentWeapon.Update();
                    if (Engine.GetKey(Keys.SPACE) && canShoot) CurrentWeapon.Fire(Position + RailPosition);
                    if (Engine.GetKey(Keys.Q)) NextWeapon();
                    canShoot = false;
                }
            }
        }

        public override void Destroy()
        {
            new GenericEffect(Position, new Vector2(1.3f, 1.3f), new Vector2(30, 60), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
            this.objectCollider.OnCollision -= this.OnCollision; this.AnyDamage -= Damage; this.SmokeDamageAnim.OnAnimationFinished -= OnSmokeEnded;

            ManagerLevel1.OnPlayerDeath?.Invoke(UI.ShippysLeft); // Maybe needs to be reworked.
            GameObjectManager.RemoveGameObject(this);
        }

        // Special player functions
        public void GetWeapon(iWeapon newWeapon)
        {
            newWeapon.NewOwner(this);
            AllWeapons.Add(newWeapon);

            if (AllWeapons.Count == 1)
            {
                NextWeapon();
            }
        }

        private void NextWeapon()
        {
            if (AllWeapons.Count != 0)
            {
                currentWeapIndex++;
                {
                    if (currentWeapIndex >= AllWeapons.Count)
                    {
                        currentWeapIndex = 0;
                    }
                }
                Console.WriteLine("CNW -> " + currentWeapIndex + " Count -> " + AllWeapons.Count + " Name -> " + CurrentWeapon);
            }
        }

        private void UpdateShipTexture()
        {
            // Ship texture (shows damage)
            if ((life * 100) / originalLifes > 85)
            {
                ShipAnim.ChangeFrame(4);
                UI.UpdateUIShippy(ShipAnim.GetFrameTexture(4));
            }
            else if ((life * 100) / originalLifes < 85 && (life * 100) / originalLifes > 50)
            {
                ShipAnim.ChangeFrame(3);
                UI.UpdateUIShippy(ShipAnim.GetFrameTexture(3));
            }
            else if ((life * 100) / originalLifes < 50 && (life * 100) / originalLifes > 25)
            {
                ShipAnim.ChangeFrame(2);
                UI.UpdateUIShippy(ShipAnim.GetFrameTexture(2));
            }
            else if ((life * 100) / originalLifes < 25 && (life * 100) / originalLifes > 0)
            {
                ShipAnim.ChangeFrame(1);
                UI.UpdateUIShippy(ShipAnim.GetFrameTexture(1));
            }
        }

        private void RepairShip()
        {
            life += 5;
            if (life > originalLifes) life = originalLifes; // If grabs more than it should, set max.
            UpdateShipTexture();
            ResetBlinking(5);
            Console.WriteLine("Player --> Nave reparada con {0}, nuevo estado: {1}", 5, life);
        }

        private void Shield()
        {
            ResetBlinking(50);
            Console.WriteLine("Player --> Ítem de escudo recibido.");
        }
    }
}