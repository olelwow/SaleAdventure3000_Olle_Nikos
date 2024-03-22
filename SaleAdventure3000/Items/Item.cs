using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Item : Entity
    {   
        private int healAmount;
        private int powerAdded;
        private int hpBoost;
        private int luck;
        private int evasion;
        private int armor;
        private string? itemType;
        private bool wear = false;
        private int amount;
        private bool equipped = false;
        public Item() { }
        

        // Metod som styr vad som händer när man plockar upp ett Item,
        // olika beteende beroende på ifall föremålet är wearable eller consumable.
        public virtual void OnPickup (Item item, Player player)
        {
        }

        // Metod som addera item till inventory/bag
        public static void AddToBag (Item item, Player player)
        {
            Dictionary<Item,int> bag = player.GetBag();
            if (!bag.TryGetValue(item, out int value))
            {
                bag.Add(item, 1);
            }
            else
            {
                bag[item] = ++value;
                /* Löste problemet här genom att lägga till en override metod i klassen Item.
                   Eftersom två objekt inte betraktas som lika även om de har samma namn, stats osv
                   så blev det en ny rad i vilket fall. Kör man override på metoden Equals så kommer
                   den att betrakta två objekt som likadana ifall de har samma Name.
                */
            }
        }

        // Override metoder som kommer från Entity classen
        public override void EntitySelection(string name, string type)
        {
        }
        public override void EntitySelection(string name)
        {
        }

        //Getters/Setters
        public int HealAmount
        {
            get { return this.healAmount; }
            set { this.healAmount = value; }
        }
        public int PowerAdded
        {
            get { return this.powerAdded; }
            set { this.powerAdded = value; }
        }
        public int HpBoost
        {
            get { return this.hpBoost; }
            set { this.hpBoost = value; }
        }
        public bool Wear
        {
            get { return this.wear; }
            set { this.wear = value; }
        }
        public int Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }
        public bool Equipped
        {
            get { return this.equipped; }
            set { this.equipped = value; }
        }
        public int Evasion
        {
            get { return this.evasion; }
            set { this.evasion = value; }
        }
        public int Armor
        {
            get { return this.armor; }
            set { this.armor = value; }
        }
        public int Luck
        {
            get { return this.luck; }
            set { this.luck = value; }
        }
        public string ItemType
        {
            get { return this.itemType; }
            set { this.itemType = value; }
        }
    }
}
