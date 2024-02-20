using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Consumable : Item
    {
        private readonly string[] names = ["Egg", "Pie", "Cheese", "Bad-Apple"];
        private readonly string[] symbols = [" E ", " P ", " C ", "B A"];// CxDDDD
        private readonly string[] colors = ["#b5ba9b", "#61401b", "#dbc63d", "#382121"];
        private readonly Dictionary<string, int> ConsumableAttributes = new Dictionary<string, int>()
        {
            // Egg ger 10hp, Pie ger 20, Cheese ger 30, Rotten Apple ger -30
            {" E ", 10 },
            {" P ", 20},
            {" C ", 30},
            {"B A", -30} 
        };
        public Consumable(string type, int posX, int posY)
        {
            EntitySelection(type);
            this.PosXGetSet = posX;
            this.PosYGetSet = posY;
        }
        
        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.NameGetSet} picked up a {item.NameGetSet}");
            AddToBag(item, player);
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
                    this.SymbolColorGetSet = colors[i];
                    this.HealAmountGetSet = ConsumableAttributes[symbols[i]];
                }
            }
        }

    }
}
