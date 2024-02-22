namespace SaleAdventure3000.Entities
{
    public class NPC : Creature
    {
        private readonly string[] symbols = [" # ", " * ", " @ ",  " & ", " £ "];
        private readonly string[] names = ["Nikos","Ragnar", "Klasse",  "Olle", "Jonas"];
        private readonly Dictionary<string, int[]> NPCProperties = new Dictionary<string, int[]>()
        {
            {" # ", [150, 17]},
            {" * ", [100, 12]},
            {" @ ", [75, 10]},
            {" & ", [60, 8]},
            {" £ ", [30, 6] }
            // Siffrorna i arrayen representerar HP och Power.
        };

        public NPC(string type, int posX, int posY)
        {
            EntitySelection(type);
            this.PosX = posX;
            this.PosY = posY;
            this.SymbolColor = "#000000";
        }
        public override void EntitySelection(string type)
        { 
            // Metod som väljer rätt egenskaper beroende på vilket namn man skrivit in i konstruktorn.
            for (int i = 0; i < names.Length; i++)
            {
                if (type == names[i])
                {
                    this.Symbol = symbols[i];
                    this.Name = names[i];
                    this.HP = NPCProperties[Symbol].ElementAt(0);
                    this.Power = NPCProperties[Symbol].ElementAt(1);
                }
            }
        }
    }
}
