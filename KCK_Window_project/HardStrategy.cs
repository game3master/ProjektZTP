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

            RedEnemy redEnemyOne = (RedEnemy)enemyTypes["red"].Clone();
            redEnemyOne.SetX(3);
            redEnemyOne.SetY(1);

            BasicEnemy basicEnemyTwo = (BasicEnemy)enemyTypes["basic"].Clone();
            basicEnemyTwo.SetX(1);
            basicEnemyTwo.SetY(9);

            RedEnemy redEnemyTwo = (RedEnemy)enemyTypes["red"].Clone();
            redEnemyTwo.SetX(6);
            redEnemyTwo.SetY(1);

            enemies.Add(basicEnemyOne);
            enemies.Add(redEnemyOne);
            enemies.Add(basicEnemyTwo);
            enemies.Add(redEnemyTwo);
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
