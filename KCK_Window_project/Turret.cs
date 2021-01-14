using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class Turret
    {
        //pola
        //private int level;
        private int ammo;
        //private int dmg;
        private TurretType turretType;

        private int posX;
        private int posY;

        //konstruktor
        public Turret(Hero hero, TurretType turretType)
        {
            //level = 1;
            //ammo = 3;
            //dmg = 50;
            switch (turretType.GetLevel())
            {
                case 1:
                    ammo = 3;
                    break;
                case 2:
                    ammo = 5;
                    break;
                default:
                    ammo = 8;
                    break;
            }
            this.turretType = turretType;

            posY = 10;
            posX = hero.GetX();
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
        //public int GetDmg()
        //{
        //    return dmg;
        //}
        public int GetAmmo()
        {
            return ammo;
        }
        public TurretType GetTurretType()
        {
            return turretType;
        }
        //public int GetLevel()
        //{
        //    return level;
        //}
        //do wywalenia
        /*
        public char GetLevelChar()
        {
            switch (level)
            {
                case 1:
                    return '1';
                case 2:
                    return '2';
                default:
                    return '3';
            }
        }*/
        
        public int GetUpgradeCost()
        {
            switch (turretType.GetLevel())
            {
                case 1:
                    return 75;
                case 2:
                    return 125;
                default:
                    return 175;
            }
        }
        
        public static int GetBuildCost()
        {
            return 75;
        }

        //metody
        public TurretType Upgrade()
        {
            switch (turretType.GetLevel() + 1)
            {
                case 2:
                    ammo = 5;
                    return TurretFactory.GetTurretType(2, 75, "phase_2");
                default:
                    ammo = 8;
                    return TurretFactory.GetTurretType(3, 100, "phase_3");
            }
        }
        
        public void Shot()
        {
            ammo -= 1;
        }
    }
}
