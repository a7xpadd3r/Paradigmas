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

        // Position stuff - needs to be moved to game object
        private static float posX = 500;
        private static float posY = 900;
        public static Vector2 Position => new Vector2(posX, posY);
        public Vector2 OwnerRailPosition => ship.ShipRailPosition;
        private bool ready = false;

        // Weapons stuff - iGetWeapon
        private int currentWeapIndex = -1;
        public List<iWeapon> AllWeapons { get; private set; } = new List<iWeapon>();
        public iWeapon CurrentWeapon => AllWeapons[currentWeapIndex];

        // Delay to prevent flash swap
        private bool canInput = true;
        private float currentInputCD = 0;
        private readonly float inputCD = 0.15f;

        // Damage stuff - needs to be moved to ship object
        private float originalLifes; // This is used to show the ship damage in percentage.
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
            this.ShipAnim = ShipConfiguration.ShipAnim;
            this.ShipPropellersAnim = ShipConfiguration.PropellersAnim;
            this.ShieldAnim = new Animation("PlayerShield", 0.03f, Effects.GetEffectTextures(2));
            this.ShipAnim.ChangeFrame(4); // Intact ship texture
            this.realSize = new Vector2(ShipAnim.CurrentTexture.Width, ShipAnim.CurrentTexture.Height);

            // "Blue Trail" is always the default weapon, because it has infinite ammo.
            GetWeapon(new Item(ItemType.Weapon, new Vector2(), WeaponTypes.BlueRail));
            NextWeapon();

            // Update UI
            UI.UpdateUIShippy(ShipAnim.GetFrameTexture(4));
            UI.UpdateWeapons(AllWeapons);

            // Collision
            this.objectCollider = new Collider(Position, this.ship.ShipSize, this.owner, this.tag, 3);

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
                        if (theItem != null)
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
                                    GetWeapon(theItem);
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
                this.currentShieldTime = 0;
                this.life -= amount;
                new GenericEffect(Position, new Vector2(1.8f, 1.8f), new Vector2(30, 60), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);

                if (life <= 0) Destroy();
                
                UpdateShipTexture();
            }
        }

        public override void Update()
        {
            if (ready)
            {
                float delta = Program.GetDeltaTime();
                // Update stuff
                UpdateShipPosition(Position);
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset);
                callsDamageOnCollision = !IsShielding;

                // Movement controls
                if (Engine.GetKey(Keys.A) && posX > -55) posX -= ShipConfiguration.ShipSpeed * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.D) && posX < 1840) posX += ShipConfiguration.ShipSpeed * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.W) && posY > 0) posY -= (ShipConfiguration.ShipSpeed / 1.1f) * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.S) && posY < 1040) posY += (ShipConfiguration.ShipSpeed / 1.1f) * Program.GetDeltaTime();

                // Shield managment
                if (IsShielding) { currentShieldTime += delta; }
                if (currentShieldTime >= shieldTime && IsShielding) IsShielding = false;

                // Weapon managment, called only if there is at least one weapon available.
                if (currentInputCD < inputCD) currentInputCD += Program.GetDeltaTime();
                if (currentInputCD >= inputCD && !canInput) canInput = true;

                if (AllWeapons.Count != 0)
                {
                    // Remove the weapon if no ammo is available but ONLY if is not Blue Rail.
                    if (CurrentWeapon.CurrentAmmo <= 0 && CurrentWeapon.Type != WeaponTypes.BlueRail) RemoveWeapon();

                    // Call update on the current weapon
                    CurrentWeapon.Update(delta, Position + OwnerRailPosition);

                    // Swapping inventory
                    if (AllWeapons.Count > 1 && !Engine.GetKey(Keys.SPACE) && canInput)
                    {
                        if (Engine.GetKey(Keys.Q)) { NextWeapon(); }
                        if (Engine.GetKey(Keys.E)) { PreviousWeapon(); }

                        if ((Engine.GetKey(Keys.Q)) || (Engine.GetKey(Keys.E)))
                        {
                            canInput = false;
                            currentInputCD = 0;
                        }
                    }

                    // Call fire ONLY if there is at least 1 bullet (if is not the Blue Rail one).
                    if (Engine.GetKey(Keys.SPACE))
                    {
                        // Blue Rail always fires.
                        if (CurrentWeapon.Type == WeaponTypes.BlueRail) CurrentWeapon.Fire();

                        if (CurrentWeapon.CurrentAmmo > 0 && CurrentWeapon.Type != WeaponTypes.BlueRail) 
                        { CurrentWeapon.Fire(); UI.UpdateAmmo(CurrentWeapon.CurrentAmmo); }
                    }
                }
            }
        }

        public override void Destroy()
        {
            this.objectCollider.OnCollision -= this.OnCollision; this.AnyDamage -= Damage;

            ManagerLevel1.OnPlayerDeath?.Invoke(UI.ShippysLeft); // Maybe needs to be reworked.
            GameObjectManager.RemoveGameObject(this);
        }

        // Special player functions
        public void GetWeapon(Item theItem)
        {
            // Generate the new weapon by getting the type from the item.
            var newWeapon = fWeapon.CreateWeapon(WeaponTypes.BlueRail); // Needs to have a default value.
            switch (theItem.WeaponType)
            {
                case WeaponTypes.BlueRail:
                    newWeapon = fWeapon.CreateWeapon(WeaponTypes.BlueRail);
                    break;
                case WeaponTypes.RedDiamond:
                    newWeapon = fWeapon.CreateWeapon(WeaponTypes.RedDiamond);
                    break;
                case WeaponTypes.GreenCrast:
                    newWeapon = fWeapon.CreateWeapon(WeaponTypes.GreenCrast);
                    break;
                case WeaponTypes.HeatTrail:
                    newWeapon = fWeapon.CreateWeapon(WeaponTypes.HeatTrail);
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

            // After that, get the type of this new weapon and check if the player already has it. If it does, then add ammo.
            bool alreadyInInvetory = false;
            foreach (iWeapon weapon in AllWeapons) if (weapon.Type == newWeapon.Type) { alreadyInInvetory = true; weapon.AddAmmo(); UI.UpdateAmmo(CurrentWeapon.CurrentAmmo); }

            // If not, then...
            if (!alreadyInInvetory) { newWeapon.NewOwner(this); AllWeapons.Add(newWeapon); } // Add the new weapon.
            if (AllWeapons.Count == 1) NextWeapon(); // Swap the weapon if the inventory is empty.
        }

        private void RemoveWeapon()
        {
            AllWeapons.Remove(CurrentWeapon);
            NextWeapon();
        }

        private void PreviousWeapon()
        {
            if (AllWeapons.Count != 0 && AllWeapons.Count != 1)
            {
                {
                    if (currentWeapIndex == 0) currentWeapIndex = AllWeapons.Count - 1;
                    else if (currentWeapIndex > 0) currentWeapIndex--;
                }

                UI.UpdateAmmo(CurrentWeapon.CurrentAmmo);
                UI.UpdateCurrentWeapons(CurrentWeapon);
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

                UI.UpdateAmmo(CurrentWeapon.CurrentAmmo);
                UI.UpdateCurrentWeapons(CurrentWeapon);
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