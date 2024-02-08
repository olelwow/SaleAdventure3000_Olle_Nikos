using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000.Items
{
    public class Consumable : Item
    {
        public Consumable()
        {
            this.Name = ConsumableNames[randomThing];
            this.Symbol = ConsumableSymbols[randomThing];
            this.HealAmount = ConsumableAttributes[this.Symbol];
            this.Amount = 1;
            this.Wear = false;
        }

        string[] ConsumableNames = ["Egg", "Pie", "Cheese"];
        string[] ConsumableSymbols = [" E ", " P ", " C "];
        public Dictionary<string, int> ConsumableAttributes = new Dictionary<string, int>()
        {
            {" E ", 10 },
            {" P ", 20},
            {" C ", 30}
        };
        // Egg ger 10hp, Pie ger 20, Cheese ger 30. 
        private int Amount { get; set; }
        int randomThing = new Random().Next(0, 3);

    }
}
