using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Wearable : Item
    {
        
        private readonly string[] symbols = [" H ", " B ", " N "];
        private readonly string[] names = ["Hat", "Boots", "Necklace"];
        private readonly Dictionary<string, int[]> wearableProperties = new Dictionary<string, int[]>()
        {
            {" H ", [20, 6]},
            {" B ", [15, 10]},
            {" N ", [5, 2]}
            // Siffrorna i arrayen representerar Poweradded och HPBoost.
        };
        public Wearable(bool eq) 
        {
            this.Equipped = eq;
        }
        public Wearable(string type, int posX, int posY)
        {
            EntitySelection(type);
            this.PosX = posX;
            this.PosY = posY;
            this.SymbolColor = "#6699ff";
            this.Wear = true;
        }
        public Wearable (string type)
        {
            EntitySelection(type);
            this.Wear = true;
        }
        public override void EntitySelection(string type)
        {
            // Metod som väljer rätt egenskaper beroende på vilket namn man skrivit in i konstruktorn.
            for (int i = 0; i < names.Length; i++)
            {
                if (type == names[i])
                {
                    this.Name = names[i];
                    this.Symbol = symbols[i];
                    this.PowerAdded = wearableProperties[Symbol].ElementAt(0);
                    this.HpBoost = wearableProperties[Symbol].ElementAt(1);
                }
            }
        }

        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.Name} equips " +
                              $"{item.Name}, gaining {item.HpBoost} HP " +
                              $"and {item.PowerAdded} power."
                              );

            player.HP += item.HpBoost;
            player.Power += item.PowerAdded;
            item.Equipped = true;
            AddToBag(item, player);
        }
    }
}
