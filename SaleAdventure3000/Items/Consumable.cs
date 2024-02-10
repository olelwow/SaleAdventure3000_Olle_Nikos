using SaleAdventure3000.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000.Items
{
    public class Consumable : Item
    {
        private readonly string[] ConsumableNames = ["Egg", "Pie", "Cheese"];
        private readonly string[] ConsumableSymbols = [" E ", " P ", " C "];
        private readonly Dictionary<string, int> ConsumableAttributes = new Dictionary<string, int>()
        {
            {" E ", 10 },
            {" P ", 20},
            {" C ", 30}
            // Egg ger 10hp, Pie ger 20, Cheese ger 30. 
        };
        public int Amount { get; set; }
        private readonly int randomizedProperties = new Random().Next(0, 3);
        public Consumable()
        {
            this.Name = ConsumableNames[randomizedProperties];
            this.Symbol = ConsumableSymbols[randomizedProperties];
            this.HealAmount = ConsumableAttributes[this.Symbol];
            this.Amount = 1;
            this.Wear = false;
        }
        
        public override void OnPickup (Consumable item, Player player)
        {
            
                Console.WriteLine($"{player.Name} picked up a {item.Name}");
                if (!player.Bag.ContainsKey(item))
                {
                    //item.Amount
                    player.Bag.Add(item, item.Amount);
                }
                else
                {
                    //player.Bag.Add(item, 1);
                    player.Bag[item]++;
                    // FIXA DETTA, IDIOT!
                }
            
            
        }
        
    }
}
