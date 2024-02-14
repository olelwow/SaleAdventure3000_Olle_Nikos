namespace SaleAdventure3000.Entities
{
    public class NPC : Creature
    {
        private readonly string[] symbols = [" * ", " @ ", " # ", " & ", " £ "];
        private readonly string[] names = ["Ragnar", "Klasse", "Nikos", "Olle", "Jonas"];
        private readonly int randomizedProperties = new Random().Next(0, 5);
        private readonly Dictionary<string, int[]> NPCProperties = new Dictionary<string, int[]>()
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
            Symbol = symbols[randomizedProperties];
            Name = names[randomizedProperties];
            HP = NPCProperties[Symbol].ElementAt(0);
            Power = NPCProperties[Symbol].ElementAt(1);
        }
    }
}
