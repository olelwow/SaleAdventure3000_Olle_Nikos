namespace SaleAdventure3000.Entities
{
    public abstract class Creature : Entity
    {
        private int HP;
        private int Power;
        public Creature() { }
        public override void EntitySelection(string type)
        {
        }
        public int HPGetSet
        {
            get { return this.HP; }
            set { this.HP = value; }
        }
        public int PowerGetSet
        {
            get { return this.Power; }
            set { this.Power = value; }
        }
    }
}