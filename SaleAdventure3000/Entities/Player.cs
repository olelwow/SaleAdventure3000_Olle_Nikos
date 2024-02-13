using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SaleAdventure3000;
using SaleAdventure3000.Items;
using Spectre.Console;

namespace SaleAdventure3000.Entities
{
    public class Player : Creature
    {
        public bool Quit = true;
        private Dictionary<Item, int> Bag = new Dictionary<Item, int>();
        // Spelarens väska, lagrar objektet som key
        // och value indikerar antal av detta objekt som spelaren har i väskan.
        // Satt till Private eftersom endast spelarobjektet behöver ha koll på väskan.
        public int score = 0;
        public Player(string name)
        {
            this.Symbol = " 0 ";
            this.Name = name;
            this.HP = 100;
            this.Power = 15;
            this.score = 0;
        }
        // När man skapar ett spelar-objekt så lägger man även till namn.

        public void MovePlayer(Entity[,] gameBoard, int firstX, int firstY, Player player, Grid grid)
        {
            player.PosX = firstX;
            player.PosY = firstY;
            gameBoard[PosX, PosY] = player;
            // Startposition för spelaren anges när metoden anropas.
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[PosX, PosY] = new Entity() { Symbol = " - "};
                // Ersätter spelarens gamla position med ett -
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    player.Quit = false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.B)
                {
                    OpenBag(player);
                }
                else if ((keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W) && PosX > 1)
                {
                    PosX--;
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosX < 10)
                {
                    PosX++;
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosY < 10)
                {
                    PosY++;
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosY > 1)
                {
                    PosY--;
                }

                Mechanics.ControlCollision(gameBoard, player, grid);
                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.

                gameBoard = Mechanics.ChangePosition(gameBoard, player);
                grid.DrawGameBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.
            }
        }

        public static void OpenBag (Player player)
        {
            string[] spacerArray = ["        ", " ", "       ",
                                    "      ", " ",
                                    "      ", " ",
                                    "    ", "   ", " ",
                                    " ", " "];
            Item[] items = new Item[player.Bag.Count];
            // Array av items för jämförelse längre ner.
            var showBag = 
                new SelectionPrompt<string>()
                .Title("  Item     Wearable   Amount   Equipped")
                .PageSize(player.Bag.Count + 3);
            // Skapar ny SelectionPrompt, med rubrikerna ovan.
            int index = 0;

            foreach (var item in player.Bag)
            {
                showBag.AddChoice
                    ($"{item.Key.Name}" +
                     $"{spacerArray[item.Key.Name.Length + 4 - 2]}" +
                     $"{item.Key.Wear}" +
                     $"{spacerArray[item.Key.Wear.ToString().Length - 2]}" +
                     $"{item.Value}" +
                     $"{spacerArray[0]}" +
                     $"{item.Key.Equipped}"
                     );
                
                // Adderar nödvändig info till showBag. För att detta skulle funka var jag tvungen 
                // att ändra i Entity så att Name inte kan vara null.
                items[index] = item.Key;
                index++;
                // Lägger till Item i arrayen.
            }
            showBag.AddChoice("Close Bag");
            
            var bagChoice = AnsiConsole.Prompt(showBag);
            // Gör det möjligt att välja Item med piltangenterna, och sparar valet man gör med enter.
            
            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.Name) && item.Key.Wear == false)
                {
                    Console.WriteLine($"{player.Name} eats a {item.Key.Name}. It heals for {item.Key.HealAmount}.");
                    player.HP += item.Key.HealAmount;
                    player.Consume(player, item.Key);
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == true)
                {
                    player.Unequip(player, item.Key);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == false)
                {
                    player.Equip(player, item.Key);
                }
                else if (bagChoice.Equals("Close Bag"))
                {
                    break;
                }
            }
        }
        public void Consume(Player player, Item item)
        {
            if (player.Bag.ContainsKey(item) && player.Bag[item] > 1)
            {
                player.Bag[item]--;
            }
            else if (player.Bag[item] <= 1)
            {
                player.Bag.Remove(item);
            }
            // Sköter vad som händer när man använder en consumable. Finns det fler av
            // samma item i bagen så minskar value med 1, äter man den sista tas både
            // key och value bort från spelarens bag.
        }

        public void Unequip (Player player, Item item)
        {
            Console.WriteLine($"{player.Name} unequips " +
                              $"{item.Name}, losing {item.HpBoost} HP " +
                              $"and {item.PowerAdded} power.");
            item.Equipped = false;
            player.HP -= item.HpBoost;
            player.Power -= item.PowerAdded;
        }

        public void Equip (Player player, Item item)
        {
            Console.WriteLine($"{player.Name} equips " +
                              $"{item.Name}, gaining {item.HpBoost} HP " +
                              $"and {item.PowerAdded} power.");
            item.Equipped = true;
            player.HP += item.HpBoost;
            player.Power += item.PowerAdded;
        }
        public Dictionary<Item, int> GetBag ()
        {
            return this.Bag;
        }
        // Getter för spelarens bag, eftersom bagen är private och inte synlig utanför
        // klassen player.
    }
}
