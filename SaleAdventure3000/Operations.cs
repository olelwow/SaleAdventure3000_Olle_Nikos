using SaleAdventure3000.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SaleAdventure3000
{
    public class Operations
    {
        public static void Logo()
        {
            Console.WriteLine("*******************************\n" +
                    "Welcome to the Fight Simulator\n" +
                    "*******************************\n");
        }
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
            StreamWriter stream = new StreamWriter(@"C:\Users\nick_\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt", true);
            Player player = new Player(chosenName);
            stream.WriteLine($"Name : {chosenName} - Score : {player.score}");
            stream.Close();
        }
        public static void Login()
        {
            Console.Clear();
            Logo();
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();
            bool found = false;
            string[] lines = File.ReadAllLines(@"C:\Users\nick_\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt");
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

        internal static void Scoreboard()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\nick_\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.ReadLine();
        }

        public Operations() 
        {
            
        }
    }
}
