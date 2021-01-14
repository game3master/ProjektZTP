using System;
using System.Collections.Generic;
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

        /* Konstruktor */
        public TurretType(int level, int dmg, string name)
        {
            this.level = level;
            this.dmg = dmg;
            this.name = name;
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

        /* Jakies metody */
    }
}
