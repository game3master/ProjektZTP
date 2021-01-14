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
            int direction = rnd.Next(2);
            // <0, 70> - nie skakac
            // <71, 99> - skakac
            int shouldJump = rnd.Next(100);

            // Jezeli stoi pod sciana
            if (enemy.GetY() == 9)
                return;
            
            if (shouldJump >= 71 && shouldJump <= 99)
            {
                // Jezeli przeciwnik idzie przy lewej scianie
                if (enemy.GetX() == 0)
                {
                    // Sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() + 1] == '.')
                    {
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() + 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // Jezeli przeciwnik idzie przy prawej scianie
                if (enemy.GetX() == 9)
                {
                    // Sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() - 1] == '.')
                    {
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() - 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // Skok w lewo
                if (direction == 0 && enemy.GetX() - 1 >= 0)
                {
                    // Sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() - 1] == '.')
                    {
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() - 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
                // Skok w prawo
                if (direction == 1 && enemy.GetX() + 1 <= 9)
                {
                    // Sprawdzenie czy obok nie zajmuje ktos miejsca
                    if (GameBoard.board[enemy.GetY() + 1, enemy.GetX() + 1] == '.')
                    {
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        int newPosX = enemy.GetX() + 1;
                        int newPosY = enemy.GetY() + 1;
                        enemy.SetX(newPosX);
                        enemy.SetY(newPosY);
                        //GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                        return;
                    }
                }
            }
            else
            {
                //GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                enemy.Move();
                //GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
            }
        }
    }
}
