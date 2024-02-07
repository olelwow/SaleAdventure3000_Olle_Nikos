using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000
{
    internal class Creature : Game
    {
        public Creature() { }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string? Name { get; set; }
        public int HP { get; set; }
        public int Power { get; set; }
        public string? Symbol { get; set; }

        


    }
}