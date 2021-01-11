using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class HardStrategy : Strategy
    {
        Random rnd = new Random();

        void Strategy.Move(Enemy enemy)
        {
            // 0 - lewo
            // 1 - prawo
            int direction = rnd.Next() % 2;
            // <0, 70> - nie skakac
            // <71, 99> - skakac
            int shouldJump = rnd.Next() % 100;

            
            if (shouldJump >= 71 && shouldJump <= 99)
            {
                // jezeli przeciwnik idzie przy lewej scianie
                if (enemy.GetX() == 0)
                {
                    // sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() + 1] == '.')
                    {
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() + 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // jezeli przeciwnik idzie przy prawej scianie
                if (enemy.GetX() == 9)
                {
                    // sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() - 1] == '.')
                    {
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() - 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // skok w lewo
                if (direction == 0)
                {
                    // sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() - 1] == '.')
                    {
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() - 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // skok w prawo
                if (direction == 1)
                {
                    // sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() + 1] == '.')
                    {
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() + 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
            }
            else
            {
                enemy.Move(GameBoard.board);
            }
        }
    }
}
