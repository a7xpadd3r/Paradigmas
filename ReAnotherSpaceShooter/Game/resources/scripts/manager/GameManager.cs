using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;

namespace Game
{
    public enum GameStatus { Main, Game, End, None }
    public enum Sounds { ItemGrab, Shield, Repair, Special, Respawn }
    public enum MusicTracks { Intro, Stage, Win, Lose }
    public class GameManager
    {
        // Manager status
        private static GameStatus currentScreen = GameStatus.None;
        public GameStatus CurrentGameStatus => currentScreen;
        static mStageSpawner enemySpawn = new mStageSpawner();

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
        private static int latestscore = 0;
        private static NumbersToSprites score2sprites = new NumbersToSprites();

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

        // Enemy spawner timer
        private static bool canSpawn = true;
        private static float currentCD = 0;
        private static readonly float maxCD = 0.25f;

        // SFX
        private static bool playsomesounds = false;
        static SoundPlayer music = new SoundPlayer("resources/sfx/music/fbattery_loop.wav");
        public static SoundPlayer gameover = new SoundPlayer("resources/sfx/music/gameover.wav");
        public static SoundPlayer win = new SoundPlayer("resources/sfx/music/gameover.wav");
        private static WMPLib.WindowsMediaPlayer grabitem = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer shield = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer repair = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer respawn = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer special = new WMPLib.WindowsMediaPlayer();

        // Start
        public static void InitializeGame()
        {
            for (int i = 0; i < 30; i++) mBackgroud.AddBackStar(new Star());
            for (int i = 0; i < 30; i++) mBackgroud.AddFrontStar(new Star());

            MakeSounds();
            OnMainMenuScreen += MainMenu;
            OnGameScreen += ResetGame;
            OnEndScreen += EndGame;
            OnMainMenuScreen?.Invoke();
        }
        public static void MakeSounds()
        {
            repair.URL = "resources/sfx/effects/storedpowerup.wav";
            repair.controls.stop();
            shield.URL = "resources/sfx/effects/throw.wav";
            shield.controls.stop();
            grabitem.URL = "resources/sfx/effects/sprout.wav";
            grabitem.controls.stop();
            respawn.URL = "resources/sfx/effects/bob-omb.wav";
            respawn.controls.stop();
            special.URL = "resources/sfx/effects/inventory.wav";
            special.controls.stop();
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
            PlayMusic(MusicTracks.Stage);
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
            enemySpawn = new mStageSpawner();
            enemySpawn.Win += Win;
        }
        private static void EndGame(bool win)
        {
            music.Stop();
            latestscore = score;
            OnScoreUpdate -= UpdateUIScore;
            currentScreen = GameStatus.End;
            ui = null;
            theplayer = null;
            currentshipdata = null;
            mGameObject.WipeGameObjects();
        }
        public static void SpawnItem(Vector2 spawnPosition, ItemType type, WeaponTypes weaptype = WeaponTypes.BlueRail)
        {
            int newid = mGameObject.GenerateObjectID();
            var itemspecial = fyPoolDay.Pool.CreateItem("World", newid);

            itemspecial.Awake(spawnPosition, type, weaptype);
        }

