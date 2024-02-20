using SaleAdventure3000;

internal class Program
{
    private static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            run = MenuOperations.PrintStartMenu(run);
        }
    }
}

