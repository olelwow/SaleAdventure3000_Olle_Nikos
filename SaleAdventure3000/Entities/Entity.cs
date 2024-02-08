using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000.Entities
{
    internal class Entity : Game
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }

    }
}
