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
    }
}
