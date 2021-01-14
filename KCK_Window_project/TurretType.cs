using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class TurretType
    {
        private int level;
        private int dmg;
        private string name;
        private Image image;

        /* Konstruktor */
        public TurretType(int level, int dmg, string name, Image image)
        {
            this.level = level;
            this.dmg = dmg;
            this.name = name;
            this.image = image;
        }

        /* Gettery */
        public int GetLevel()
        {
            return level;
        }
        public int GetDmg()
        {
            return dmg;
        }
        public string GetName()
        {
            return name;
        }
        public Image GetImage()
        {
            return image;
        }

        /* Jakies metody */
    }
}
