using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class BasicEnemy : Enemy
    {
        /* Konstruktor */
        public BasicEnemy()
        {
            hp = 150;
            dmg = 10;
            speed = 1;

            Random rnd = new Random();
            posX = rnd.Next(10);
            posY = 0;

            justCreated = true;
            enemyType = "basic";
        }

        // Klonowanie.
        public override Enemy Clone()
        {
            return this.MemberwiseClone() as Enemy;
        }
    }
}
