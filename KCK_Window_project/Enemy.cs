using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    public abstract class Enemy
    {
        public abstract int GetX();
        public abstract int GetY();
        public abstract int GetDmg();
        public abstract int GetHP();
        public abstract bool GetStatus();
        public abstract string GetEnemyType();

        public abstract void SetHP(int hp);
        public abstract void SetX(int posX);
        public abstract void SetY(int posY);

        public abstract void Move();
        public abstract void Hit(int dmg);
        public abstract void ChangeStatus();
        public abstract Enemy Clone();
    }
}
