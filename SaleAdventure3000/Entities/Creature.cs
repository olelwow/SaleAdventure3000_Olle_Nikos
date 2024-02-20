namespace SaleAdventure3000.Entities
{
    public abstract class Creature : Entity
    {
        public Creature() { }
        public int HP { get; set; }
        public int Power { get; set; }

        public override void EntitySelection(string type)
        { 
        }
    }
}