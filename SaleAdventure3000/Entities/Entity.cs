using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public class Entity
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string? Symbol { get; set; }
        public string Name { get; set; }

        public bool CanPass = true;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Symbol == other.Symbol;
        }
        // Override metod som gör det möjligt att betrakta två objekt som lika
        // Ifall de delar samma Name.

        public override int GetHashCode()
        {
            if (Symbol != null)
            {
                return Symbol.GetHashCode();
            }
            return -1;
        }

    }
}
