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

        //konstruktor
        public Resources()
        {
            woodCurrentPhase = 0;
            stoneCurrentPhase = 0;
        }

        //gettery
        // 1 - drewno, 0 - kamien
        public int GetCurrentPhase(int which)
        {
            if (which == 1)
                return woodCurrentPhase;
            else
                return stoneCurrentPhase;
        }

        //metody
        public void WoodNextPhase()
        {
            woodCurrentPhase += 1;
            woodCurrentPhase %= 3;
        }
        public void StoneNextPhase()
        {
            stoneCurrentPhase += 1;
            stoneCurrentPhase %= 3;
        }
    }
}
