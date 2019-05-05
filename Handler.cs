﻿using System;
using System.Collections.Generic;
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
        public static BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        public static BufferedGraphics buffer;
        private Form form;
        private Graphics graphics;
        private Game game;
        private string gameState;
        private Timer timer;

        public Handler(Form gameForm)
        {
            form = gameForm;
            graphics = form.CreateGraphics();
            buffer = context.Allocate(graphics, form.DisplayRectangle);
            timer = new Timer { Interval = 6 };
            timer.Tick += Update;
            timer.Start();
        }

        public void Start()
        {
            gameState = "menu";
        }


        
        private void StartGame()
        {
            gameState = "game";
            game = new Game();
            form.KeyDown += GameKeyDownHandler;
            form.KeyUp += GameKeyUpHandler;
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
            StartGame();
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
            buffer.Graphics.Clear(Color.LimeGreen);

            switch (gameState)
            {
                case "menu":
                    StartGame();
                    break;
                case "game":
                    if (game.Hero.IsDead)
                        gameState = "loose";
                    game.Update(buffer.Graphics);
                    break;
                case "loose":
                    Loose();
                    break;
            }

            buffer.Render();
        }
    }
}
