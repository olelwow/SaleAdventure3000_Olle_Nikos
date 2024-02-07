using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000
{
    internal class NPCs : Creature
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

        };
        
        public NPCs ()
        {
            this.Symbol = symbols[randomThing];
            this.HP = NPCProperties[this.Symbol].ElementAt(0);
            this.Power = NPCProperties[this.Symbol].ElementAt(1);
            this.Name = names[randomThing];
        }
    }
}
