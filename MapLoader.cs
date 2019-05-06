using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSABomber.Classes;
using static System.IO.File;

namespace RSABomber
{
    internal static class MapLoader
    {
        private static readonly int width = 16;
        private static readonly int height = 16;
        private static int screenW = 740;
        private static int screenH = 740;
        private static int step = screenH / width;


        internal static List<IGameObject> Parse(string s, int lineNumber)
        {
            List<IGameObject> objects = new List<IGameObject>();

            for (var i = 0; i < width; i++)
            {
                switch (s[i])
                {
                    case 'W':
                        objects.Add(new Wall(i * step, lineNumber * step, step, step));
                        break;
                    case 'S':
                        objects.Add(new Station(i * step + 3, lineNumber * step));
                        break;
                }
            }

            return objects;
        }

        public static List<IGameObject> Load(string path)
        {
            var objects = new List<IGameObject>();

            using (StreamReader sr = new StreamReader(path))
            {
                for (var i = 0; i < height; i++)
                    objects.AddRange(Parse(sr.ReadLine(), i));
            }
            return objects;
        }
    }
}
