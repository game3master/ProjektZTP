using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Media;

namespace KCK_Window_project
{
    public partial class Game : Form
    {
        Hero hero;
        Resources resources;

        public static int wood = 1500;
        public static int stone = 1500;
        public static int score = 0;
        public static int hp = 100;

        Timer woodTimer;
        Timer stoneTimer;
        Timer enemyCreateTimer;
        Timer enemyMoveTimer;
        Timer enemyGetHit;
        Timer enemyAttack;

        List<Enemy> enemyList = new List<Enemy>();
        List<Turret> turretList = new List<Turret>();
        List<Enemy> enemiesUnderWall = new List<Enemy>();

        public Game()
        {
            InitializeComponent();
        }

        //inicjalizowanie list nullami
        private void InitializeLists()
        {
            for (int i = 0; i < 10; i++)
            {
                turretList.Add(null);
                enemiesUnderWall.Add(null);
            }

        }

        private void Game_Load(object sender, EventArgs e)
        {
            //hero = new Hero();
            hero = Hero.getInstance();
            //resources = new Resources();
            resources = Resources.getInstance();
            GameBoard.CreateBoard();
            InitializeLists();

            FillSquare();
            labelWood.Font = new Font("Arial", 24);
            labelWood.Text = "Drewno - " + wood.ToString();
            labelStone.Font = new Font("Arial", 24);
            labelStone.Text = "Kamień - " + stone.ToString();
            labelHP.Font = new Font("Arial", 24, FontStyle.Bold);
            labelHP.Text = "HP: " + hp.ToString();
            labelScore.Font = new Font("Segoe UI Black", 28);
            labelScore.Text = "Punkty: " + score.ToString();

            woodTimer = new Timer();
            woodTimer.Interval = 5000; //5 sec
            woodTimer.Tick += new EventHandler(WoodTimer_Tick);
            woodTimer.Start();
            stoneTimer = new Timer();
            stoneTimer.Interval = 5000; //5 sec
            stoneTimer.Tick += new EventHandler(StoneTimer_Tick);
            stoneTimer.Start();
            enemyCreateTimer = new Timer();
            enemyCreateTimer.Interval = 10000; //10 sec
            enemyCreateTimer.Tick += new EventHandler(EnemyCreateTimer_Tick);
            enemyCreateTimer.Start();
            enemyMoveTimer = new Timer();
            enemyMoveTimer.Interval = 2500; //2.5 sec
            enemyMoveTimer.Tick += new EventHandler(EnemyMoveTimer_Tick);
            enemyMoveTimer.Start();
            enemyGetHit = new Timer();
            enemyGetHit.Interval = 1000; //1 sec
            enemyGetHit.Tick += new EventHandler(EnemyGetHit_Tick);
            enemyGetHit.Start();
            enemyAttack = new Timer();
            enemyAttack.Interval = 5000;
            enemyAttack.Tick += new EventHandler(EnemyAttack_Tick);
            enemyAttack.Start();
        }

