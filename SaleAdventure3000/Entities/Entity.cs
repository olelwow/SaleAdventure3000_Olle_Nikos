using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public abstract class Entity
    {
        private int PosX;
        private int PosY;
        private string? Symbol;
        private string? Name;
        private string SymbolColor = "#ffffff";
        private string BackgroundColor = "000000";

        public bool CanPass = true;

        public abstract void EntitySelection(string type);

        // Override metod som gör det möjligt att betrakta två objekt som lika
        // Ifall de delar samma Name.
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Symbol == other.Symbol;
        }
        public override int GetHashCode()
        {
            if (Symbol != null)
            {
                return Symbol.GetHashCode();
            }
            return -1;
        }
        public int PosXGetSet
        {
            get { return this.PosX ; }
            set { this.PosX = value; }
        }
        public int PosYGetSet
        {
            get { return this.PosY; }
            set { this.PosY = value; }
        }
        public string NameGetSet
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        public string SymbolGetSet
        {
            get { return this.Symbol; }
            set { this.Symbol = value; }
        }
        public string SymbolColorGetSet
        {
            get { return this.SymbolColor; }
            set { this.SymbolColor = value; }
        }
        public string BackgroundColorGetSet
        {
            get { return this.BackgroundColor; }
            set { this.BackgroundColor = value; }
        }
    }
}
