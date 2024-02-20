namespace SaleAdventure3000.Entities
{
    public abstract class Creature : Entity
    {
        private int hP;
        private int power;
        public Creature() { }
        public override void EntitySelection(string type)
        {
        }
        public int HP
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