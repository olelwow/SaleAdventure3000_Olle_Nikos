namespace SaleAdventure3000
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.SetWindowSize(120,60);
            bool run = true;
            while (run)
            {
                run = MenuOperations.PrintStartMenu(run);
            }
        }
    }
}