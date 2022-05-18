using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Enemies
    {
        Normal, Fast, Low, Mini, Boss, Tank
    }
    public static class EnemyFactory
   {              
        public static IEnemy CreateEnemies(Enemies enemies, Vector2 position)
        {
            switch (enemies)
            {
                case Enemies.Normal:
                    return new Enemy("EnemyNormal", 25, 80, position, Vector2.One, 5, Bullet.MisilEnemy, 250, 50, FactoryAnimation.CreateAnimation("EnemyAnimation"), 180);
                case Enemies.Fast:
                    return new EnemyFast("EnemyFast", 25, 100, position, Vector2.One, 4, Bullet.MisilHelicopter, 100, 50, FactoryAnimation.CreateAnimation("EnemyFast"));
                case Enemies.Low:
                    return new EnemyLow("EnemyLow", 25, 80, position, Vector2.One, 4, Bullet.MisilEnemyLow, 500, 50, FactoryAnimation.CreateAnimation("EnemyLow"), 180);
                case Enemies.Mini:
                    return new EnemyMini("EnemyMini", 25, 80, position, new Vector2 (0.8f, 0.8f), 5, Bullet.PlasmaMini, 50, 50, FactoryAnimation.CreateAnimation("EnemyMini"), 180);
                case Enemies.Boss:
                    return new Boss("EnemyBoss", 200, position, new Vector2(2, 2), 1f, Bullet.MisilBoss, 3000, 50, FactoryAnimation.CreateAnimation("EnemyBoss"), 180);
                case Enemies.Tank:
                    return new EnemyTank("EnemyTank", 25, 80, position, Vector2.One, 5, Bullet.PlasmaTank, 500, 50, FactoryAnimation.CreateAnimation("EnemyTank"), 180);
                default:
                    return null;
            }
        }
        

    }
}
