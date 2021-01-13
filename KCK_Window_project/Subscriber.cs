using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public abstract class Subscriber
    {
        public abstract void update(Game game);
        // przy usuwaniu przeciwnikow bedzie problem chyba
    }
}
