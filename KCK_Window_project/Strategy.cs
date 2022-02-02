using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public interface Strategy
    {
        void MoveEnemies(List<Enemy> enemies);

        List<Enemy> CreateEnemies(Dictionary<string, Enemy> enemyTypes);
    }
}
