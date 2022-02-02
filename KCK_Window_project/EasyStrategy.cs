using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class EasyStrategy : Strategy
    {
        List<Enemy> Strategy.CreateEnemies(Dictionary<string, Enemy> enemyTypes)
        {
            List<Enemy> enemies = new List<Enemy>();

            BasicEnemy basicEnemy = (BasicEnemy)enemyTypes["basic"].Clone();
            basicEnemy.SetX(1);
            basicEnemy.SetY(5);

            RedEnemy redEnemy = (RedEnemy)enemyTypes["red"].Clone();
            redEnemy.SetX(6);
            redEnemy.SetY(1);

            enemies.Add(basicEnemy);
            enemies.Add(redEnemy);
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
