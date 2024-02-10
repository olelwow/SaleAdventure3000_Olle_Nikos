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
        
        public virtual void OnPickup (Wearable w, Player p)
        {
        }
        public virtual void OnPickup(Consumable c, Player p) 
        { 
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Name == other.Name;
        }

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
