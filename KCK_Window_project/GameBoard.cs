using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Window_project
{
    class GameBoard : Subscriber
    {
        // 15 - wiersze, 10 - kolumny.
        public static char[,] board = new char[15, 10];
        private static GameBoard instance;

        /* Singleton - GameBoard */
        private GameBoard()
        {
            CreateBoard();
        }
        
        public static GameBoard GetInstance()
        {
            if (instance == null)
                instance = new GameBoard();
            return instance;
        }

        /* Metody */
        // Stworzenie logiki planszy (Tablica z rozmieszczeniem przeciwnikow).
        private void CreateBoard()
        {
           
        }

        // Nadpisana metoda z wzorca Observer - zaktualizowanie planszy.
        public override void update(Game game)
        {
           
        }
    }
}
