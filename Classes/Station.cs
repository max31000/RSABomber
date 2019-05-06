using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    public class Station : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public Type Type { get; }
        public bool IsDead { get; set; }

        public Station(int xPos, int yPos)
        {
            Width = 40;
            Height = 40;
            Position = new Vector2(xPos, yPos);
            Type = GetType();
            Collider = new BoxCollider(xPos, yPos, 40, 40);
            IsDead = false;
        }

        public void Update(List<IGameObject> objects){ }
    }
}
