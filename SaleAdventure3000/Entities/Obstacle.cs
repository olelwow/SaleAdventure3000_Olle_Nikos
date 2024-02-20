using SaleAdventure3000.Entities;
    
public class Obstacle : Entity
{
    public Obstacle(string symbol) 
    {
        this.Symbol = symbol;
        this.CanPass = false;
    }

    public override void EntitySelection(string type)
    {
    }
}