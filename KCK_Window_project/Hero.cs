using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class Hero
    {
        private int posX;
        private int posY;

        /* Singleton - Hero */
        private static Hero instance;

        private Hero()
        {
            posX = 5;
            posY = 13;
        }

        public static Hero getInstance()
        {
            if (instance == null)
                instance = new Hero();
            return instance;
        }

        /* Gettery */
        public int GetX()
        {
            return posX;
        }
        public int GetY()
        {
            return posY;
        }

        /* Metody */
        // Ruch w lewo.
        public void MoveLeft()
        {
            // Zeby nie wyjsc za sciane.
            if (posX == 1)
                return;
            posX -= 1;
        }

        // Ruch w prawo.
        public void MoveRight()
        {
            // Zeby nie wyjsc za sciane.
            if (posX == 8)
                return;
            posX += 1;
        }

        // Ruch w gore.
        public void MoveUp()
        {
            if (posY == 1)
                return;
            posY -= 1;
        }

        // Ruch w dol.
        public void MoveDown()
        {
            // Zeby nie wyjsc za sciane.
            if (posY == 13)
                return;
            posY += 1;
        }

    }
}
