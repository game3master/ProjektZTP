using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class Enemy
    {
        private int hp;
        private int dmg;
        private int speed;

        private int posX;
        private int posY;

        private bool justCreated;

        //konstruktor
        public Enemy()
        {
            hp = 300;
            dmg = 10;
            speed = 1;

            Random rnd = new Random();
            posX = rnd.Next(10);
            posY = 0;

            justCreated = true;
        }

        //gettery
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

        //settery
        public void SetHP(int hp)
        {
            this.hp = hp;
        }

        //metody
        public void Move(char[,] board)//board[15, 10]
        {
            //jezeli wyszedl by za sciane
            if (posY + speed >= 10)
                return;
            //jezeli przed nim stoi inny przeciwnik
            if (board[posY + speed, posX] != '.')
                return;
            posY += speed;
        }
        public void Hit(int dmg)
        {
            hp -= dmg;
        }
        //metoda potrzebna by przeciwnik po stworzeniu nie ruszyl sie odrazu
        public void ChangeStatus()
        {
            justCreated = false;
        }
    }
}
