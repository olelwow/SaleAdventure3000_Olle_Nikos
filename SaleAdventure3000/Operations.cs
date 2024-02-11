using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string name = Console.ReadLine();
            StreamWriter stream = new StreamWriter(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt");
            stream.WriteLine(name);
            stream.Close();
        }
        public static void Login()
        {
            Console.Clear();
            Logo();
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            bool found = false;
            string[] lines = File.ReadAllLines(@"C:\Users\Sheikah Slate\source\repos\SaleAdventure3000_Olle_Nikos\SaleAdventure3000\Login.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == username)
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
                game.StartGame();
            }
            else
            {

                Console.WriteLine("\n\tInvalid username");
                Console.ReadLine();
            }
        }
        public Operations() 
        {
            
        }
    }
}
