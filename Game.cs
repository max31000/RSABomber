using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using RSABomber.Classes;

namespace RSABomber
{
    public class Game
    {
        public Player Hero;
        internal List<IGameObject> gameObjects;
        private InGameForm gForm;
        private Painter painter;
        private int score;

        public Game(InGameForm gForm, string levelPath)
        {
            gameObjects = new List<IGameObject>();
            Hero = new Player(new Vector2(50, 100), 32, 42) {Speed = 3f};
            gameObjects.Add(Hero);
            gameObjects.AddRange(MapLoader.Load(levelPath));
            this.gForm = gForm;
            painter = new Painter(gameObjects, gForm);
        }
  
        internal void SetBomb()
        {
            var b = new Bomb((int)Hero.Position.X, (int)Hero.Position.Y + 5, 40,  40);
            gameObjects.Add(b);
        }

        internal void Update()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameObjects);
            }
            
            for (var i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].IsDead)
                {
                    if (gameObjects[i].Type == typeof(Station))
                    {
                        score += 40;
                        painter.DrawScore(score);
                    }

                    if (gameObjects[i].Type == typeof(Enemy))
                    {
                        score += 25;
                        painter.DrawScore(score);
                    }

                    gameObjects.RemoveAt(i);
                }
            }

            painter.Draw();
        }
    }
}