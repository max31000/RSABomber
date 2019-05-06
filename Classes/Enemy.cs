using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    class Enemy : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public Type Type { get; }

        public void Update(List<IGameObject> objects)
        {
            throw new NotImplementedException();
        }

        public void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
