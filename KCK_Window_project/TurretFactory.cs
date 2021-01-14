using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KCK_Window_project
{
    public class TurretFactory
    {
        // Pula istniejacych pylkow (Typy wiezyczek).
        private static List<TurretType> turretTypes = new List<TurretType>();

        // Zwrocenie pylka (Typu wiezyczki) o pozadanym wewnetrznym stanie.
        public static TurretType GetTurretType(int level, int dmg, string name, Image image)
        {
            foreach (TurretType t in turretTypes)
            {
                if (t.GetLevel() == level)
                {
                    return t;
                }
            }
            TurretType turret = new TurretType(level, dmg, name, image);
            turretTypes.Add(turret);
            return turret;
        }
    }
}
