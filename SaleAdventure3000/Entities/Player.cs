using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SaleAdventure3000;
using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    internal class Player : Creature
    {
        public bool Quit = true;
        private Dictionary<Item, int> Bag = new Dictionary<Item, int>();
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
        public string[,] ChangePosition(string[,] gameBoard, Player player)
        {
            gameBoard[player.PosX, player.PosY] = player.Symbol;
            return gameBoard;
        }
        // Denna funktion tar in gameboard samt position,
        // och returnerar ett nytt gameboard med spelarens nya position.
        public void MovePlayer(string[,] gameBoard, int firstX, int firstY, Player player, Game game)
        {
            player.PosX = firstX;
            player.PosY = firstY;
            gameBoard[PosX, PosY] = player.Symbol;
            // Startposition för spelaren anges när metoden anropas.
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[PosX, PosY] = " - ";
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
                ControllCollision(gameBoard, player, game);
                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.

                gameBoard = ChangePosition(gameBoard, player);
                game.DrawGameBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.
            }
        }
        public void ControllCollision(string[,] gameBoard, Player player, Game game)
        {
            //Samlas en lista av objekt 
            List<Object[]> entities = new List<Object[]>()
            {
                {game.npcs },
                {game.wears },
                {game.consumables }
            };

            for (int i = 0; i < entities.Count; i++)
            {
                Object[] entityArray = entities[i];
                // array som skrivs över vid varje varv, tar en array i taget från listan entities.

                for (int j = 0; j < entityArray.Length; j++)
                {
                    if (entityArray[j] is Consumable consumables) 
                        // Kontroll som kollar vilken typ den nuvarande arrayen är av.
                    {
                        if (gameBoard[player.PosX, player.PosY] == consumables.Symbol)
                            //kontrolleras om player träffas en specifik symbol från denna array.
                        {
                            PickUp(consumables, player);
                            break;
                        }
                    }
                    else if (entityArray[j] is Wearable wears)
                    {
                        if (gameBoard[player.PosX, player.PosY] == wears.Symbol)
                        {
                            Wear(wears, player);
                            break;
                        }
                    }
                    else if (entityArray[j] is NPC npcs)
                    {
                        if (gameBoard[player.PosX, player.PosY] == npcs.Symbol)
                        {
                            Encounter(npcs, player);
                            break;
                        }
                    }
                }
            }
        }
        public void PickUp (Consumable item, Player player)
        {
            Console.WriteLine($"{player.Name} picked up a {item.Name}");
            if (!Bag.ContainsKey(item)) 
            {
                Bag.Add(item, item.Amount);
            }
            else
            {
                Bag[item]++;
            }
            
        }
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
                                    }
                                    else
                                    {
                                        Bag[item.Key]--;
                                    }
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine($"You don't have any {consumableChoice} in your bag.");
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
            Console.WriteLine("ITEM       HEALAMOUNT     AMOUNT");
            string[] spacerArray = ["     ", "        ", "             "];
            foreach (var item in player.Bag)
            {
                if (item.Key.Name.Length == 3)
                {
                    Console.WriteLine(
                        $"{item.Key.Name}{spacerArray[1]}" +
                        $"{item.Key.HealAmount}{spacerArray[2]}{item.Value}");
                }
                else
                {
                    Console.WriteLine(
                        $"{item.Key.Name}{spacerArray[0]}" +
                        $"{item.Key.HealAmount}{spacerArray[2]}{item.Value}");
                }
            }

            if (keyInfo.Key == ConsoleKey.B)
            {
                CloseBag();
            }
            else
            {
                Console.ReadLine();
            }
        }
        public void CloseBag ()
        {

        }
        public void Consume ()
        {

        }
        public void Wear (Wearable wears, Player player)
        {
            Console.WriteLine
                ($"{player.Name} is now wearing {wears.Name}" +
                $" which gives {wears.PowerAdded} power and " +
                $"{wears.HpBoost} additional HP.");
            player.HP += wears.HpBoost;
            player.Power += wears.PowerAdded;
        }
    }
}
