using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using Spectre.Console;
using System.Numerics;

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
            string blank = " ";
            bool found = false;
            string[] lines = File.ReadAllLines(@"../../../Login.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (username != null && 
                    username != "" && 
                    username != blank &&
                    lines[i].Contains(username))
                {
                    found = true;
                    break;
                }
                else
                {
                    Console.WriteLine("\n\tInvalid username");
                    Console.ReadLine();
                    break;
                }
            }
            if (found == true)
            {
                Console.WriteLine("\n\tLogin success");
                LoadingGame();
                Thread.Sleep(1300);
                Game.StartGame(username);
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

                        if (currentPoints <= newPoints)
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
                     $"{item.Key.Equipped, -5}");
                // Lägger till Item i arrayen.
                items[index] = item.Key;
                index++;
            }
            showBag.AddChoice("Close Bag");
            return AnsiConsole.Prompt(showBag);
        }
        public static void PrintGameInfo(Player player)
        {
            AnsiConsole.WriteLine($"==================================================================");
            AnsiConsole.MarkupLine($"|                 [blue]Use WASD or arrow keys to move[/]                 |");
            AnsiConsole.MarkupLine($"|                        [cyan]B[/] to open Bag                           |");
            AnsiConsole.MarkupLine($"|                        [red]Q[/] to quit game                          |");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.MarkupLine($"|                     HP remaining: {ReturnHpWithColor(player)}                        |");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.WriteLine($"|                      Current score: {player.score, -2}                         |");
            AnsiConsole.WriteLine($"==================================================================");
        }
        public static void PrintGameInfo (Player player, (Item item, double heal) t)
        {
            // Dictionary med två bools som key, den första representerar ifall föremålet är wearable eller ej,
            // den andra representerar ifall föremålet är equipped eller ej.
            Dictionary <Tuple<bool, bool>, string> gameMessages = new Dictionary<Tuple< bool, bool>, string> ()
            {
                {Tuple.Create(true, true) , $"{player.Name, 10}".Trim() + " " + $"{"equips", 8}".Trim() + " " + $"{t.item.Name, 8}".Trim() + ", " +
                                            $"gaining [green]{t.item.HpBoost}[/] HP and [green]{t.item.PowerAdded}[/] power."
                },
                {Tuple.Create(true, false), $"{player.Name, 10}".Trim() + " unequips " + $"{t.item.Name, 8}".Trim() + ", " +
                                            $"{"losing", 7}".Trim() + " " + $"[red]{t.item.HpBoost}[/] HP and [red]{t.item.PowerAdded}[/] power."
                },
                {Tuple.Create(false, false), ReturnHealingWithColor(player, t)}
            };
            foreach (var message in gameMessages) 
            {
                if ((message.Key.Item1 == true && message.Key.Item2 == true) &&
                    (t.item.Wear == true && t.item.Equipped == true))
                {
                    var value = Tuple.Create(true, true);
                    InfoText(gameMessages, value, player);
                }
                else if ((message.Key.Item1 == true && message.Key.Item2 == false) &&
                    (t.item.Wear == true && t.item.Equipped == false ))
                {
                    var value = Tuple.Create(true, false);
                    InfoText(gameMessages, value, player);
                }
                else if ((message.Key.Item1 == false && message.Key.Item2 == false) &&
                            (t.item.Wear == false && t.item.Equipped == false))
                {
                    var value = Tuple.Create(false, false);
                    InfoText(gameMessages, value, player);
                }
            }
        }
        public static void InfoText(Dictionary<Tuple<bool,bool>,string> gameMessages,
                                    Tuple<bool, bool> value, Player player)
        {
            const int maxLength = 84;
            string truncatedMessage = gameMessages[value].Length > maxLength
                                    ? gameMessages[value].Substring(0, maxLength)
                                    : gameMessages[value];
            AnsiConsole.WriteLine($"==================================================================");
            AnsiConsole.MarkupLine($"|                 [blue]Use WASD or arrow keys to move[/]                 |");
            AnsiConsole.MarkupLine($"|                        [cyan]B[/] to open Bag                           |");
            AnsiConsole.MarkupLine($"|                        [red]Q[/] to quit game                          |");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.MarkupLine($"|                     HP remaining: {ReturnHpWithColor(player)}                        |");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.MarkupLine($"|{truncatedMessage, - maxLength}|");
            AnsiConsole.WriteLine($"|                                                                |");
            AnsiConsole.WriteLine($"==================================================================");
        }
        // Två metoder nedan för att ändra färg på hp/healing när de är under/över specifika värden.
        public static string ReturnHpWithColor (Player player)
        {
            return player.HP > 60 
                   ? $"[green]{player.HP.ToString("F1"),-5}[/]" 
                   : $"[red]{player.HP.ToString("F1"),-5}[/]";
        }
        public static string ReturnHealingWithColor (Player player, (Item item, double heal) t)
        {
            return t.heal < 1 
                   ? "    " + $"{player.Name, 10}".Trim() + " eats a "+ $"{t.item.Name, 9}".Trim() + ". " +
                   $"It heals for [red]{t.heal.ToString("F0"), 2}[/] HP." 
                   : "    " + $"{player.Name, 10}".Trim() + $" eats a " + $"{t.item.Name, 9}".Trim() + ". " +
                   $"It heals for [green]{t.heal.ToString("F0"), 2}[/] HP.";
        }
        // Loadingscreen
        public static void LoadingGame ()
        {
            string[] values = ["[grey]   LOG:[/] [gold3]Loading npcs...[/]", "[grey]   LOG:[/] [greenyellow]Increasing difficulty...[/]",
                               "[grey]   LOG:[/] [green]Enabling WackMode....[/]", "[green]   LOG: Finished![/]"];
            Console.CursorVisible = false;
            AnsiConsole.Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Dots12)
            .SpinnerStyle(Style.Parse("gold3"))
            .Start("Loading...", ctx =>
            {
                AnsiConsole.MarkupLine(values[0]);
                Thread.Sleep(1500);

                AnsiConsole.MarkupLine(values[1]);
                Thread.Sleep(1500);

                AnsiConsole.MarkupLine(values[2]);
                Thread.Sleep(1500);
                AnsiConsole.MarkupLine(values[3]);
            });
            Console.CursorVisible = true;
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