        // Player events
        public static void GiveItem(ItemType itemtype, WeaponTypes weaptype = WeaponTypes.BlueRail)
        {
            switch (itemtype)
            {
                case ItemType.Repair:   theplayer.GetItem(ItemType.Repair);          break;
                case ItemType.Shield:   theplayer.GetItem(ItemType.Shield);          break;
                case ItemType.Special:  PlaySound(Sounds.Special); playerlifes++; ui.UpdateLifes(playerlifes);  break;
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
        private static void Win()
        {
            if (theplayer != null)
            {
                theplayer.CurrentShipDamage -= UpdateCurrentShipDamage;
                theplayer.OnPlayerDeath -= PlayerSleeping;
                theplayer.OnAmmoUpdate -= UpdateUIAmmo;
                theplayer.OnWeaponsUpdate -= UpdateWeapons;
                theplayer.OnWeaponChange -= UpdateCurrentWeapon;
            }

            gameoverscene = new sGameOver(true); 
            OnEndScreen?.Invoke(true);
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

            SpawnEnemyHotkey();
            if (enemySpawn != null) enemySpawn.Update(Program.GetDeltaTime);
        }
        private static void EndUpdate()
        {
            if (gameoverscene == null) return;

            gameoverscene.OnExitScene += MainMenu;
            gameoverscene.Update();
            score2sprites.RenderNumbers(latestscore, new Vector2(900, 600), new Vector2(2, 2));
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
        private static void UpdateCurrentWeapon(iWeapon newweapon) { if (ui != null) ui.UpdateCurrentWeapon(newweapon); }
        private static void UpdateCurrentShipDamage(Texture newtexture) { if (ui != null) ui.UpdateShipLifeTexture(newtexture); }
        private static void UpdateUIScore(int howmuch) { score += howmuch; if (score < 0) score = 0; ui.score = score; }

        // Enemy spawn
        private static void SpawnEnemyHotkey()
        {
            // Spawn enemies
            if (Engine.GetKey(Keys.F1) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newmosquitoe = fyPoolDay.Pool.CreateMosquitoe(newid);
                newmosquitoe.Awake(new Vector2(260, 100), false, MovementType.WaveY, AxisY.None, -100, 2020, -100, 1180);
            }
            if (Engine.GetKey(Keys.F2) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newmosquitoe = fyPoolDay.Pool.CreateMosquitoe(newid);
                newmosquitoe.Awake(new Vector2(460, 100), true);
            }
            if (Engine.GetKey(Keys.F3) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newslider = fyPoolDay.Pool.CreateSlider(newid);
                newslider.Awake(new Vector2(760, 100), false);
            }
            if (Engine.GetKey(Keys.F4) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newslider = fyPoolDay.Pool.CreateSlider(newid);
                newslider.Awake(new Vector2(960, 100), true);
            }
            if (Engine.GetKey(Keys.F5) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newtremor = fyPoolDay.Pool.CreateTremor(newid);
                newtremor.Awake(new Vector2(1260, 100), false);
            }
            if (Engine.GetKey(Keys.F6) && canSpawn)
            {
                canSpawn = false;
                int newid = mGameObject.GenerateObjectID();
                var newtremor = fyPoolDay.Pool.CreateTremor(newid);
                newtremor.Awake(new Vector2(1460, 100), true);
            }

            if (!canSpawn && currentCD < maxCD) currentCD += Program.GetDeltaTime;
            if (!canSpawn && currentCD > maxCD) { currentCD = 0; canSpawn = true; }
        }

        // Utils
        public static Direction ProyectileDirection(Vector2 input)
        {
            Direction value = Direction.Down;
            if (input.Y > PlayerPosition.Y) value = Direction.Up;
            return value;
        }
        public static void PlaySound(Sounds whichone)
        {
            if (playsomesounds)
            {
                switch (whichone)
                {
                    case Sounds.ItemGrab: GameManager.grabitem.controls.play(); break;
                    case Sounds.Shield: GameManager.shield.controls.play(); break;
                    case Sounds.Repair: GameManager.repair.controls.play(); break;
                    case Sounds.Special: GameManager.special.controls.play(); break;
                    case Sounds.Respawn: GameManager.respawn.controls.play(); break;
                    default:    GameManager.grabitem.controls.play(); break;
                }
            }
        }
        public static void PlayMusic(MusicTracks whichone)
        {
            switch (whichone)
            {
                case MusicTracks.Intro: break;
                case MusicTracks.Stage: music.PlayLooping(); break;
                case MusicTracks.Win: break;
                case MusicTracks.Lose: gameover.Play(); break;
            }
        }
    }
}
