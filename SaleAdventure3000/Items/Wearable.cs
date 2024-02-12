﻿using SaleAdventure3000.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000.Items
{
    public class Wearable : Item
    {
        
        private readonly string[] symbols = [" H ", " B ", " N "];
        private readonly string[] names = ["Hat", "Boots", "Necklace"];
        private readonly int randomizedProperties = new Random().Next(0, 3);
        private readonly Dictionary<string, int[]> wearableProperties = new Dictionary<string, int[]>()
        {
            {" H ", [20, 6]},
            {" B ", [15, 10]},
            {" N ", [5, 2]}
            // Siffrorna i arrayen representerar Poweradded och HPBoost.
        };

        public Wearable()
        {
            Symbol = symbols[randomizedProperties];
            Name = names[randomizedProperties];
            PowerAdded = wearableProperties[Symbol].ElementAt(0);
            HpBoost = wearableProperties[Symbol].ElementAt(1);
            Wear = true;
        }

        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.Name} equips " +
                              $"{item.Name}, gaining {item.HpBoost} HP " +
                              $"and {item.PowerAdded} power.");

            player.HP += item.HpBoost;
            player.Power += item.PowerAdded;
            item.Equipped = true;
            AddToBag(item, player);
            
        }
    }
}
