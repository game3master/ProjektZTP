using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class HardStrategy : Strategy
    {
        List<Enemy> Strategy.CreateEnemies(Dictionary<string, Enemy> enemyTypes)
        {
            List<Enemy> enemies = new List<Enemy>();

            BasicEnemy basicEnemyOne = (BasicEnemy)enemyTypes["basic"].Clone();
            basicEnemyOne.SetX(1);
            basicEnemyOne.SetY(4);

            TankEnemy tankEnemyOne = (TankEnemy)enemyTypes["tank"].Clone();
            tankEnemyOne.SetX(3);
            tankEnemyOne.SetY(1);

            BasicEnemy basicEnemyTwo = (BasicEnemy)enemyTypes["basic"].Clone();
            basicEnemyTwo.SetX(1);
            basicEnemyTwo.SetY(9);

            TankEnemy tankEnemyTwo = (TankEnemy)enemyTypes["tank"].Clone();
            tankEnemyTwo.SetX(6);
            tankEnemyTwo.SetY(1);

            enemies.Add(basicEnemyOne);
            enemies.Add(tankEnemyOne);
            enemies.Add(basicEnemyTwo);
            enemies.Add(tankEnemyTwo);
            return enemies;
        }

        void Strategy.MoveEnemies(List<Enemy> enemies)
        {
            enemies.ForEach((Enemy enemy) =>
            {
                enemy.Move();
            });
        }
    }
}
