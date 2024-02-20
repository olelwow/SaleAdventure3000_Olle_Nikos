﻿namespace SaleAdventure3000.Entities
{
    public class NPC : Creature
    {
        private readonly string[] symbols = [" # ", " * ", " @ ",  " & ", " £ "];
        private readonly string[] names = ["Nikos","Ragnar", "Klasse",  "Olle", "Jonas"];
        private readonly string[] colors = ["#660000", "#b30000", "#ff0000", "#ff4d4d", "#ff9999"];
        private readonly int randomizedProperties = new Random().Next(0, 5);
        private readonly Dictionary<string, int[]> NPCProperties = new Dictionary<string, int[]>()
        {
            {" # ", [150, 7]},
            {" * ", [100, 12]},
            {" @ ", [75, 20]},
            {" & ", [60, 15]},
            {" £ ", [30, 5] }
            // Siffrorna i arrayen representerar HP och Power.
        };

        public NPC(string type, int posX, int posY)
        {
            EntitySelection(type);
            PosXGetSet = posX;
            PosYGetSet = posY;
        }
        public override void EntitySelection(string type)
        { 
            // Metod som väljer rätt egenskaper beroende på vilket namn man skrivit in i konstruktorn.
            for (int i = 0; i < names.Length; i++)
            {
                if (type == names[i])
                {
                    this.SymbolGetSet = symbols[i];
                    this.NameGetSet = names[i];
                    this.HPGetSet = NPCProperties[SymbolGetSet].ElementAt(0);
                    this.PowerGetSet = NPCProperties[SymbolGetSet].ElementAt(1);
                    this.SymbolColorGetSet = colors[i];
                }
            }
        }
    }
}
