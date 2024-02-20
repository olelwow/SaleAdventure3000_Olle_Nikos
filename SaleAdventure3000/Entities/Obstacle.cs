using SaleAdventure3000.Entities;
    
public class Obstacle : Entity
{
    public Obstacle(string symbol) 
    {
        this.Symbol = symbol;
        this.CanPass = false;
        this.SymbolColor = "#666565";

    }

    public override void EntitySelection(string type)
    {
    }
}