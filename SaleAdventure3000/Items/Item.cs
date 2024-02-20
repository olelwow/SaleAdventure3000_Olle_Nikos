using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Item : Entity
    {   
        private int HealAmount;
        private int PowerAdded;
        private int HpBoost;
        private bool Wear;
        private int Amount;
        private bool Equipped = false;
        public Item() { }
        

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
        
        public int HealAmountGetSet
        {
            get { return this.HealAmount; }
            set { this.HealAmount = value; }
        }
        public int PowerAddedGetSet
        {
            get { return this.PowerAdded; }
            set { this.PowerAdded = value; }
        }
        public int HpBoostGetSet
        {
            get { return this.HpBoost; }
            set { this.HpBoost = value; }
        }
        public bool WearGetSet
        {
            get { return this.Wear; }
            set { this.Wear = value; }
        }
        public int AmountGetSet
        {
            get { return this.Amount; }
            set { this.Amount = value; }
        }
        public bool EquippedGetSet
        {
            get { return this.Equipped; }
            set { this.Equipped = value; }
        }
    }
}
