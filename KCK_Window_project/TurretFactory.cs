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
        private List<TurretType> turretTypes = new List<TurretType>();
        private List<Turret> turrets = new List<Turret>();

        public TurretFactory()
        {
            turretTypes.Add(new TurretType(1, Properties.Resources.brick_orange));
            turretTypes.Add(new TurretType(2, Properties.Resources.grassBlock));
        }

        private TurretType GetTurretType(int index)
        {
            foreach(TurretType t in turretTypes)
            {
                if (t.GetIndex() == index)
                {
                    return t;
                }
            }
            return null;
        }

        public void AddTurret(int index, int posX, int posY)
        {
            TurretType type = this.GetTurretType(index);
            if (type == null)
            {
                return;
            }
            Turret turret = new Turret(type, posX, posY);
            turrets.Add(turret);
        }

        public List<Turret> GetAllTurrets()
        {
            return this.turrets;
        }
        
    }
}
