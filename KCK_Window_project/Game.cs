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

        Strategy strategy;

        public static int wood;
        public static int stone;
        public static int score;
        public static int hp;

        Timer enemyMoveTimer;

        List<Enemy> enemyList = new List<Enemy>();
        List<Block> blockList = new List<Block>();
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
                blockList.Add(null);
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
            this.enemyList = strategy.CreateEnemies(enemyTypes);
        }

        // Wykonanie metody strategii - ruch.
        public void MoveStrategy()
        {
            strategy.MoveEnemies(enemyList);
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

        // Rozmieszczenie bloków na mapce
        private void PlaceBlocks()
        {
            BlockFactory factory = new BlockFactory();
            int index = 1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (i == 0 || i == 9 || j == 0 || j == 14)
                    {
                        factory.AddBlock(index, i, j);
                        if (index == 1)
                        {
                            index = 2;
                        }
                        else
                        {
                            index = 1;
                        }
                    }

                }
            }
            factory.GetAllBlocks().ForEach(
                (Block block) => PlaceTurret(block)
            );
        }

        private void GameLoad(object sender, EventArgs e)
        {
            // Prototype.
            SetEnemy("basic", new BasicEnemy(0, 0));
            SetEnemy("red", new RedEnemy(0, 0));

            // Ustawienie strategii
            // 0 - poziom latwy.
            // 1 - poziom trudny.
            if (StartGame.GetDifficulty() == 1)
                SetStrategy(new HardStrategy());
            else
                SetStrategy(new EasyStrategy());

            // Observer.
            Subscribe(GameBoard.GetInstance());
            hero = Hero.getInstance();
            FillSquare();

            InitializeLists();

            // Flyweight
            PlaceBlocks();

            wood = 1500;
            stone = 1500;
            score = 0;
            hp = 100;


            enemyMoveTimer = new Timer();
            enemyMoveTimer.Interval = 700; //MS
            enemyMoveTimer.Tick += new EventHandler(EnemyMoveTimer_Tick);
            enemyMoveTimer.Start();
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
        public void PlaceTurret(Block block)
        {
            int inOne = block.GetY() * 10 + block.GetX();
            string searched = "label" + inOne + "ee";
            foreach (Label ctrl in this.Controls)
            {
                if (ctrl.Name == searched)
                {
                    ctrl.TextAlign = ContentAlignment.TopRight;
                    ctrl.Image = block.GetBlockType().GetImage();
                }
            }
        }


        // Obsluga klawiszy.
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    ClearSquare();
                    hero.MoveUp();
                    FillSquare();
                    
                    break;
                case Keys.Down:
                    ClearSquare();
                    hero.MoveDown();
                    FillSquare();
                    break;
                case Keys.Right:
                    ClearSquare();
                    hero.MoveRight();
                    FillSquare();
                    break;
                case Keys.Left:
                    ClearSquare();
                    hero.MoveLeft();
                    FillSquare();
                    break;
                default:
                    break;
            }
        }


        // Timer od poruszania sie przeciwnikow.
        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            enemyList.ForEach((Enemy enemy) =>
            {
                ClearSquare(enemy);
            });
            MoveStrategy();
            enemyList.ForEach((Enemy enemy) =>
            {
                FillSquare(enemy);
            });        
            NotifySubscribers();
            this.IsGameLost();
        }


        private void IsGameLost()
        {
            // Zakonczenie gry.
            if (hp <= 0)
            {
                enemyMoveTimer.Enabled = false;
                this.Close();
            }
        }

    }
}
