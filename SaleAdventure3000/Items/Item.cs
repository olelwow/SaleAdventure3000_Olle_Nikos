using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Item : Entity
    {
        public Item() { }
        public int HealAmount { get; set; }
        public int PowerAdded { get; set; }
        public int HpBoost { get; set; }
        public bool Wear {  get; set; }
        public int Amount { get; set; }
        public bool Equipped { get; set; } = false;

        // Metod som styr vad som händer när man plockar upp ett Item,
        // olika beteende beroende på ifall föremålet är wearable eller consumable.
        public virtual void OnPickup (Item item, Player player)
        {
        }
        public void AddToBag (Item item, Player player)
        {
            Dictionary<Item,int> bag = player.GetBag();
            if (!bag.ContainsKey(item))
            {
                bag.Add(item, 1);
            }
            else
            {
                bag[item]++;
                /* Löste problemet här genom att lägga till en override metod i klassen Item.
                   Eftersom två objekt inte betraktas som lika även om de har samma namn, stats osv
                   så blev det en ny rad i vilket fall. Kör man override på metoden Equals så kommer
                   den att betrakta två objekt som likadana ifall de har samma Name.
                */
            }
        }
        public override void EntitySelection(string type)
        {
        }
    }
}
