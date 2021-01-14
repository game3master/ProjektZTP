using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public class TurretFactory
    {
        // Pula istniejacych pylkow.
        private static List<TurretType> turretTypes = new List<TurretType>();

        // Zwrocenie pylka o pozadanym wewnetrznym stanie.
        public static TurretType GetTurretType(int level, int dmg, string name)
        {
            foreach (TurretType t in turretTypes)
            {
                if (t.GetLevel() == level)
                {
                    return t;
                }
            }
            TurretType turret = new TurretType(level, dmg, name);
            turretTypes.Add(turret);
            return turret;
        }
    }
}
