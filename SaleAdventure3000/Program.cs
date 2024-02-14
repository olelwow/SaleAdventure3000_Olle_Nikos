using SaleAdventure3000;

internal class Program
{
    
    private static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            Console.Clear();    
            Operations.Logo();
            Console.WriteLine("- - - Menu - - -\n");
            int choice = Operations.PrintChoiceMenu("Register",
                "Log in", "View scoreboard", "Quit");

            switch (choice)
            {
                case 1:
                    Operations.RegisterPlayer();
                    break;
                case 2:
                    Operations.Login();
                break;
                case 3:
                    Operations.Scoreboard();
                break;
                case 4:
                run = false;
                break;
                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }
        }
    }
}