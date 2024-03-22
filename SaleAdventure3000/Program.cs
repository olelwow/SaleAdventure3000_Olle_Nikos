namespace SaleAdventure3000
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.SetWindowSize(120,60); // Sätter konsolfönstrets storlek.
            Console.Title = "Super awesome adventure game";
            bool run = true;
            while (run)
            {
                run = MenuOperations.PrintStartMenu(run); // Kör igång spelet, printar startmeny.
            }
        }
    }
}