using System;
using System.Collections.Generic;

namespace Game
{
    public class Level1 : IScene
    {
        private static List<Enemy> enemyNormalList;
        private static List<EnemyMini> enemyMiniList;
        private static List<EnemyFast> enemyFastList;
        private static List<EnemyLow> enemyLowList;
        private static List<Boss> enemyBossList;
        private static List<EnemyTank> enemyTankList;

        public Level1()
        {
        }
        public void Initialize()
        {
            //Player
            var player = new PlayerController("Player", new Vector2(500, 500), 200, 200, new Vector2(0.8f, 0.8f), 1000, FactoryAnimation.CreateAnimation("Player"));
            player.OnDestroy += OnPlayerIsDeadHandler;

            //Weapons
            var misilPlayer = WeaponFactory.CreateWeapon(Weapon.MisilPlayer);
            var fuego = WeaponFactory.CreateWeapon(Weapon.Fuego);
            player.AddWeapon(misilPlayer);
            player.AddWeapon(fuego);

            //Enemy List
            enemyNormalList = new List<Enemy>();
            enemyLowList = new List<EnemyLow>();
            enemyMiniList = new List<EnemyMini>();
            enemyFastList = new List<EnemyFast>();
            enemyBossList = new List<Boss>();
            enemyTankList = new List<EnemyTank>();

            //EnemyFast
            for (float i = 1; i <= 3; i++)
            {
                var enemyFast = EnemyFactory.CreateEnemies(Enemies.Fast, new Vector2(100 * i, -300 * i));
                enemyFast.OnDestroy += OnEnemyIsDeadHandler;
            }

            //Low
            for (float i = 0.5f; i <= 5; i++)
            {
                var enemyLow = EnemyFactory.CreateEnemies(Enemies.Low, new Vector2(300 * i, -600 + i));
                enemyLow.OnDestroy += OnEnemyIsDeadHandler;
            }

            //Normal
            for (float i = 0.5f; i <= 3; i++)
            {
                var enemyNormal = EnemyFactory.CreateEnemies(Enemies.Normal, new Vector2(300 * i, -200 + i));
                enemyNormal.OnDestroy += OnEnemyIsDeadHandler;
            }
        /*
            //Mini
            for (float i = 1; i <= 5; i++)
            {
                var enemyMini = EnemyFactory.CreateEnemies(Enemies.Mini, new Vector2(800 * i, -350 * i));
                enemyMini.OnDestroy += OnEnemyIsDeadHandler;
            }

            //EnemyTank
            for (float i = 0.5f; i <= 5; i++)
            {
                var enemyTank = EnemyFactory.CreateEnemies(Enemies.Tank, new Vector2(550 * i, -600 + i));
                enemyTank.OnDestroy += OnEnemyIsDeadHandler;
            }
        */
        }
        public void Update()
        {
            if (Engine.GetKey(Keys.ESCAPE))
            {
                Environment.Exit(1);
            }
            if (Engine.GetKey(Keys.F3))
            {
                GameManager.Instance.ChangeScene(Scenes.MainMenu);
            }
            Win();
            if (CheckScore())
            {
                Points.Instance.ResetScore();            
            }
        }          
        public bool CheckScore()
        {
            return Points.Instance.ReturnScoreTotal() == enemyLowList.Count;
            return Points.Instance.ReturnScoreTotal() == enemyFastList.Count;
            return Points.Instance.ReturnScoreTotal() == enemyMiniList.Count;
            return Points.Instance.ReturnScoreTotal() == enemyNormalList.Count;
            return Points.Instance.ReturnScoreTotal() == enemyBossList.Count;
            return Points.Instance.ReturnScoreTotal() == enemyTankList.Count;
        }
        public void Render()
        {
            Engine.Draw("Textures/Background/2.png");
        }
        private void OnPlayerIsDeadHandler(GameObject gameObject)
        {
            GameObjectManager.RemoveGameObject(gameObject);
            GameManager.Instance.ChangeScene(Scenes.GameOver);

        }
        private void OnEnemyIsDeadHandler(GameObject gameObject)
        {
            GameObjectManager.RemoveGameObject(gameObject);
        }
        private void Win()
        {
            var listGameObject = GameObjectManager.ActiveGameObjects;
            for (int i = 1; i < listGameObject.Count; i++)
            {
                if (listGameObject[i].Tag == "Enemies")
                {
                    return;
                }
                else if (listGameObject[i].Tag != "Enemies")
                {
                   GameManager.Instance.ChangeScene(Scenes.Level2);
                }
            }
        }
        public void Finish()
        {
        }
    }
}
