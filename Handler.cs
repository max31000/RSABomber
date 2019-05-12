using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RSABomber.Classes;

namespace RSABomber
{
    public class Handler
    {
        private InGameForm gameForm;
        private Game game;
        private State gameState;
        private Timer timer;
        private MenuForm menuForm;
        private string[] levels;
        private int currentLevel;

        private enum State { Game, Loose, Win, Wait }

        public Handler()
        {
            timer = new Timer { Interval = 6 };
            menuForm = new MenuForm(this);
            levels = new string[3];
            for (var i = 1; i < levels.Length; i++)
                levels[i] = "Maps/" + i + ".map.txt";
        }

        private void LoadGameForm()
        {
            gameForm = new InGameForm();
            gameForm.Closing += (sender, e) => Loose();
        }

        public void Start()
        {
            Application.Run(menuForm);
        }
        
        internal void StartGame()
        {
            menuForm.Hide();
            LoadGameForm();
            gameForm.Show();
            gameForm.Activate();
            timer.Tick += Update;
            timer.Start();
            NextLevel();
            gameForm.KeyDown += GameKeyDownHandler;
            gameForm.KeyUp += GameKeyUpHandler;
        }

        private void GameKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (game is null)
                return;

            switch (e.KeyData)
            {
                case Keys.W:
                    game.Hero.Direction = new Vector2(Math.Sign(game.Hero.Direction.X), -1);
                    break;
                case Keys.A:
                    game.Hero.Direction = new Vector2(-1, Math.Sign(game.Hero.Direction.Y));
                    break;
                case Keys.S:
                    game.Hero.Direction = new Vector2(Math.Sign(game.Hero.Direction.X), 1);
                    break;
                case Keys.D:
                    game.Hero.Direction = new Vector2(1, Math.Sign(game.Hero.Direction.Y));
                    break;
                case Keys.Space:
                    game.SetBomb();
                    break;
            }
        }

        private void Loose()
        {
            timer.Tick -= Update;
            currentLevel = 0;
            timer.Stop();
            gameForm.Hide();
            menuForm.Show();
            menuForm.Activate();
        }

        private void NextLevel()
        {
            currentLevel++;
            if (currentLevel >= levels.Length)
            {
                gameState = State.Wait;
                MessageBox.Show("Вы выиграли!",
                                "Конгратулейшенс");
                Loose();
                return;
            }
            game = new Game(gameForm, levels[currentLevel]);
            gameState = State.Game;
        }

        private void GameKeyUpHandler(object sender, KeyEventArgs e)
        {
            if (game is null)
                return;

            if (e.KeyData == Keys.W ||
                e.KeyData == Keys.S)
                game.Hero.Direction = new Vector2(game.Hero.Direction.X, 0);
            if (e.KeyData == Keys.A ||
                e.KeyData == Keys.D)
                game.Hero.Direction = new Vector2(0, game.Hero.Direction.Y);
        }

        private void Update(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case State.Game:
                    if (game.Hero.IsDead)
                        gameState = State.Loose;
                    if (game.gameObjects.Count(x => x.Type == typeof(Station)) == 0)
                        gameState = State.Win;
                    game.Update();
                    break;
                case State.Loose:
                    Loose();
                    break;
                case State.Win:
                    NextLevel();
                    break;
                case State.Wait:
                    break;
            }
        }
    }
}
