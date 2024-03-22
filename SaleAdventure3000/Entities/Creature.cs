namespace SaleAdventure3000.Entities
{
    public abstract class Creature : Entity
    {
        private double hP;
        private int power;
        
        public Creature() { }

        // Metoder som underlättar vid skapandet av Entities.
        public override void EntitySelection(string name, string type)
        {
        }
        public override void EntitySelection(string name)
        {
        }
        // Get/Set
        public double HP
        {
            get { return this.hP; }
            set { this.hP = value; }
        }
        public int Power
        {
            get { return this.power; }
            set { this.power = value; }
        }
    }
}