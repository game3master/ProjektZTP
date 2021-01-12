using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class TankEnemy : Enemy
    {
        private int hp;
        private int dmg;
        private int speed;

        private int posX;
        private int posY;

        private bool justCreated;
        private string enemyType;

        /* Konstruktor */
        public TankEnemy()
        {
            hp = 300;
            dmg = 10;
            speed = 1;

            Random rnd = new Random();
            posX = rnd.Next(10);
            posY = 0;

            justCreated = true;
            enemyType = "tank";
        }

        /* Gettery */
        public override int GetX()
        {
            return posX;
        }
        public override int GetY()
        {
            return posY;
        }
        public override int GetDmg()
        {
            return dmg;
        }
        public override int GetHP()
        {
            return hp;
        }
        public override bool GetStatus()
        {
            return justCreated;
        }
        public override string GetEnemyType()
        {
            return enemyType;
        }

        /* Settery */
        public override void SetHP(int hp)
        {
            this.hp = hp;
        }
        public override void SetX(int posX)
        {
            this.posX = posX;
        }
        public override void SetY(int posY)
        {
            this.posY = posY;
        }

        /* Metody */
        public override void Move()//board[15, 10]
        {
            // Jezeli wyszedlby za sciane
            if (posY + speed >= 10)
                return;
            // Jezeli przed nim stoi inny przeciwnik
            if (GameBoard.board[posY + speed, posX] != '.')
                return;
            posY += speed;
        }
        public override void Hit(int dmg)
        {
            hp -= dmg;
        }

        // Metoda potrzebna by przeciwnik po stworzeniu nie ruszyl sie odrazu
        public override void ChangeStatus()
        {
            justCreated = false;
        }

        // Klonowanie
        public override Enemy Clone()
        {
            return this.MemberwiseClone() as Enemy;
        }
    }
}
