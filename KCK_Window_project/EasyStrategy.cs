using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class EasyStrategy : Strategy
    {
        void Strategy.Move(Enemy enemy)
        {
            GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
            enemy.Move();
            GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
        }
    }
}
