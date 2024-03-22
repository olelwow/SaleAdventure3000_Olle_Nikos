using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Consumable : Item
    {
        // Arrays för Consumables som innehåller deras symboler, namn och färg.
        // Dictionary används för att avgöra consumable stats.
        private readonly string[] names = ["Egg", "Pie", "Cheese", "Bad-Apple"];
        private readonly string[] symbols = [" E ", " P ", " C ", "B A"];// CxDDDD
        private readonly string[] colors = ["#24691c", "#f786c6", "#dbc63d", "#382121"];
        private readonly Dictionary<string, int> ConsumableAttributes = new()
        {
            // Egg ger 10hp, Pie ger 20, Cheese ger 30, Rotten Apple ger -30.
            {" E ", 10 },
            {" P ", 20},
            {" C ", 30},
            {"B A", -30} 
        };
        public Consumable () { }
        public Consumable(string type, int posX, int posY)
        {
            EntitySelection(type);
            this.PosX = posX;
            this.PosY = posY;
        }
        
        //Metod som adderar consumable till inventory/bag.
        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.Name} picked up a {item.Name}");
            AddToBag(item, player);
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
                    this.SymbolColor = colors[i];
                    this.HealAmount = ConsumableAttributes[symbols[i]];
                }
            }
        }

    }
}
