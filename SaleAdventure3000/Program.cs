namespace SaleAdventure3000
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool run = true;
            //MenuOperations.ProgressBar();
            //Console.ReadLine();
            while (run)
            {
                run = MenuOperations.PrintStartMenu(run);
            }
        }
    }
}