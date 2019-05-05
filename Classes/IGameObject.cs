using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RSABomber.Classes
{
    public interface IGameObject
    {
        Vector2 Position { get; set; }
        List<IGameObject> Children { get; }
        int Width { get; }
        int Height { get; }
        Vector2 Direction { get; set; }
        BoxCollider Collider { get; set; }
        bool IsDead { get; set; }

        void Update(List<IGameObject> objects);
        void Draw(Graphics g);
    }
}
