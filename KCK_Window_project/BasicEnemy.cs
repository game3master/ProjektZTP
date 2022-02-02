using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    enum HorizontalDirection
    {
        RIGHT,
        LEFT
    }
    class BasicEnemy : Enemy
    {

        private HorizontalDirection direction = HorizontalDirection.RIGHT;
        
        /* Konstruktor */
        public BasicEnemy(int posX, int posY) : base(posX, posY)
        {
            enemyType = "basic";
        }

        // Klonowanie.
        public override Enemy Clone()
        {
            return this.MemberwiseClone() as Enemy;
        }

        public override void Move()
        {
            // Sprawdź czy trzeba zmienić kierunek
            if (this.direction == HorizontalDirection.RIGHT && this.posX + SPEED >= 9)
            {
                this.direction = HorizontalDirection.LEFT;
            }
            if (this.direction == HorizontalDirection.LEFT && this.posX - SPEED <= 0)
            {
                this.direction = HorizontalDirection.RIGHT;
            }

            //Porusz się
            if (this.direction == HorizontalDirection.RIGHT)
            {
                this.posX += SPEED;
            }
            else
            {
                this.posX -= SPEED;
            }
        }
    }
}
