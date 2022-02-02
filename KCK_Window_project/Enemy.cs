using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public abstract class Enemy
    {
        protected const int SPEED = 1;

        protected int posX;
        protected int posY;

        protected string enemyType;

        /* Gettery */
        public int GetX()
        {
            return posX;
        }
        public int GetY()
        {
            return posY;
        }
        public string GetEnemyType()
        {
            return enemyType;
        }

        public Enemy(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        /* Settery */
        public void SetX(int posX)
        {
            this.posX = posX;
        }
        public void SetY(int posY)
        {
            this.posY = posY;
        }

        /* Metody */
        // Poruszanie sie przeciwnika.
        public abstract void Move();

        // Klonowanie.
        public abstract Enemy Clone();
    }
}
