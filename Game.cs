﻿using System;
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
        private List<IGameObject> gameObjects;
        private InGameForm gForm;
        private Painter painter;

        public Game(InGameForm gForm)
        {
            gameObjects = new List<IGameObject>();
            Hero = new Player(new Vector2(100, 100), 32, 42) {Speed = 3f};
            gameObjects.Add(Hero);
            gameObjects.AddRange(MapLoader.Load(@"Maps/1.map.txt"));
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
                    gameObjects.RemoveAt(i);
            }

            painter.Draw();
        }
    }
}