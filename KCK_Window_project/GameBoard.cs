using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class GameBoard
    {
        public static char[,] board = new char[15, 10]; //wiersze, kolumny

        public static void CreateBoard()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = '.';
                    if (i == 10)
                        board[i, j] = '#';
                }
            }
            board[14, 0] = 'v';
            board[14, 9] = 's';
        }
    }
}