        //zamalowanie pola w miejscu bohatera
        public void FillSquare()
        {
            int posX = hero.GetX();
            int posY = hero.GetY();
            int inOne = posY * 10 + posX;
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.BackColor = Color.Red;
                }
            }
        }

        //zamalowanie pola w miejscu przeciwnika
        public void FillSquare(Enemy enemy)
        {
            int posX = enemy.GetX();
            int posY = enemy.GetY();
            int inOne = posY * 10 + posX;
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    //ctrl.BackColor = Color.Green;
                    //ctrl.Text = enemy.GetHP().ToString();
                    ctrl.Image = Properties.Resources.enemy;
                }
            }
        }

        //wyczyszczenie pola w miejscu bohatera
        public void ClearSquare()
        {
            int posX = hero.GetX();
            int posY = hero.GetY();
            int inOne = posY * 10 + posX;
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.BackColor = Color.Gray;
                }
            }
        }

        //wyczyszczenie pola w miejscu przeciwnika
        public void ClearSquare(Enemy enemy)
        {
            int posX = enemy.GetX();
            int posY = enemy.GetY();
            int inOne = posY * 10 + posX;
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.Image = null;
                    ctrl.BackColor = Color.Gray;
                    ctrl.Text = ".";
                }
            }
        }

        //namalowanie wiezyczki na mapie
        public void PlaceTurret(Turret turret)
        {
            int inOne = turret.GetY() * 10 + turret.GetX();
            string searched = "label" + inOne + "ee";
            //bez korzystania z gameboard
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    switch (turret.GetLevel())
                    {
                        case 1:
                            //ctrl.BackColor = Color.Aqua;
                            ctrl.Text = "1" + turret.GetAmmo();
                            ctrl.Image = Properties.Resources.turret_phase1;
                            break;
                        case 2:
                            //ctrl.BackColor = Color.DarkTurquoise;
                            ctrl.Text = "2" + turret.GetAmmo();
                            ctrl.Image = Properties.Resources.turret_phase2;
                            break;
                        default:
                            //ctrl.BackColor = Color.CornflowerBlue;
                            ctrl.Text = "3" + turret.GetAmmo();
                            ctrl.Image = Properties.Resources.turret_phase3;
                            break;
                    }
                }
            }
        }

        //wczyszczenie wiezyczki na mapie
        public void ClearTurret(Turret turret)
        {
            int inOne = turret.GetY() * 10 + turret.GetX();
            string searched = "label" + inOne + "ee";
            //bez korzystania z gameboard
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.BackColor = Color.DeepSkyBlue;
                    ctrl.Text = ".";
                    ctrl.Image = null;
                }
            }
        }

        //obsluga klawiszy
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            Stream str;
            SoundPlayer snd;
            switch (e.KeyData)
            {
                case Keys.Up:
                    ClearSquare();
                    hero.MoveUp();
                    FillSquare();
                    // dzwiek
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Down:
                    ClearSquare();
                    hero.MoveDown();
                    FillSquare();
                    // dzwiek
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Right:
                    ClearSquare();
                    hero.MoveRight();
                    FillSquare();
                    // dzwiek
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Left:
                    ClearSquare();
                    hero.MoveLeft();
                    FillSquare();
                    // dzwiek
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Enter:
                    int collected = hero.Collect(resources);
                    //jesli zebrano drewno
                    if (collected == 1)
                    {
                        labelWood.Text = "Drewno - " + wood;
                        //label140ee.BackColor = Color.PeachPuff;
                        //label140ee.Text = "1";
                        label140ee.Image = Properties.Resources.wood_phase1;
                        woodTimer.Start();
                        // dzwiek
                        str = Properties.Resources.collect_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    //jesli zebrano kamien
                    if (collected == 0)
                    {
                        labelStone.Text = "Kamień - " + stone;
                        //label149ee.BackColor = Color.LightSteelBlue;
                        //label149ee.Text = "1";
                        label149ee.Image = Properties.Resources.stone_phase1;
                        stoneTimer.Start();
                        // dzwiek
                        str = Properties.Resources.collect_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    //budowanie wiezyczki
                    if (hero.GetY() == 11 && hero.CanPlace(turretList) == true)
                    {
                        Turret turret = new Turret(hero);
                        turretList.RemoveAt(hero.GetX());
                        turretList.Insert(hero.GetX(), turret);
                        PlaceTurret(turret);
                        wood -= Turret.GetBuildCost();
                        stone -= Turret.GetBuildCost();
                        labelWood.Text = "Drewno - " + wood;
                        labelStone.Text = "Kamień - " + stone;
                        // dzwiek
                        str = Properties.Resources.build_upgrade_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    //ulepszanie wiezyczki
                    else if (hero.GetY() == 11 && hero.CanUpgrade(turretList) == true)
                    {
                        Turret turret = turretList.ElementAt(hero.GetX());
                        wood -= turret.GetUpgradeCost();
                        stone -= turret.GetUpgradeCost();
                        turret.Upgrade();
                        PlaceTurret(turret);
                        labelWood.Text = "Drewno - " + wood;
                        labelStone.Text = "Kamień - " + stone;
                        // dzwiek
                        str = Properties.Resources.build_upgrade_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    break;
                default:
                    break;
            }
        }

        /**********
         * Timery *
         **********/
        //Timer od drewna
        private void WoodTimer_Tick(object sender, EventArgs e)
        {
            //drewno
            resources.WoodNextPhase();
            switch (resources.GetCurrentPhase(1))   
            {
                case 0:
                    //label140ee.BackColor = Color.PeachPuff;
                    //label140ee.Text = "1";
                    label140ee.Image = Properties.Resources.wood_phase1;
                    break;
                case 1:
                    //label140ee.BackColor = Color.Peru;
                    //label140ee.Text = "2";
                    label140ee.Image = Properties.Resources.wood_phase2;
                    break;
                default:
                    //label140ee.BackColor = Color.SaddleBrown;
                    //label140ee.Text = "3";
                    label140ee.Image = Properties.Resources.wood_phase3;
                    break;
            }   
            if (resources.GetCurrentPhase(1) == 2)
                woodTimer.Stop();
        }

        //Timer od kamienia
        private void StoneTimer_Tick(object sender, EventArgs e)
        {
            //kamien
            resources.StoneNextPhase();
            switch (resources.GetCurrentPhase(0))
            {
                case 0:
                    //label149ee.BackColor = Color.LightSteelBlue;
                    //label149ee.Text = "1";
                    label149ee.Image = Properties.Resources.stone_phase1;
                    break;
                case 1:
                    //label149ee.BackColor = Color.Silver;
                    //label149ee.Text = "2";
                    label149ee.Image = Properties.Resources.stone_phase2;
                    break;
                default:
                    //label149ee.BackColor = Color.LightSlateGray;
                    //label149ee.Text = "3";
                    label149ee.Image = Properties.Resources.stone_phase3;
                    break;
            }
            if (resources.GetCurrentPhase(0) == 2)
                stoneTimer.Stop();
        }
        //Timer od tworzenia przeciwnikow
        private void EnemyCreateTimer_Tick(object sender, EventArgs e)
        {
            Enemy enemy = new Enemy();
            enemyList.Add(enemy);
            GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
            FillSquare(enemy);
        }
        //Timer od poruszania sie przeciwnikow
        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            if (enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.GetStatus() == true)
                    {
                        enemy.ChangeStatus();
                    }
                    else
                    {
                        ClearSquare(enemy);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                        enemy.Move(GameBoard.board);
                        FillSquare(enemy);
                        GameBoard.board[enemy.GetY(), enemy.GetX()] = '@';
                    }
                    if (enemy.GetY() == 9)
                    {
                        enemiesUnderWall.RemoveAt(enemy.GetX());
                        enemiesUnderWall.Insert(enemy.GetX(), enemy);
                    }
                }
            }
        }
        //Timer od zadawania obrazen przeciwnikom
        private void EnemyGetHit_Tick(object sender, EventArgs e)
        {
            List<Enemy> supportList = new List<Enemy>();
            bool shouldRefresh = false;
            foreach (Enemy enemy in enemiesUnderWall)
            {
                //jesli na scianie stoi wiezyczka przed przeciwnikiem
                if (enemy != null && turretList.ElementAt(enemy.GetX()) != null)
                {
                    Turret turret = turretList.ElementAt(enemy.GetX());
                    enemy.Hit(turret.GetDmg());
                    FillSquare(enemy);
                    //wiezyczka traci pocisk
                    turret.Shot();
                    //odswiezenie magazynku
                    PlaceTurret(turret);
                    //jesli wiezyczka nie ma juz naboi
                    if (turret.GetAmmo() <= 0)
                    {
                        turretList.RemoveAt(enemy.GetX());
                        turretList.Insert(enemy.GetX(), null);
                        ClearTurret(turret);
                    }
                }
                //jesli hp przeciwnika spadnie do 0
                //pomocnicza lista do usuwania przeciwnikow
                if (enemy != null && enemy.GetHP() <= 0)
                {
                    supportList.Add(enemy);
                    shouldRefresh = true;
                    score += 250;
                    labelScore.Text = "Punkty: " + score.ToString();
                    // dzwiek
                    Stream str = Properties.Resources.enemy_destroyed_sound;
                    SoundPlayer snd = new SoundPlayer(str);
                    snd.Play();
                }
            }
            if (shouldRefresh == true)
            {
                foreach (Enemy enemy in supportList)
                {
                    ClearSquare(enemy);
                    GameBoard.board[enemy.GetY(), enemy.GetX()] = '.';
                    enemiesUnderWall.RemoveAt(enemy.GetX());
                    enemiesUnderWall.Insert(enemy.GetX(), null);
                    enemyList.Remove(enemy);
                }
            }
        }
        //Timer od zadawania obrazen przez przeciwnikow
        private void EnemyAttack_Tick(object sender, EventArgs e)
        {
            bool playSound = false;
            foreach (Enemy enemy in enemyList)
            {
                if (enemy.GetY() == 9)
                {
                    hp -= enemy.GetDmg();
                    labelHP.Text = "HP: " + hp.ToString();
                    playSound = true;
                }
            }
            if (playSound == true)
            {
                // dzwiek
                Stream str = Properties.Resources.dmg_dealt_sound;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
            }
        }
    }
}
