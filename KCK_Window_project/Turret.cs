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
        private TurretType turretType;
        private int posX;
        private int posY;

        /* Konstruktor */
        public Turret(TurretType t, int posX, int posY)
        {
            this.turretType = t;
            this.posX = posX;
            this.posY = posY;
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
        public TurretType GetTurretType()
        {
            return turretType;
        }
    }
}
