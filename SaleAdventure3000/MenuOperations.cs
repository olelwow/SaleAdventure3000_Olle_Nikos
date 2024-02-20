using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using Spectre.Console;

namespace SaleAdventure3000
{
    //Den här klassen innehåller bas metoder som påverkar utskrift av medelande
    public abstract class MenuOperations
    {
        public static bool PrintStartMenu(bool run)
        {
            Console.Clear();
            Logo();
            Console.WriteLine("- - - Menu - - -\n");
            int choice = PrintChoiceMenu("Register", "Log in",
                                         "View scoreboard", "Quit"
                                        );
            switch (choice)
            {
                case 1:
                    RegisterPlayer();
                    break;

                case 2:
                    Login();
                    break;

                case 3:
                    Scoreboard();
                    break;

                case 4:
                    run = false;
                    break;

                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }
            // returnerar run, vilket blir false om man väljer case 4. 
            // Annars fortsätter loopen i Program.cs
            return run;
        }

        //Skriver ut logon(duh)
        public static void Logo()
        {
            Console.WriteLine("*******************************\n" +
                    "Welcome to the Fight Simulator\n" +
                    "*******************************\n");
        }
        //med denna metod registreras en ny spelare till txt filen
        public static void RegisterPlayer()
        {
            Console.Clear();
            Logo();
            Console.Write("Pick a name: ");
            string? chosenName = Console.ReadLine();
            while (chosenName == null || chosenName.Length < 3)
            {
                Console.WriteLine("Invalid name, must be at least 3 character long.");
                Console.WriteLine("What is your name, adventurer?");
                chosenName = Console.ReadLine();
            }
            StreamWriter stream = new StreamWriter(@"../../../Login.txt", true);
            stream.WriteLine($"Name : {chosenName}");
            stream.Close();
        }
        // i denna metod kontrolleras om spelarens namn finns redan i txt filen och börjar spelet
        public static void Login()
        {
            Console.Clear();
            Logo();
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();
            bool found = false;
            string[] lines = File.ReadAllLines(@"../../../Login.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (username != null && lines[i].Contains(username))
                {
                    found = true;
                    break;
                }
            }
            if (found == true && username != null)
            {
                Console.WriteLine("\n\tLoging succeed");
                Thread.Sleep(1500);
                Game.StartGame(username);
            }
            else
            {

                Console.WriteLine("\n\tInvalid username");
                Console.ReadLine();
            }
        }
        //Scoreboard metoden skriver ut scoreboard av alla spelare som har spelat
        internal static void Scoreboard()
        {
            string[] lines = File.ReadAllLines(@"../../../Scoreboard.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.ReadLine();
        }
        //Generellt metod som skriver ut menyerna med hjälp av Spectre.Console som är snyggare än vanlig console
        public static int PrintChoiceMenu(string choice1, string choice2, string choice3, string choice4)
        {
            // Dictionary för att koppla ihop respektive val med en siffra.
            Dictionary<string, int> choice = new Dictionary<string, int>()
            { {choice1, 1},
              {choice2, 2 },
              {choice3, 3 },
              {choice4, 4 }
            };

            var displayMenu =
                AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Make your choice")
                .PageSize(5)
                .AddChoices(new[]
                {
                    choice1, choice2, choice3, choice4
                }));
            // Returnerar värdet i choice som hör ihop med rätt menyval.
            return choice[displayMenu];
        }

        /*I denna metod hämtar vi registrerat scoreboard.
         *Populerar vi 2 listor med scoreboard och itererar en av de.
         *Sen händer jämförelse mellan nuvarande score och registrerat score.
         *I den icke-iterations lista ersätter vi data enligt jämförelse och 
         *Ersätter alla raderna i txt filen med uppdaterat namn och score.
         */
        internal static void ScoreBoardReg(Player player)
        {
            StreamReader reader = new StreamReader(@"../../../Scoreboard.txt");
            string line = "";
            List<string> lines = new List<string>();
            List<string> newLines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
                newLines.Add(line);
            }
            reader.Close();

            StreamWriter writer = new StreamWriter(@"../../../Scoreboard.txt");

            if (lines.Count > 0)
            {
                foreach (string row in lines)
                {
                    if (row.Contains(player.Name))
                    {
                        int newPoints = player.score;
                        int index = row.Length - 2;
                        int currentPoints = int.Parse(row.Substring(index, 2));

                        if (currentPoints < newPoints)
                        {
                            newLines.Remove(row);
                            newLines.Add($"Name : {player.Name} - Score : {newPoints}");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        newLines.Add($"Name : {player.Name} - Score : {player.score}");
                    }
                }
            }
            else
            {
                newLines.Add($"Name : {player.Name} - Score : {player.score}");
            }
            foreach (string s in newLines)
            {
                writer.WriteLine(s);
            }
            writer.Close();
        }

        public static void PrintGameInfo(Player player)
        {
            // en wedi ruta med grejer

            Console.WriteLine($"             =======================================");
            Console.WriteLine($"             |   Use WASD or arrow keys to move    |");
            Console.WriteLine($"             |   B to open Bag                     |");
            Console.WriteLine($"             |   Q to quit game                    |");
            Console.WriteLine($"                 {player.Name}'s HP:{player.HP}   ");
            Console.WriteLine($"             |                                     |");
            Console.WriteLine($"             =======================================");
        }
    }
}
