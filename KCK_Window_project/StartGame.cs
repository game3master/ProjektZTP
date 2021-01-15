using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCK_Window_project
{
    public partial class StartGame : Form
    {
        private static int difficulty;

        /* Konstruktor */
        public StartGame()
        {
            InitializeComponent();
        }

        /* Getter */
        public static int GetDifficulty()
        {
            return difficulty;
        }

        /* Eventy */
        private void buttonEasy_Click(object sender, EventArgs e)
        {
            difficulty = 0;
            this.Close();
        }

        private void buttonHard_Click(object sender, EventArgs e)
        {
            difficulty = 1;
            this.Close();
        }
    }
}
