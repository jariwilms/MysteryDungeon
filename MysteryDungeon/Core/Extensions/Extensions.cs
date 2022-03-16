using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Map;
using System;
using System.Collections.Generic;
using System.IO;

namespace MysteryDungeon.Core.Extensions
{
    public static class Extensions
    {
        public static Rectangle Scale(this Rectangle rectangle, float scale)
        {
            rectangle.Width = (int)(rectangle.Width * scale);
            rectangle.Height = (int)(rectangle.Height * scale);

            return rectangle;
        }

        public static char[,] LoadMapFromFile(string DungeonPath)
        {
            using StreamReader reader = new StreamReader(DungeonPath); //Change to Dungeon name
            List<string> lines = new List<string>();
            string line = reader.ReadLine();
            int lineWidth = line.Length;

            while (line != null)
            {
                if (line.Length != lineWidth)
                    throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));

                lines.Add(line);
                line = reader.ReadLine();
            }

            char[,] charmap = new char[lineWidth, lines.Count];

            for (int y = 0; y < lines.Count; y++)
                for (int x = 0; x < lineWidth; x++)
                    charmap[x, y] = lines[y][x];

            return charmap;
        }
    }
}
