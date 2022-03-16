using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Map
{
    public class Grid<T>
    {
        private T[,] Nodes;

        public int Width { get { return Nodes.GetLength(0); } }
        public int Height { get { return Nodes.GetLength(1); } }

        public Grid()
        {

        }
    }
}
