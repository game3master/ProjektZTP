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

        //konstruktor
        public Hero()
        {
            posX = 5;
            posY = 13;
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

        //poruszanie sie
        public void MoveLeft()
        {
            //zeby nie wyjsc za sciane
            if (posX == 0)
                return;
            //zeby nie wejsc na drewno
            if (posX == 1 && posY == 14)
                return;
            posX -= 1;
        }
        public void MoveRight()
        {
            //zeby nie wyjsc za sciane
            if (posX == 9)
                return;
            //zeby nie wejsc na kamien
            if (posX == 8 && posY == 14)
                return;
            posX += 1;
        }
        public void MoveUp()
        {
            if (posY == 11)
                return;
            posY -= 1;
        }
        public void MoveDown()
        {
            //zeby nie wyjsc za sciane
            if (posY == 14)
                return;
            //zeby nie wejsc na drewno
            if (posX == 0 && posY == 13)
                return;
            //zeby nie wejsc na kamien
            if (posX == 9 && posY == 13)
                return;
            posY += 1;
        }

        // 0 - zebrano kamien
        // 1 - zebrano drewno
        // 2 - nie zebrano nic
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

        //czy mozna postawic wiezyczke
        public bool CanPlace(List<Turret> turretList)
        {
            if (turretList.ElementAt(posX) == null)
            {
                if (Game.wood >= Turret.GetBuildCost() && Game.stone >= Turret.GetBuildCost())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //czy mozna ulepszyc wiezyczke
        public bool CanUpgrade(List<Turret> turretList)
        {
            if (turretList.ElementAt(posX) != null)
            {
                Turret t = turretList.ElementAt(posX);
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
