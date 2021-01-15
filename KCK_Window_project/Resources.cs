using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class Resources
    {
        private int woodCurrentPhase;
        private int stoneCurrentPhase;

        /* Singleton - Resources */
        private static Resources instance;

        private Resources()
        {
            woodCurrentPhase = 0;
            stoneCurrentPhase = 0;
        }

        public static Resources getInstance()
        {
            if (instance == null)
                instance = new Resources();
            return instance;
        }

        /* Gettery */
        // 1 - drewno.
        // 0 - kamien.
        public int GetCurrentPhase(int which)
        {
            if (which == 1)
                return woodCurrentPhase;
            else
                return stoneCurrentPhase;
        }

        /* Metody */
        // Nastepna faza drewna.
        public void WoodNextPhase()
        {
            woodCurrentPhase += 1;
            woodCurrentPhase %= 3;
        }

        // Nastepna faza kamienia.
        public void StoneNextPhase()
        {
            stoneCurrentPhase += 1;
            stoneCurrentPhase %= 3;
        }
    }
}
