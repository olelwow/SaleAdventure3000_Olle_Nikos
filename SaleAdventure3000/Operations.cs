using SaleAdventure3000.Entities;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SaleAdventure3000
{
    //Den här klassen innehåller bas metoder som påverkar utskrift av medelande
    public class Operations
    {
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
            string chosenName = Console.ReadLine();
            while (chosenName == null || chosenName.Length < 3)
            {
                Console.WriteLine("Invalid name, must be at least 3 character long.");
                Console.WriteLine("What is your name, adventurer?");
                chosenName = Console.ReadLine();
            }
            StreamWriter stream = new StreamWriter(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt", true);
            //Player player = new Player(chosenName);
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
            string[] lines = File.ReadAllLines(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(username))
                {
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                Console.WriteLine("\n\tLoging succeed");
                Thread.Sleep(1500);
                Game game = new Game();
                game.StartGame(username);
            }
            else
            {

                Console.WriteLine("\n\tInvalid username");
                Console.ReadLine();
            }
            
        }
        //Scoreboard metoden skriver ut scoreboard av alla sp
        internal static void Scoreboard()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Scoreboard.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.ReadLine();
        }

        public Operations() 
        {
            
        }

        public static string Attack (Player player, NPC npc)
        {
            if(player.HP > 0)
            {
                npc.HP -= player.Power;
                return $"{player.Name} attacks with {player.Power} power" +
                   $"\n{player.Name}s HP: {player.HP}," +
                   $" {npc.Name} HP:{npc.HP}!";
            }
            return "";
            // Metod som sköter spelarens attack.
        }
        public static string Attack(NPC npc, Player player)
        {
            if (npc.HP > 0)
            {
                player.HP -= npc.Power;
                return $"{npc.Name} attacks with {npc.Power} power" +
                   $"\n{player.Name} HP: {player.HP}," +
                   $" {npc.Name} HP: {npc.HP}!";
            }
            return "";
            // Metod som sköter NPCs attack, ifall npc redan dött returneras en tom sträng.
        }
        public static string Block (NPC npc, Player player)
        {
            return $"{npc.Name} blocks the attack! " +
                   $"\n{player.Name} HP: {player.HP}, " +
                   $"{npc.Name} HP: {npc.HP}";
        }
        public static string Block (Player player, NPC npc)
        {
            return $"{player.Name} blocks the attack! " +
                   $"\n{player.Name} HP: {player.HP}, " +
                   $"{npc.Name} HP: {npc.HP}";
        }
        public static string Death (Player player, NPC npc)
        {
            if (player.HP < 1)
            {
                player.HP = 0;
                return $"{player.Name} died and {npc.Name} has {npc.HP} HP left.";
            }
            else if (npc.HP < 1)
            {
                npc.HP = 0;
                player.score += 10;
                Operations.ScoreBoardReg(player);
                return $"{npc.Name} died and player has {player.HP} HP left.";
            }
            return "";
        }
        
        public static int PrintChoiceMenu (string choice1, string choice2, string choice3, string choice4)
        {
            Dictionary<string, int> choice = new Dictionary<string, int>()
            { {choice1, 1},
              {choice2, 2 },
              {choice3, 3 },
              {choice4, 4 }
            };
            // Dictionary för att koppla ihop respektive val med en siffra.
            var displayMenu = 
                AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Make your choice")
                .PageSize(5)
                .AddChoices(new[] 
                {
                    choice1, choice2, choice3, choice4 
                }));
            return choice[displayMenu];
            // Returnerar värdet i choice som hör ihop med rätt menyval.
        }

        internal static void ScoreBoardReg(Player player)
        {
            StreamReader reader = new StreamReader(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Scoreboard.txt");
            string line = "";
            List<string> lines = new List<string>();
            List<string> newLines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
                newLines.Add(line);
            }
            reader.Close();

            StreamWriter writer = new StreamWriter(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Scoreboard.txt");

            if (lines.Count > 0) 
            {
                foreach (string row in lines)
                {
                    if (row.Contains(player.Name))
                    {
                        int newPoints = player.score;
                        //int index = row.LastIndexOf(row);
                        int index = row.Length - 2 ;
                        int currentPoints= int.Parse(row.Substring(index, 2));
                        
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
    }
}