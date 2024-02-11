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
        public Dictionary<Item, int> Bag = new Dictionary<Item, int>();
        // Spelarens väska, lagrar objektet som key
        // och value indikerar antal av detta objekt som spelaren har i väskan.
        // Satt till Private eftersom endast spelarobjektet behöver ha koll på väskan.
        public Player(string name)
        {
            this.Symbol = " 0 ";
            this.Name = name;
            this.HP = 100;
            this.Power = 15;
        }
        // När man skapar ett spelar-objekt så lägger man även till symbol och namn.
        private object[,] ChangePosition(Entity[,] gameBoard, Player player)
        {
            gameBoard[player.PosX, player.PosY] = player;
            return gameBoard;
        }
        // Denna funktion tar in gameboard samt position,
        // och returnerar ett nytt gameboard med spelarens nya position.
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
                    OpenBag(keyInfo, player);
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

                ControllCollision(gameBoard, player, grid);
                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.

                gameBoard = (Entity[,])ChangePosition(gameBoard, player);
                grid.DrawGridBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.
            }
        }
        private void ControllCollision(Entity[,] gameBoard, Player player, Grid grid)
        {
            //Samlas en lista av objekt 
            List<Entity[]> entities = new List<Entity[]>()
            {
                {grid.npcs },
                {grid.wears },
                {grid.consumables }
            };

            for (int i = 0; i < entities.Count; i++)
            {
                Entity[] entityArray = entities[i];
                // array som skrivs över vid varje varv, tar en array i taget från listan entities.

                for (int j = 0; j < entityArray.Length; j++)
                {
                    if (entityArray[j] is Consumable consumables) 
                        // Kontroll som kollar vilken typ den nuvarande arrayen är av.
                    {
                        if (gameBoard[player.PosX, player.PosY] == consumables)
                            //kontrolleras om player träffas en specifik symbol från denna array.
                        {
                            consumables.OnPickup(consumables, player);
                            break;
                        }
                    }
                    else if (entityArray[j] is Wearable wears)
                    {
                        if (gameBoard[player.PosX, player.PosY] == wears)
                        {
                            wears.OnPickup(wears, player);
                            break;
                        }
                    }
                    else if (entityArray[j] is NPC npcs)
                    {
                        if (gameBoard[player.PosX, player.PosY] == npcs)
                        {
                            Encounter(npcs, player);
                            break;
                        }
                    }
                }
            }
        }
        //public void PickUp (Consumable item, Player player)
        //{
        //    Console.WriteLine($"{player.Name} picked up a {item.Name}");
        //    if (!Bag.ContainsKey(item)) 
        //    {
        //        Bag.Add(item, item.Amount);
        //    }
        //    else
        //    {
        //        Bag[item]++;
        //    }
        //}

        public void Encounter (NPC npc, Player player)
        {
            bool run = true;

            while (run)
            {
                if (player.HP < 1) 
                {
                    player.HP = 0;
                    Console.WriteLine($"{player.Name} died and {npc.Name} has {npc.HP} left.");
                    run = false;
                    break;
                }
                else if (npc.HP < 1)
                {
                    npc.HP = 0;
                    Console.WriteLine($"{npc.Name} died and player has {player.HP} left.");
                    run = false;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(
                        $"{player.Name}:{player.HP}HP VERSUS " +
                        $"{npc.Name}:{npc.HP}HP \n");

                    //Spelaren kan välja mellan 3 olika alt
                    Console.WriteLine("What do you want to do? \n" +
                        "1. Punch \n" +
                        "2. Block \n" +
                        "3. Escape \n" +
                        "4. Use item");
                    bool successfulInput = Int32.TryParse(Console.ReadLine(), out int choice);
                    int computerChoice = new Random().Next(1, 4);
                    switch (choice)
                    {
                        case 1:
                            if (computerChoice == 2)
                            {
                                Console.WriteLine(
                                    $"{npc.Name} blocks the attack! " +
                                    $"{player.Name} HP: {player.HP}, " +
                                    $"{npc.Name} HP: {npc.HP}");
                            }
                            else
                            {
                                Console.WriteLine($"{player.Name} attacks with {player.Power} power");
                                npc.HP -= player.Power;
                                Console.WriteLine(
                                    $"{player.Name}s HP: {player.HP}," +
                                    $" {npc.Name} HP:{npc.HP}!");
                            }
                            Console.ReadLine();
                        break;

                        case 2: Console.WriteLine($"{player.Name} blocks {npc.Name}'s attack!");
                            Console.ReadLine();
                        break;

                        case 3:
                            Console.WriteLine($"{player.Name} runs for his life...");
                            Console.ReadLine();
                            run = false;
                        break;

                        case 4:
                            
                            foreach (var item in Bag)
                            {
                                Console.WriteLine($"{item.Key.Name} {item.Value}");
                            }
                            Console.WriteLine("Which item do you want to use?");
                            // Skriver ut en lista av items man har i Bag.
                            string? consumableChoice = Console.ReadLine();
                            // Kollar sedan igenom Bag igen, och ifall input matchar
                            // något som finns i Bag så används detta item. Ifall man har fler än två
                            // så minskar antalet med ett.
                            foreach (var item in Bag)
                            {
                                if (consumableChoice == item.Key.Name)
                                {
                                    Console.WriteLine(
                                        $"{player.Name} ate {item.Key.Name}. " +
                                        $"It healed {item.Key.HealAmount}HP.");
                                    player.HP += item.Key.HealAmount;
                                    if (item.Value == 1)
                                    {
                                        Bag.Remove(item.Key);
                                        Console.ReadLine();
                                        break;
                                    }
                                    else
                                    {
                                        Bag[item.Key]--;
                                        Console.ReadLine();
                                        break;
                                    }
                                    
                                }
                                else if (!Bag.ContainsKey(item.Key))
                                {
                                    Console.WriteLine($"You don't have any {consumableChoice} in your bag.");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                        break;
                    }
                    //NPCs tur att agera mot spelaren
                    switch (computerChoice)
                    {
                        case 1:
                            if (choice == 2)
                            {
                                Console.WriteLine(
                                    $"{player.Name} blocks the attack! " +
                                    $"{player.Name} HP: {player.HP}, " +
                                    $"{npc.Name} HP: {npc.HP}");
                            }
                            else
                            {
                                Console.WriteLine($"{npc.Name} attacks with {npc.Power} power");
                                player.HP -= npc.Power;
                                Console.WriteLine(
                                    $"{player.Name} HP: {player.HP}," +
                                    $" {npc.Name} HP: {npc.HP}!");
                                Console.ReadLine();
                            }
                        break;

                        case 2:
                            Console.WriteLine(
                                $"{npc.Name} blocks the attack! " +
                                $"{player.Name} HP: {player.HP}, " +
                                $"{npc.Name} HP: {npc.HP}");
                        break;

                        case 3:
                            if (npc.HP < (npc.HP * 0.1))
                            {
                                Console.WriteLine($"{npc.Name} runs for his life...");
                                run = false;
                            }
                        break;
                    }
                }
            }
        }
        public void OpenBag (ConsoleKeyInfo keyInfo, Player player)
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
                     $"{item.Key.Amount}" +
                     $"{spacerArray[0]}" +
                     $"{item.Key.Equipped}"
                     );
                // Adderar nödvändig info till showBag. För att detta skulle funka var jag tvungen 
                // att ändra i Entity så att Name inte kan vara null.
                items[index] = item.Key;
                index++;
                // Lägger till Item i arrayen.
            }
            var bagChoice = AnsiConsole.Prompt(showBag);
            // Gör det möjligt att välja Item med piltangenterna, och sparar valet man gör med enter.
            foreach (Item item in items)
            {
                if (bagChoice.Contains(item.Name) && item.Wear == false)
                {
                    Console.WriteLine($"{player.Name} eats a {item.Name}. It heals for {item.HealAmount}.");
                    player.HP += item.HealAmount;
                    Consume(player, item);
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                }
                else if ((bagChoice.Contains(item.Name) && item.Wear == true) && item.Equipped == true)
                {
                    Unequip(player, item);
                }
                else if ((bagChoice.Contains(item.Name) && item.Wear == true) && item.Equipped == false)
                {
                    Equip(player, item);
                }
            }
        }
        public void Consume(Player player, Item item)
        {
            if (player.Bag.ContainsKey(item) && player.Bag[item] > 1)
            {
                item.Amount--;
            }
            else
            {
                player.Bag.Remove(item);
            }
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
        //public void Wear (Wearable wears, Player player)
        //{
        //    Console.WriteLine
        //        ($"{player.Name} is now wearing {wears.Name}" +
        //        $" which gives {wears.PowerAdded} power and " +
        //        $"{wears.HpBoost} additional HP.");
        //    player.HP += wears.HpBoost;
        //    player.Power += wears.PowerAdded;
        //}
    }
}
