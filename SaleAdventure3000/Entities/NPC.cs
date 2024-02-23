using System.Text;

namespace SaleAdventure3000.Entities
{
    public class NPC : Creature
    {
        private static string Boss = Convert.ToChar(169).ToString();
        private readonly string[] symbols = [" # ", " * ", " @ ", " & ", " £ ", Boss];
        private readonly string[] names = ["Nikos","Ragnar", "Klasse",  "Olle", "Jonas", "Tintin"];
        private readonly Dictionary<string, int[]> NPCProperties = new()
        {
            {" # ", [150, 17]},
            {" * ", [100, 12]},
            {" @ ", [75, 10]},
            {" & ", [60, 8]},
            {" £ ", [30, 6] },
            {Boss, [300, 50] }
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
