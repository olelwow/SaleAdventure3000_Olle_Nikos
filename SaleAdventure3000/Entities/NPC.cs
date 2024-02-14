namespace SaleAdventure3000.Entities
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

        public NPC(string npcType, int posX, int posY)
        {
            // gammal idé
            //Symbol = symbols[randomizedProperties];
            //Name = names[randomizedProperties];
            //HP = NPCProperties[Symbol].ElementAt(0);
            //Power = NPCProperties[Symbol].ElementAt(1);

            // ny idé, wack
            EntitySelection(npcType);
            this.PosX = posX;
            this.PosY = posY;
            
        }
        public override void EntitySelection(string npcType)
        { 
            for (int i = 0; i < names.Length; i++)
            {
                if (npcType == names[i])
                {
                    this.Symbol = symbols[i];
                    this.Name = names[i];
                    this.HP = NPCProperties[Symbol].ElementAt(0);
                    this.Power = NPCProperties[Symbol].ElementAt(1);
                    this.Color = colors[i];
                }
            }
        }
    }
}
