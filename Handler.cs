using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSABomber
{
    public class Handler
    {
        private InGameForm gameForm;
        private Game game;
        private string gameState;
        private Timer timer;
        private MenuForm menuForm;

        public Handler()
        {
            timer = new Timer { Interval = 6 };
            menuForm = new MenuForm(this);
        }

        private void LoadGameForm()
        {
            gameForm = new InGameForm();
            gameForm.Closing += CloseGameForm;
        }

        private void CloseGameForm(object sender, CancelEventArgs e) => Loose();

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
            gameState = "game";
            game = new Game(gameForm);
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
            timer.Stop();
            gameForm.Hide();
            menuForm.Show();
            menuForm.Activate();
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
                case "game":
                    if (game.Hero.IsDead)
                        gameState = "loose";
                    game.Update();
                    break;
                case "loose":
                    Loose();
                    break;
            }
        }
    }
}
