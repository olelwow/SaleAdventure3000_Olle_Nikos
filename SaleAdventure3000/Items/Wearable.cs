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
        string[] symbols = [" H ", " B ", " N "];
        string[] names = ["Hat", "Boots", "Necklace"];
        int randomThing = new Random().Next(0, 3);
        Dictionary<string, int[]> wearbleProperties = new Dictionary<string, int[]>()
        {
            {" H ", [20, 6]},
            {" B ", [15, 10]},
            {" N ", [5, 2]}
            // Siffrorna i arrayen representerar Poweradded och HPBoost.

        };

        public Wearable()
        {
            Symbol = symbols[randomThing];
            PowerAdded = wearbleProperties[Symbol].ElementAt(0);
            HpBoost = wearbleProperties[Symbol].ElementAt(1);
            Name = names[randomThing];
        }
    }
}
