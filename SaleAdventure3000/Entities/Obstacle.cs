namespace SaleAdventure3000.Entities
{
    public class Obstacle : Entity
    {
        public Obstacle(string symbol)
        {
            Symbol = symbol;
            CanPass = false;
            SymbolColor = "#666565";

        }
        public override void EntitySelection(string type) {}

        public override void EntitySelection(string name, string type) { }
    }
}