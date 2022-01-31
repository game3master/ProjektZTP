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
        Strategy strategy;

        public static int wood;
        public static int stone;
        public static int score;
        public static int hp;

        //Timer woodTimer;
        //Timer stoneTimer;
        Timer enemyCreateTimer;
        Timer enemyMoveTimer;
        //Timer enemyGetHit;
        //Timer enemyAttack;

        List<Enemy> enemyList = new List<Enemy>();
        List<Turret> turretList = new List<Turret>();
        List<Enemy> enemiesUnderWall = new List<Enemy>();

        // Kolekcja dla wzorca Protoype.
        Dictionary<string, Enemy> enemyTypes = new Dictionary<string, Enemy>();
        // Kolekcja dla wzorca Observer.
        List<Subscriber> subscribers = new List<Subscriber>();

        public Game()
        {
            InitializeComponent();
        }

        /* Gettery */
        public List<Enemy> GetEnemyList()
        {
            return enemyList;
        }

        // Inicjalizowanie list nullami.
        private void InitializeLists()
        {
            for (int i = 0; i < 10; i++)
            {
                turretList.Add(null);
                enemiesUnderWall.Add(null);
            }
        }

        // Dodanie subskrybenta - wzorzec Observer.
        public void Subscribe(Subscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        // Usuniecie subskrybenta - wzorzec Observer.
        public void Unsubscribe(Subscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        // Powiadomienie subskrybentow - wzorzecz Observer.
        public void NotifySubscribers()
        {
            foreach (Subscriber s in subscribers)
            {
                s.update(this);
            }
        }

        // Ustawienie strategii - wzorzec Strategia.
        public void SetStrategy(Strategy strategy)
        {
            this.strategy = strategy;
        }

        // Wykonanie metody strategii - ruch.
        public void MoveStrategy(Enemy enemy)
        {
            strategy.Move(enemy);
        }

        // Indexer od wzorca Prototype - pobranie przeciwnika.
        public Enemy GetEnemy(string key)
        {
            return enemyTypes[key];
        }

        // Indexer od wzorca Prototype - dodanie typu przeciwnika.
        public void SetEnemy(string key, Enemy enemy)
        {
            enemyTypes.Add(key, enemy);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            // Ustawienie strategii
            // 0 - poziom latwy.
            // 1 - poziom trudny.
            if (StartGame.GetDifficulty() == 1)
                SetStrategy(new HardStrategy());
            else
                SetStrategy(new EasyStrategy());

            // Observer.
            Subscribe(GameBoard.GetInstance());

            //SetStrategy(new HardStrategy());
            hero = Hero.getInstance();
            resources = Resources.getInstance();
            FillSquare();

            InitializeLists();

            // Prototype.
            SetEnemy("basic", new BasicEnemy());
            SetEnemy("tank", new TankEnemy());

            wood = 1500;
            stone = 1500;
            score = 0;
            hp = 100;

            // Inicjalizacja labeli.
            labelWood.Font = new Font("Arial", 24);
            labelWood.Text = "Drewno - " + wood.ToString();
            labelStone.Font = new Font("Arial", 24);
            labelStone.Text = "Kamień - " + stone.ToString();
            labelHP.Font = new Font("Arial", 24, FontStyle.Bold);
            labelHP.Text = "HP: " + hp.ToString();
            labelScore.Font = new Font("Segoe UI Black", 28);
            labelScore.Text = "Punkty: " + score.ToString();

            // Inicjalizacja timerow.
            //woodTimer = new Timer();
            //woodTimer.Interval = 5000;          // 5 sec
            //woodTimer.Tick += new EventHandler(WoodTimer_Tick);
            //woodTimer.Start();
            //stoneTimer = new Timer();
            //stoneTimer.Interval = 5000;         // 5 sec
            //stoneTimer.Tick += new EventHandler(StoneTimer_Tick);
            //stoneTimer.Start();
            enemyCreateTimer = new Timer();
            enemyCreateTimer.Interval = 10000;  // 10 sec
            enemyCreateTimer.Tick += new EventHandler(EnemyCreateTimer_Tick);
            enemyCreateTimer.Start();
            enemyMoveTimer = new Timer();
            enemyMoveTimer.Interval = 2500;     // 2.5 sec
            enemyMoveTimer.Tick += new EventHandler(EnemyMoveTimer_Tick);
            enemyMoveTimer.Start();
            //enemyGetHit = new Timer();
            //enemyGetHit.Interval = 1000;        // 1 sec
            //enemyGetHit.Tick += new EventHandler(EnemyGetHit_Tick);
            //enemyGetHit.Start();
            //enemyAttack = new Timer();
            //enemyAttack.Interval = 5000;        // 5 sec.
            //enemyAttack.Tick += new EventHandler(EnemyAttack_Tick);
            //enemyAttack.Start();
        }

        // Zamalowanie pola w miejscu bohatera.
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
                    ctrl.Image = Properties.Resources.hero;
                }
            }
        }

        // Zamalowanie pola w miejscu przeciwnika.
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
                    if (enemy.GetEnemyType() == "basic")
                        ctrl.Image = Properties.Resources.enemy;
                    else
                        ctrl.Image = Properties.Resources.enemyTank;
                }
            }
        }

        // Wyczyszczenie pola w miejscu bohatera.
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
                    ctrl.Image = null;
                }
            }
        }

        // Wyczyszczenie pola w miejscu przeciwnika.
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
                }
            }
        }

        // Namalowanie wiezyczki na mapie.
        public void PlaceTurret(Turret turret)
        {
            int inOne = turret.GetY() * 10 + turret.GetX();
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.Text = turret.GetAmmo().ToString();
                    ctrl.TextAlign = ContentAlignment.TopRight;
                    ctrl.Image = turret.GetTurretType().GetImage();
                }
            }
        }
        
        // Wyczyszczenie wiezyczki na mapie.
        public void ClearTurret(Turret turret)
        {
            int inOne = turret.GetY() * 10 + turret.GetX();
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.BackColor = Color.DarkBlue;
                    ctrl.TextAlign = ContentAlignment.MiddleCenter;
                    ctrl.Text = ".";
                    ctrl.Image = null;
                }
            }
        }

        // Obsluga klawiszy.
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
                    // Dzwiek.
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Down:
                    ClearSquare();
                    hero.MoveDown();
                    FillSquare();
                    // Dzwiek.
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Right:
                    ClearSquare();
                    hero.MoveRight();
                    FillSquare();
                    // Dzwiek.
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Left:
                    ClearSquare();
                    hero.MoveLeft();
                    FillSquare();
                    // Dzwiek.
                    str = Properties.Resources.player_move_sound;
                    snd = new SoundPlayer(str);
                    snd.Play();
                    break;
                case Keys.Enter:
                    int collected = hero.Collect(resources);
                    // Jesli zebrano drewno.
                    if (collected == 1)
                    {
                        labelWood.Text = "Drewno - " + wood;
                        label140ee.Image = Properties.Resources.wood_phase1;
                        //woodTimer.Start();
                        // Dzwiek.
                        str = Properties.Resources.collect_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    // Jesli zebrano kamien.
                    if (collected == 0)
                    {
                        labelStone.Text = "Kamień - " + stone;
                        label149ee.Image = Properties.Resources.stone_phase1;
                        //stoneTimer.Start();
                        // Dzwiek.
                        str = Properties.Resources.collect_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }

                    //Jeśli zebrano Najmana
                    if (collected == 3)
                    {
                        hp = 0;
                    }

                    // Budowanie wiezyczki.
                    if (hero.GetY() == 11 && hero.CanPlace(turretList) == true)
                    {
                        TurretType type = TurretFactory.GetTurretType(1, 50, "phase_1", Properties.Resources.turret_phase1);
                        Turret turret = new Turret(hero, type);
                        turretList.RemoveAt(hero.GetX());
                        turretList.Insert(hero.GetX(), turret);
                        PlaceTurret(turret);

                        wood -= Turret.GetBuildCost();
                        stone -= Turret.GetBuildCost();
                        labelWood.Text = "Drewno - " + wood;
                        labelStone.Text = "Kamień - " + stone;
                        // Dzwiek.
                        str = Properties.Resources.build_upgrade_sound;
                        snd = new SoundPlayer(str);
                        snd.Play();
                    }
                    // Ulepszanie wiezyczki.
                    else if (hero.GetY() == 11 && hero.CanUpgrade(turretList) == true)
                    {
                        Turret turret = turretList.ElementAt(hero.GetX());
                        wood -= turret.GetUpgradeCost();
                        stone -= turret.GetUpgradeCost();
                        TurretType type = turret.Upgrade();
                        Turret upgradedTurret = new Turret(hero, type);
                        turretList.RemoveAt(hero.GetX());
                        turretList.Insert(hero.GetX(), upgradedTurret);
                        PlaceTurret(upgradedTurret);

                        labelWood.Text = "Drewno - " + wood;
                        labelStone.Text = "Kamień - " + stone;
                        // Dzwiek.
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
        // Timer od drewna.
        //private void WoodTimer_Tick(object sender, EventArgs e)
        //{
        //    // Drewno.
        //    resources.WoodNextPhase();
        //    switch (resources.GetCurrentPhase(1))   
        //    {
        //        case 0:
        //            label140ee.Image = Properties.Resources.wood_phase1;
        //            break;
        //        case 1:
        //            label140ee.Image = Properties.Resources.wood_phase2;
        //            break;
        //        default:
        //            label140ee.Image = Properties.Resources.wood_phase3;
        //            break;
        //    }   
        //    if (resources.GetCurrentPhase(1) == 2)
        //        //woodTimer.Stop();
        //}

        // Timer od kamienia.
        //private void StoneTimer_Tick(object sender, EventArgs e)
        //{
        //    // Kamien.
        //    resources.StoneNextPhase();
        //    switch (resources.GetCurrentPhase(0))
        //    {
        //        case 0:
        //            label149ee.Image = Properties.Resources.stone_phase1;
        //            break;
        //        case 1:
        //            label149ee.Image = Properties.Resources.stone_phase2;
        //            break;
        //        default:
        //            label149ee.Image = Properties.Resources.stone_phase3;
        //            break;
        //    }
        //    if (resources.GetCurrentPhase(0) == 2)
        //        stoneTimer.Stop();
        //}

        // Timer od tworzenia przeciwnikow.
        private void EnemyCreateTimer_Tick(object sender, EventArgs e)
        {
            Random type = new Random();
            Enemy enemy;
            if (type.Next(2) == 0)
                enemy = GetEnemy("basic").Clone() as BasicEnemy;
            else
                enemy = GetEnemy("tank").Clone() as TankEnemy;
            //Random rnd = new Random();
            int startX = 8;
            enemy.SetX(startX);
            enemyList.Add(enemy);
            FillSquare(enemy);
            NotifySubscribers();
        }

        // Timer od poruszania sie przeciwnikow.
        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            if (enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    // Przeciwnik dopiero zostal stworzony
                    if (enemy.GetStatus() == true)
                    {
                        enemy.ChangeStatus();
                    }
                    else
                    {
                        ClearSquare(enemy);
                        MoveStrategy(enemy);
                        FillSquare(enemy);
                    }
                    // Przeciwnik stoi pod sciana.
                    if (enemy.GetY() == 9)
                    {
                        enemiesUnderWall.RemoveAt(enemy.GetX());
                        enemiesUnderWall.Insert(enemy.GetX(), enemy);
                    }
                }
            }
            NotifySubscribers();
            this.IsGameLost();
        }

        //Timer od zadawania obrazen przeciwnikom.
        //private void EnemyGetHit_Tick(object sender, EventArgs e)
        //{
        //    // Pomocnicza lista do usuwania przeciwnikow.
        //    List<Enemy> supportList = new List<Enemy>();
        //    bool shouldRefresh = false;
        //    foreach (Enemy enemy in enemiesUnderWall)
        //    {
        //        // Jesli na scianie stoi wiezyczka przed przeciwnikiem.
        //        if (enemy != null && turretList.ElementAt(enemy.GetX()) != null)
        //        {
        //            Turret turret = turretList.ElementAt(enemy.GetX());
        //            enemy.Hit(turret.GetTurretType().GetDmg());
        //            FillSquare(enemy);
        //            turret.Shot();
        //            PlaceTurret(turret);
        //            // Jesli wiezyczka nie ma juz naboi.
        //            if (turret.GetAmmo() <= 0)
        //            {
        //                turretList.RemoveAt(enemy.GetX());
        //                turretList.Insert(enemy.GetX(), null);
        //                ClearTurret(turret);
        //            }
        //        }
        //        // Jesli hp przeciwnika spadnie do 0.
        //        if (enemy != null && enemy.GetHP() <= 0)
        //        {
        //            supportList.Add(enemy);
        //            shouldRefresh = true;
        //            score += 250;
        //            labelScore.Text = "Punkty: " + score.ToString();
        //            // Dzwiek.
        //            Stream str = Properties.Resources.enemy_destroyed_sound;
        //            SoundPlayer snd = new SoundPlayer(str);
        //            snd.Play();
        //        }
        //    }
        //    // Czy jakis przeciwnik zostal pokonany
        //    if (shouldRefresh == true)
        //    {
        //        foreach (Enemy enemy in supportList)
        //        {
        //            ClearSquare(enemy);
        //            enemiesUnderWall.RemoveAt(enemy.GetX());
        //            enemiesUnderWall.Insert(enemy.GetX(), null);
        //            enemyList.Remove(enemy);
        //        }
        //        NotifySubscribers();
        //    }
        //}

        // Timer od zadawania obrazen przez przeciwnikow.
        //private void EnemyAttack_Tick(object sender, EventArgs e)
        //{
        //    bool playSound = false;
        //    foreach (Enemy enemy in enemyList)
        //    {
        //        if (enemy.GetY() == 9)
        //        {
        //            hp -= enemy.GetDmg();
        //            labelHP.Text = "HP: " + hp.ToString();
        //            playSound = true;
        //        }
        //    }
        //    if (playSound == true)
        //    {
        //        // Dzwiek.
        //        Stream str = Properties.Resources.dmg_dealt_sound;
        //        SoundPlayer snd = new SoundPlayer(str);
        //        snd.Play();
        //    }

        private void IsGameLost()
        {
            // Zakonczenie gry.
            if (hp <= 0)
            {
                // Dzwiek.
                Stream str = Properties.Resources.gameover;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
                //woodTimer.Enabled = false;
                //stoneTimer.Enabled = false;
                //enemyCreateTimer.Enabled = false;
                enemyMoveTimer.Enabled = false;
                //enemyGetHit.Enabled = false;
                //enemyAttack.Enabled = false;
                this.Close();
            }
        }
            
        //}

    }
}
