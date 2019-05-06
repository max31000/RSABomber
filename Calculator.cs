using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber
{
    public static class Calculator
    {
        public static float GetParallelogramSquare(Vector2 v1, Vector2 v2)
        {
            var angle = (float)Math.Acos(Vector2.Dot(v1, v2) / (v1.Length() * v2.Length()));
            return v1.Length() * v2.Length() * (float)Math.Sin(angle);
        }

        public static bool DotIsInsideRect(Vector2 dot, Vector2 rPos, int rWidth, int rHeight)
        {
            return rPos.X <= dot.X && rPos.X + rWidth >= dot.X && rPos.Y <= dot.Y && rPos.Y + rHeight >= dot.Y;
        }
    }
}
