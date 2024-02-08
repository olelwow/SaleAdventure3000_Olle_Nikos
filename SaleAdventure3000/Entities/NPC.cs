using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000.Entities
{
    public class NPC : Creature
    {
        string[] symbols = [" * ", " @ ", " # ", " & ", " £ "];
        string[] names = ["Ragnar", "Klasse", "Nikos", "Olle", "Jonas"];
        int randomThing = new Random().Next(0, 5);
        Dictionary<string, int[]> NPCProperties = new Dictionary<string, int[]>()
        {
            {" * ", [100, 12]},
            {" @ ", [75, 20]},
            {" # ", [150, 7]},
            {" & ", [60, 15]},
            {" £ ", [30, 5] }
            // Siffrorna i arrayen representerar HP och Power.

        };

        public NPC()
        {
            Symbol = symbols[randomThing];
            HP = NPCProperties[Symbol].ElementAt(0);
            Power = NPCProperties[Symbol].ElementAt(1);
            Name = names[randomThing];
        }
    }
}
