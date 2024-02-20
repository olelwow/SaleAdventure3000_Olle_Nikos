using SaleAdventure3000.Entities;
    
public class Obstacle : Entity
{
    public Obstacle(string symbol) 
    {
        SymbolGetSet = symbol;
        this.CanPass = false;
    }

    public override void EntitySelection(string type)
    {
    }
}