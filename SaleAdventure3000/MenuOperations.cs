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
                                         "View scoreboard", "Quit");
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
            string[] lines = File.ReadAllLines(@"../../../Login.txt");
            bool found = false;
            bool run = true;
            Console.Write("Pick a name: ");
            string? chosenName = Console.ReadLine();
            while (chosenName == null || chosenName.Length < 3)
            {
                Console.WriteLine("Invalid name, must be at least 3 characters long");
                Console.Write("Pick a name: ");
                chosenName = Console.ReadLine();
            }
            while (run)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(chosenName))
                    {
                        Console.WriteLine("Name is already taken");
                        Console.Write("Pick a name: ");
                        chosenName = Console.ReadLine();
                        found = true;
                        run = true;
                    }
                    else
                    {
                        found = false;
                        run = false;
                    }
                }
            }
            if (found == false && !(chosenName == null || chosenName.Length < 3))
            {
                StreamWriter stream = new StreamWriter(@"../../../Login.txt", true);
                stream.WriteLine($"Name : {chosenName}");
                Console.WriteLine("Registering succeed!");
                Console.ReadLine();
                stream.Close();
            }
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
                Console.WriteLine("\n\tLogin success");
                ProgressBar();
                Thread.Sleep(4000);
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
                .AddChoices([choice1, choice2, choice3, choice4]));
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
        public static string PrintBagMenuAndReturnChoice(Player player)
        {
            // Tar in spelarens bag via GetBag, och skapar ny array av items för jämförelse längre ner.
            Dictionary<Item, int> bag = player.GetBag();
            Item[] items = new Item[bag.Count];

            // Skapar ny SelectionPrompt, med rubrikerna ovan.
            var showBag =
                new SelectionPrompt<string>()
                .Title("  Item       Wearable     Amount   Equipped")
                .PageSize(bag.Count + 4);

            int index = 0;

            foreach (var item in bag)
            {
                // Adderar nödvändig info till showBag. För att detta skulle funka var jag tvungen 
                // att ändra i Entity så att Name inte kan vara null. Siffrorna efter variabeln i måsvingarna
                // är till för formatering för att göra menyn symmetrisk.
                showBag.AddChoice
                    ($"{item.Key.Name, -11}" +
                     $"{item.Key.Wear, -13}" +
                     $"{item.Value, -9}" +
                     $"{item.Key.Equipped, -5}"
                     );
                // Lägger till Item i arrayen.
                items[index] = item.Key;
                index++;
            }
            showBag.AddChoice("Close Bag");
            return AnsiConsole.Prompt(showBag);
        }
        public static void PrintGameInfo(Player player)
        {
            // en wedi ruta med grejer

            Console.WriteLine($"==================================================================");
            Console.WriteLine($"|                 Use WASD or arrow keys to move                 |");
            Console.WriteLine($"|                        B to open Bag                           |");
            Console.WriteLine($"|                        Q to quit game                          |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"|                     HP remaining: {player.HP.ToString("F1"),-5}                        |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"==================================================================");
        }
        public static void PrintGameInfo (Player player, Item item)
        {
            Dictionary <Tuple<bool, bool>, string> gameMessages = new Dictionary<Tuple< bool, bool>, string> ()
            {
                {Tuple.Create(true, true) , $"Player {player.Name} equips {item.Name}," +
                                            $" gaining {item.HpBoost} HP and {item.PowerAdded} power."
                },
                {Tuple.Create(true, false), $"Player {player.Name} unequips {item.Name}," +
                                            $" losing {item.HpBoost} HP and {item.PowerAdded} power."
                },
                {Tuple.Create(false, false), $"Player {player.Name} eats a {item.Name}." +
                                             $" It heals for {item.HealAmount}."
                }
            };
            foreach (var condition in gameMessages) 
            {
                if ((condition.Key.Item1 == true && condition.Key.Item2 == true) &&
                    (item.Wear == true && item.Equipped == true))
                {
                    var value = Tuple.Create(true, true);
                    InfoText(gameMessages, value, player);
                }
                else if ((condition.Key.Item1 == true && condition.Key.Item2 == false) &&
                    (item.Wear == true && item.Equipped == false ))
                {
                    var value = Tuple.Create(true, false);
                    InfoText(gameMessages, value, player);
                }
                else if ((condition.Key.Item1 == false && condition.Key.Item2 == false) &&
                            (item.Wear == false && item.Equipped == false))
                {
                    var value = Tuple.Create(false, false);
                    InfoText(gameMessages, value, player);
                }
            }
        }
        public static void InfoText(Dictionary<Tuple<bool,bool>,string> gameMessages, Tuple<bool, bool> value, Player player)
        {
            Console.WriteLine($"==================================================================");
            Console.WriteLine($"|                  Use WASD or arrow keys to move                |");
            Console.WriteLine($"|                         B to open Bag                          |");
            Console.WriteLine($"|                         Q to quit game                         |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"|                     HP remaining: {player.HP.ToString("F1"),-5}                        |");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"|     {gameMessages[value], -59}|");
            Console.WriteLine($"|                                                                |");
            Console.WriteLine($"==================================================================");
        }

        public static void ProgressBar ()
        {
             AnsiConsole.Progress()
            .AutoRefresh(true)
            .AutoClear(false)   
            .HideCompleted(false)   
            .Columns(new ProgressColumn[]
            {
                new TaskDescriptionColumn(),    
                new ProgressBarColumn(),        
                new PercentageColumn(),         
                new SpinnerColumn(),            
            })
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("Loading....");

                while (!ctx.IsFinished)
                {
                    await Task.Delay(75);
                    task.Increment(3);
                }
            });
        }
        public static void DisplayFightImages (Player player, NPC npc)
        {
            // Skapar nytt table med kolumner för spelare och NPC.
            var table = FightTableHeaders(player, npc);

            // Tar in ikonerna, sätter storlek samt adderar dem till table.
            var playerImage = new CanvasImage(@"../../../Icons/player_icon.png").MaxWidth(25);
            var middle = new CanvasImage(@"../../../Icons/vs_icon1.png").MaxWidth(25);
            var npcImage = new CanvasImage(@"../../../Icons/npc_icon.png").MaxWidth(25);
            table.AddRow(playerImage, middle, npcImage);
            AnsiConsole.Write(table);
        }
        public static void DisplayFightImages(string winner, Player player, NPC npc)
        {
            var table = FightTableHeaders(player, npc);
            // Beroende på vem som vann fighten visas olika bilder.
            if (winner == "player")
            {
                var playerImage = new CanvasImage(@"../../../Icons/player_icon.png").MaxWidth(25);
                var middle = new CanvasImage(@"../../../Icons/win_icon.png").MaxWidth(25);
                var npcImage = new CanvasImage(@"../../../Icons/dead_icon.png").MaxWidth(25);
                table.AddRow(playerImage, middle, npcImage);
                AnsiConsole.Write(table);
            }
            else if (winner == "npc")
            {
                var playerImage = new CanvasImage(@"../../../Icons/dead_icon.png").MaxWidth(25);
                var middle = new CanvasImage(@"../../../Icons/lose_icon.png").MaxWidth(25);
                var npcImage = new CanvasImage(@"../../../Icons/npc_icon.png").MaxWidth(25);
                table.AddRow(playerImage, middle, npcImage);
                AnsiConsole.Write(table);
            }
        }
        public static Table FightTableHeaders (Player player, NPC npc)
        {
            var table = new Table();
            table.AddColumn($"{player.Name} : {player.HP.ToString("F1")} HP");
            table.AddColumn(" VERSUS ");
            table.AddColumn($"{npc.Name} : {npc.HP.ToString("F1")} HP");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].Centered();
            }
            return table;
        }
        public static void WinScreen (Player player)
        {
            var table = new Table();
            table.AddColumn($"You made it all the way to the goal! Congratulations!" +
                            $"\nYou achieved a score of {player.score}");
            table.Columns[0].Centered();
            
            var winImage = new CanvasImage(@"../../../Icons/win_icon.png").MaxWidth(35);
            table.AddRow(winImage);
            table.Centered();
            
            AnsiConsole.Write(table);
        }
    }
}
