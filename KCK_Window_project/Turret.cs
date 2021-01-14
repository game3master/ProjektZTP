using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KCK_Window_project
{
    public class Turret
    {
        private int ammo;
        private TurretType turretType;

        private int posX;
        private int posY;

        /* Konstruktor */
        public Turret(Hero hero, TurretType turretType)
        {
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

        /* Gettery */
        public int GetX()
        {
            return posX;
        }
        public int GetY()
        {
            return posY;
        }
        public int GetAmmo()
        {
            return ammo;
        }
        public TurretType GetTurretType()
        {
            return turretType;
        }
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

        /* Metody */
        // Zwrocenie typu wiezyczki po ulepszeniu
        public TurretType Upgrade()
        {
            switch (turretType.GetLevel() + 1)
            {
                case 2:
                    ammo = 5;
                    return TurretFactory.GetTurretType(2, 75, "phase_2", Properties.Resources.turret_phase2);
                default:
                    ammo = 8;
                    return TurretFactory.GetTurretType(3, 100, "phase_3", Properties.Resources.turret_phase3);
            }
        }
        
        // Oddanie strzalu przez wiezyczke
        public void Shot()
        {
            ammo -= 1;
        }
    }
}
