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
            if (posX == 0)
                return;
            // Zeby nie wejsc na drewno.
            if (posX == 1 && posY == 14)
                return;
            posX -= 1;
        }

        // Ruch w prawo.
        public void MoveRight()
        {
            // Zeby nie wyjsc za sciane.
            if (posX == 9)
                return;
            // Zeby nie wejsc na kamien.
            if (posX == 8 && posY == 14)
                return;
            posX += 1;
        }

        // Ruch w gore.
        public void MoveUp()
        {
            if (posY == 11)
                return;
            posY -= 1;
        }

        // Ruch w dol.
        public void MoveDown()
        {
            // Zeby nie wyjsc za sciane.
            if (posY == 14)
                return;
            // Zeby nie wejsc na drewno.
            if (posX == 0 && posY == 13)
                return;
            // Zeby nie wejsc na kamien.
            if (posX == 9 && posY == 13)
                return;
            posY += 1;
        }

        // 0 - zebrano kamien.
        // 1 - zebrano drewno.
        // 2 - nie zebrano nic.
        public int Collect(Resources res)
        {
            if (posX == 1 && posY == 14)
            {
                if (res.GetCurrentPhase(1) != 2)
                    return 2;
                Game.wood += 50;
                res.WoodNextPhase();
                return 1;
            }
            if (posX == 8 && posY == 14)
            {
                if (res.GetCurrentPhase(0) != 2)
                    return 2;
                Game.stone += 50;
                res.StoneNextPhase();
                return 0;
            }
            return 2;
        }

        // Czy mozna postawic wiezyczke.
        public bool CanPlace(List<Turret> turretList)
        {
            // Czy przed bohaterem stoi jest puste miejsce.
            if (turretList.ElementAt(posX) == null)
            {
                // Czy mamy wystarczajaco drewna i kamienia by zbudowac wiezyczke.
                if (Game.wood >= Turret.GetBuildCost() && Game.stone >= Turret.GetBuildCost())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        // Czy mozna ulepszyc wiezyczke.
        public bool CanUpgrade(List<Turret> turretList)
        {
            // Czy przed bohaterem stoi wiezyczka do ulepszenia.
            if (turretList.ElementAt(posX) != null)
            {
                Turret t = turretList.ElementAt(posX);
                // Czy mamy wystarczajaco drewna i kamienia by ulepszyc wiezyczke.
                if (Game.wood >= t.GetUpgradeCost() && Game.stone >= t.GetUpgradeCost())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
