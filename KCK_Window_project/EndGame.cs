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
    public partial class EndGame : Form
    {
        private static bool closeProgram = true;

        /* Konstruktor */
        public EndGame()
        {
            InitializeComponent();
        }

        /* Getter */
        public static bool GetDecision()
        {
            return closeProgram;
        }

        /* Eventy */
        private void EndGame_Load(object sender, EventArgs e)
        {
            int score = Game.score;
            labelPoints.Text = "Twój wynik: " + score.ToString() + " pkt.";
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            closeProgram = false;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            closeProgram = true;
            this.Close();
        }
    }
}
