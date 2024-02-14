using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Consumable : Item
    {
        private readonly string[] names = ["Egg", "Pie", "Cheese", "Bad-Apple"];
        private readonly string[] symbols = [" E ", " P ", " C ", "B A"];// CxDDDD
        private readonly Dictionary<string, int> ConsumableAttributes = new Dictionary<string, int>()
        {
            {" E ", 10 },
            {" P ", 20},
            {" C ", 30},
            {"B A", -30} 
            // Egg ger 10hp, Pie ger 20, Cheese ger 30, Rotten Apple ger -30
        };
        
        private readonly int randomizedProperties = new Random().Next(0, 3);
        public Consumable(string consumableType, int posX, int posY)
        {
            //this.Symbol = ConsumableSymbols[randomizedProperties];
            //this.HealAmount = ConsumableAttributes[this.Symbol];
            //this.Wear = false;
            EntitySelection(consumableType);
            this.PosX = posX;
            this.PosY = posY;
            this.Color = "#ff9966";

        }
        
        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.Name} picked up a {item.Name}");

            AddToBag(item, player);
        }
        public override void EntitySelection(string npcType)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if (npcType == names[i])
                {
                    Symbol = symbols[i];
                    Name = names[i];
                    HealAmount = ConsumableAttributes[symbols[i]];
                }
            }
        }

    }
}
