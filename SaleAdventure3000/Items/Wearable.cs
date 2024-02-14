using SaleAdventure3000.Entities;

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
            this.Symbol = symbols[randomizedProperties];
            this.Name = names[randomizedProperties];
            this.PowerAdded = wearableProperties[Symbol].ElementAt(0);
            this.HpBoost = wearableProperties[Symbol].ElementAt(1);
            this.Wear = true;
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
