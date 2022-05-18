using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LevelScene
    {
        private static List<GameObject> gameobjects = new List<GameObject>();
        //Campos
        private static List<BulletController> playerBullets = new List<BulletController>();
        private static List<BulletController> enemyBullets = new List<BulletController>();
        private static PlayerController playerController = null;
       
        private string backgroundTexturePath;


        //Propiedades
        public static List<BulletController> PlayerBullets => playerBullets;
        public static List<BulletController> EnemyBullets => enemyBullets;
        private Vector2 backgroundPosition = Vector2.Zero;

      
        //Constructor
        public LevelScene(string backgroundTexturePath)
        {
            this.backgroundTexturePath = backgroundTexturePath;
        }
        
        //Metodos

        //Initialize
        public void Initialize()
        {          
            playerController = new PlayerController(new Vector2(300,600), 1, 0, 300, 100, 0.5f, GetPlayerAnimations());
            playerController.onDie += OnPlayerDieHandler;
                   
            //Se crea la lista de Enemigos y se instancia
          

            for(int i = 1; i <= 1; i++)
            {
                EnemyController enemy = new EnemyController(new Vector2(100, 100), 1, 180, 100, 100, 1, GetEnemyAnimations());              
            }

        }


        //Update
        public void Update()
        {
                           

        }


        //Render
        public void Render()
        {        
            Engine.Draw(backgroundTexturePath, backgroundPosition.X, backgroundPosition.Y);           
        }

        //Animaciones del Player
        private static List<Animation> GetPlayerAnimations()
        {
            //Se crea la Lista
            List<Animation> animationsPlayer = new List<Animation>();


            //Idle Animation
            List<Texture> idleFrames = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                idleFrames.Add(Engine.GetTexture($"Textures/Animations/Player/Idle/{i}.png"));
            }
            Animation idleAnimation = new Animation("IdlePlayer", idleFrames, 0.2f, true);
            animationsPlayer.Add(idleAnimation);

            return animationsPlayer;
            


            //Damage Animation Player
            List<Texture> damageFrames = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                damageFrames.Add(Engine.GetTexture($"Textures/Animations/Player/Damage/{i}.png"));
            }
            Animation damageAnimation = new Animation("DamagePlayer", damageFrames, 0.2f, false);
            animationsPlayer.Add(damageAnimation);

            return animationsPlayer;

            //Explosion Animation Player
            List<Texture> explosionFrames = new List<Texture>();

            for (int i = 0; i < 10; i++)
            {
                explosionFrames.Add(Engine.GetTexture($"Textures/Animations/Player/Explosion/{i}.png"));
            }
            Animation explosionAnimation = new Animation("ExplosionPlayer", explosionFrames, 0.2f, false);
            animationsPlayer.Add(explosionAnimation);

            return animationsPlayer;
        }


        //Enemy Animations
        private static List<Animation> GetEnemyAnimations()
        {
            //Se crea la lista de Animaciones del Enemigo
            List<Animation> animationsEnemy = new List<Animation>();

            //Idle Animation Enemy
            List<Texture> idleFrames = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                idleFrames.Add(Engine.GetTexture($"Textures/Animations/Enemy/Idle/{i}.png"));
            }
            Animation idleAnimationEnemy = new Animation("IdleEnemy", idleFrames, 0.2f, true);
            animationsEnemy.Add(idleAnimationEnemy);

            return animationsEnemy;


            //Damage Animation Enemy
            List<Texture> damageFrames = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                damageFrames.Add(Engine.GetTexture($"Textures/Animations/Player/Damage/{i}.png"));
            }
            Animation damageAnimation = new Animation("Damage", damageFrames, 0.2f, false);
            animationsEnemy.Add(damageAnimation);

            return animationsEnemy;


            //Explosion Animation Enemy
            List<Texture> explosionFrames = new List<Texture>();

            for (int i = 0; i < 10; i++)
            {
                explosionFrames.Add(Engine.GetTexture($"Textures/Animations/Enemy/Explosion/{i}.png"));
            }
            Animation explosionAnimationEnemy = new Animation("ExplosionEnemy", explosionFrames, 0.2f, false);
            animationsEnemy.Add(explosionAnimationEnemy);

            return animationsEnemy;
        }


        private void OnPlayerDieHandler()
        {
            GameManager.Instance.ChangeScene(Scenes.GameOverScene);
        }
        
    }
}
