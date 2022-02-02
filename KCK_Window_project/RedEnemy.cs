using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    enum VerticalDirection
    {
        UP,
        DOWN
    }
    class RedEnemy : Enemy
    {
        private VerticalDirection direction = VerticalDirection.DOWN;
        /* Konstruktor */
        public RedEnemy(int posX, int posY) : base(posX, posY)
        {
            enemyType = "red";
        }

        // Klonowanie.
        public override Enemy Clone()
        {
            return this.MemberwiseClone() as Enemy;
        }

        public override void Move()
        {
            // Sprawdź czy trzeba zmienić kierunek
            if (this.direction == VerticalDirection.DOWN && this.posY + SPEED >= 14)
            {
                this.direction = VerticalDirection.UP;
            }
            if (this.direction == VerticalDirection.UP && this.posY - SPEED <= 0)
            {
                this.direction = VerticalDirection.DOWN;
            }

            //Porusz się
            if (this.direction == VerticalDirection.DOWN)
            {
                this.posY += SPEED;
            }
            else
            {
                this.posY -= SPEED;
            }
        }
    }
}
