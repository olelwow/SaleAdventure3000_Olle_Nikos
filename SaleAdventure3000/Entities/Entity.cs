using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public abstract class Entity
    {
        private int posX;
        private int posY;
        private string? symbol;
        private string name = "Entity";
        private string symbolColor = "#ffffff";
        private string backgroundColor = "000000";

        public bool CanPass = true;

        public abstract void EntitySelection(string name, string type);
        public abstract void EntitySelection(string name);

        // Override metod som gör det möjligt att betrakta två objekt som lika
        // Ifall de delar samma Symbol.
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return symbol == other.symbol;
        }
        public override int GetHashCode()
        {
            // Med unchecked så kastas ett exception ifall värdet går över maxvärde för int.
            unchecked
            {
                int hash = 17;
                // Ifall Symbol inte är null returneras hash * 23 + symbolens hashcode, annars returneras 0 vilket innebär inte lika.
                hash = hash * 23 + (symbol != null ? symbol.GetHashCode() : 0);
                return hash;
            }
        }
        // Get/Set
        public int PosX
        {
            get { return this.posX ; }
            set { this.posX = value; }
        }
        public int PosY
        {
            get { return this.posY; }
            set { this.posY = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Symbol
        {
            get 
            { 
                if (this.symbol != null)
                {
                    return this.symbol;
                }
                return "   "; 
            }
            set { this.symbol = value; }
        }
        public string SymbolColor
        {
            get { return this.symbolColor; }
            set { this.symbolColor = value; }
        }
        public string BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }
    }
}
