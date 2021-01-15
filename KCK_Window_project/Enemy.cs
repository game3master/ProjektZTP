using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public abstract class Enemy
    {
        protected int hp;
        protected int dmg;
        protected int speed;

        protected int posX;
        protected int posY;

        protected bool justCreated;
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
        public int GetDmg()
        {
            return dmg;
        }
        public int GetHP()
        {
            return hp;
        }
        public bool GetStatus()
        {
            return justCreated;
        }
        public string GetEnemyType()
        {
            return enemyType;
        }

        /* Settery */
        public void SetHP(int hp)
        {
            this.hp = hp;
        }
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
        public void Move()
        {
            // Jezeli wyszedlby za sciane.
            if (posY + speed >= 10)
                return;
            // Jezeli przed nim stoi inny przeciwnik.
            if (GameBoard.board[posY + speed, posX] != '.')
                return;
            posY += speed;
        }

        // Otrzymanie obrazen.
        public void Hit(int dmg)
        {
            hp -= dmg;
        }

        // Metoda potrzebna by przeciwnik po stworzeniu nie ruszyl sie odrazu.
        public void ChangeStatus()
        {
            justCreated = false;
        }

        // Klonowanie.
        public abstract Enemy Clone();
    }
}
