using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    public enum GameStatus { Main, Game, End, None }
    public class GameManager
    {
        // Manager status
        private static GameStatus currentScreen = GameStatus.None;
        public GameStatus CurrentGameStatus => currentScreen;

        // Player relevant stuff
        public static Vector2 PlayerPosition => lastplayerpos;
        public static int PlayerLifes => playerlifes;
        public static ShipData PlayerShip => currentshipdata;
        private static Vector2 lastplayerpos = new Vector2();
        private static int playerlifes = 3;
        private readonly static int initialplayerlifes = 3;
        static Player theplayer;
        static ShipData currentshipdata;
        private static int score = 0;

        // Other
        static UI ui = new UI(0);
        static float delta = Program.GetDeltaTime;
        static int shipselection = 0;

        // Events
        public static Action OnMainMenuScreen;
        public static Action OnGameScreen;
        public static Action<bool> OnEndScreen;
        public static Action<int> OnScoreUpdate;

        // Scenes manager
        static sMainMenu mainmenuscene;
        static sGameOver gameoverscene;

        // Start
        public static void InitializeGame()
        {
            for (int i = 0; i < 30; i++) mBackgroud.AddBackStar(new Star());
            for (int i = 0; i < 30; i++) mBackgroud.AddFrontStar(new Star());

            OnMainMenuScreen += MainMenu;
            OnGameScreen += ResetGame;
            OnEndScreen += EndGame;

            SoundPlayer music = new SoundPlayer("resources/sfx/music/fbattery_loop.wav");
            //music.Play();

            OnMainMenuScreen?.Invoke();
        }
        public static void Update()
        {
            delta = Program.GetDeltaTime; 

            UnconditionalBackUpdate(delta);
            switch (currentScreen)
            {
                case GameStatus.Main:   MainMenuUpdate();   break;
                case GameStatus.Game:   StageUpdate();      break;
                case GameStatus.End:    EndUpdate();        break;
                default:                MainMenuUpdate();   break;
            }
            UnconditionalFrontUpdate(delta);
        }

        // Gameplay events
        private static void MainMenu()
        {
            currentshipdata = ShipsProperties.ElCapitan;
            mainmenuscene = null; mainmenuscene = new sMainMenu(); mainmenuscene.Start(); currentScreen = GameStatus.Main; 
            mainmenuscene.OnStartGame += ResetGame; mainmenuscene.OnShipChange += UpdateCurrentShip;
        }
        private static void ResetGame()
        {
            score = 0;
            mainmenuscene.OnStartGame -= ResetGame; mainmenuscene = null;
            OnScoreUpdate += UpdateUIScore;

            playerlifes = initialplayerlifes;
            currentshipdata = ShipsProperties.ElCapitan;
            GenerateNewPlayer();
            currentScreen = GameStatus.Game;

            ui = null;
            ui = new UI(shipselection); 
            ui.UpdateLifes(playerlifes);
        }
        private static void EndGame(bool win)
        {
            OnScoreUpdate -= UpdateUIScore;
            currentScreen = GameStatus.End;
            ui = null;
            theplayer = null;
            currentshipdata = null;
            mGameObject.WipeGameObjects();
        }

        // Player events
        public static void GiveItem(ItemType itemtype, WeaponTypes weaptype = WeaponTypes.BlueRail)
        {
            switch (itemtype)
            {
                case ItemType.Repair:   theplayer.GetItem(ItemType.Repair);          break;
                case ItemType.Shield:   theplayer.GetItem(ItemType.Shield);          break;
                case ItemType.Special:  playerlifes++; ui.UpdateLifes(playerlifes);  break;
                case ItemType.Weapon:
                    switch (weaptype)
                    {
                        case WeaponTypes.BlueRail:  theplayer.GetWeapon(WeaponTypes.BlueRail);    break;
                        case WeaponTypes.RedDiamond:theplayer.GetWeapon(WeaponTypes.RedDiamond);  break;
                        case WeaponTypes.GreenCrast:theplayer.GetWeapon(WeaponTypes.GreenCrast);  break;
                        case WeaponTypes.HeatTrail: theplayer.GetWeapon(WeaponTypes.HeatTrail);   break;
                        case WeaponTypes.OrbWeaver: theplayer.GetWeapon(WeaponTypes.OrbWeaver);   break;
                        case WeaponTypes.Gamma:     theplayer.GetWeapon(WeaponTypes.Gamma);       break;
                    }   break;
            }
        }
        private static void GenerateNewPlayer()
        {
            int newid = mGameObject.GenerateObjectID();
            var newplayer = fyPoolDay.Pool.CreatePlayer(newid);

            theplayer = newplayer;

            switch (shipselection)
            {
                case 0: theplayer.Awake(new Vector2(800, 800), ShipsProperties.ElCapitan);   break;
                case 1: theplayer.Awake(new Vector2(800, 800), ShipsProperties.SonicShip);   break;
                case 2: theplayer.Awake(new Vector2(800, 800), ShipsProperties.SkullFlower); break;
            }

            theplayer.OnPlayerDeath += PlayerSleeping;
            theplayer.OnAmmoUpdate += UpdateUIAmmo;
            theplayer.OnWeaponsUpdate += UpdateWeapons;
            theplayer.OnWeaponChange += UpdateCurrentWeapon;

            if (ui != null) 
            {
                ui.UpdateLifes(playerlifes);
                theplayer.CurrentShipDamage += UpdateCurrentShipDamage;
            }
            theplayer.CurrentShipDamage += UpdateCurrentShipDamage;

        }
        private static void PlayerSleeping()
        {
            if (theplayer != null) 
            {
                theplayer.CurrentShipDamage -= UpdateCurrentShipDamage;
                theplayer.OnPlayerDeath -= PlayerSleeping; 
                theplayer.OnAmmoUpdate -= UpdateUIAmmo; 
                theplayer.OnWeaponsUpdate -= UpdateWeapons;
                theplayer.OnWeaponChange -= UpdateCurrentWeapon;
            }
            if (PlayerLifes > 0) { playerlifes--; GenerateNewPlayer(); }
            else { gameoverscene = new sGameOver(false); OnEndScreen?.Invoke(false); }
        }

        // Gameplay updates
        private static void MainMenuUpdate()
        {
            mainmenuscene.Update(delta);
        }
        private static void StageUpdate()
        {
            if (theplayer != null) lastplayerpos = theplayer.Position;
            if (ui != null) ui.Update();
            mGameObject.Update();
            mEffects.Update(PlayerPosition);

            if (Engine.GetKey(Keys.DELETE)) { PlayerSleeping(); gameoverscene = new sGameOver(false); OnEndScreen?.Invoke(false); }
            if (Engine.GetKey(Keys.INSERT)) { OnScoreUpdate?.Invoke(250); }
            if (Engine.GetKey(Keys.HOME)) { GiveItem(ItemType.Repair); GiveItem(ItemType.Shield); if (playerlifes < 10) GiveItem(ItemType.Special); }
            if (Engine.GetKey(Keys.END))
            {
                GiveItem(ItemType.Weapon, WeaponTypes.RedDiamond); GiveItem(ItemType.Weapon, WeaponTypes.GreenCrast);
                GiveItem(ItemType.Weapon, WeaponTypes.HeatTrail); GiveItem(ItemType.Weapon, WeaponTypes.OrbWeaver);
                GiveItem(ItemType.Weapon, WeaponTypes.Gamma);
            }
        }
        private static void EndUpdate()
        {
            if (gameoverscene == null) return;

            gameoverscene.OnExitScene += MainMenu;
            gameoverscene.Update();
        }
        private static void UnconditionalBackUpdate(float delta) { mBackgroud.UpdateBack(delta); }
        private static void UnconditionalFrontUpdate(float delta) { mBackgroud.UpdateFront(delta); }

        // Main menu configs
        private static void UpdateCurrentShip(int whichone) 
        {
            switch (whichone)
            {
                case 0: shipselection = 0; break;
                case 1: shipselection = 1; break;
                case 2: shipselection = 2; break;
            }
            
        }

        // UI Updates
        private static void UpdateUIAmmo(double newamount) { if (ui != null) ui.UpdateAmmo(newamount); }
        private static void UpdateWeapons(List<iWeapon> weaponlist) { if (ui != null) ui.UpdateWeapons(weaponlist); }
        private static void UpdateCurrentWeapon(iWeapon newweapon) { if (ui != null) ui.UpdateCurrentWeapon(newweapon);  }
        private static void UpdateCurrentShipDamage(Texture newtexture) { if (ui != null) ui.UpdateShipLifeTexture(newtexture); }
        private static void UpdateUIScore(int howmuch) { score += howmuch; ui.score = score; }
    }
}
