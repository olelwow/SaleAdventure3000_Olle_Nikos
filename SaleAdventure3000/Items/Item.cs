using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual void OnPickup (Item item, Player player)
        {
        }
        public void AddToBag (Item item, Player player)
        {
            if (!player.Bag.ContainsKey(item))
            {
                player.Bag.Add(item, 1);
            }
            else
            {
                player.Bag[item]++;
                /* Löste problemet här genom att lägga till en override metod i klassen Item.
                   Eftersom två objekt inte betraktas som lika även om de har samma namn, stats osv
                   så blev det en ny rad i vilket fall. Kör man override på metoden Equals så kommer
                   den att betrakta två objekt som likadana ifall de har samma Name.
                */
            }
        }
        //public virtual void OnPickup(Consumable c, Player p) 
        //{ 
        //}
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Name == other.Name;
        }
        // Override metod som gör det möjligt att betrakta två objekt som lika
        // Ifall de delar samma Name.

        public override int GetHashCode()
        {
            if (Name != null)
            {
                return Name.GetHashCode();
            }
            return -1;
        }


    }
}
