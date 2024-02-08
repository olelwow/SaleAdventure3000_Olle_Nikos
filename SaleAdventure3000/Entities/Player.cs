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
        public Player(string Symbol)
        {
            this.Symbol = Symbol;
            this.HP = 100;
            this.Power = 15;
        }
        // När man skapar ett spelar-objekt så lägger man även till symbol.

        public bool Quit = true;
        private Dictionary<string, int> Bag = new Dictionary<string, int>();

        // Spelarens väska, lagrar föremåls namn som key
        // och value indikerar ifall det är en consumable eller wearable.

        public string[,] ChangePosition(string[,] gameBoard, int posX, int posY)
        {
            gameBoard[posX, posY] = Symbol;
            return gameBoard;
        }
        
        // Denna funktion tar in gameboard samt position,
        // och returnerar ett nytt gameboard med spelarens nya position.

        public void MovePlayer(string[,] gameBoard, int firstX, int firstY, Player player, Game game)
        {

            PosX = firstX;
            PosY = firstY;
            gameBoard[PosX, PosY] = Symbol;
            // Startposition för spelaren anges när metoden anropas.

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[PosX, PosY] = " - ";
                // Ersätter spelarens gamla position med ett -
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    this.Quit = false;
                    break;
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

                ControllCollision(gameBoard, PosX, PosY, player, game);


                gameBoard = ChangePosition(gameBoard, PosX, PosY);


                DrawGameBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.


            }
        }

        public void ControllCollision(string[,] gameBoard, int PosX, int PosY, Player player, Game game)
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
                        if (gameBoard[PosX, PosY] == consumables.Symbol)
                            //kontrolleras om player träffas en specifik symbol från denna array.
                        {
                            PickUp();
                            break;
                        }
                    }
                    else if (entityArray[j] is Wearable wears)
                    {
                        if (gameBoard[PosX, PosY] == wears.Symbol)
                        {
                            Wear();
                            break;
                        }
                    }
                    else if (entityArray[j] is NPC npcs)
                    {
                        if (gameBoard[PosX, PosY] == npcs.Symbol)
                        {
                            Encounter(npcs, player);
                            break;
                        }
                    }
                }
            }
        }

        public void PickUp ()
        {
            Console.WriteLine("Picked up");
        }

        public void Encounter (NPC npc, Player player)
        {
            Console.WriteLine(npc.Power);
            bool run = true;

            while (run)
            {
                if(player.HP < 1) 
                {
                    Console.WriteLine($"Player died and npc has {npc.HP} left.");
                    run = false;
                    break;
                }
                else if (npc.HP < 1)
                {
                    Console.WriteLine($"NPC died and player has {player.HP} left.");
                    run = false;
                    break;
                }
                else
                {
                    Console.Clear();
                    //Spelaren kan välja mellan 3 olika alt
                    Console.WriteLine("What do you want to do? \n" +
                        "1. Punch \n" +
                        "2. Block \n" +
                        "3. Escape");
                    bool successfulInput = Int32.TryParse(Console.ReadLine(), out int choice);
                    int computerChoice = new Random().Next(1, 4);
                    switch (choice)
                    {
                        case 1:
                            if (computerChoice == 2)
                            {
                                Console.WriteLine($"NPC blocks the attack! NPC HP: {npc.HP}, player HP: {player.HP}");
                            }
                            else
                            {
                                Console.WriteLine($"Player attacks with {player.Power} power");
                                npc.HP -= player.Power;
                                Console.WriteLine($"NPCs HP är: {npc.HP}, player HP: {player.HP} !");
                            }
                            Console.ReadLine();
                            break;
                        case 2: Console.WriteLine($"Player blocks the NPCs attack!");
                            Console.ReadLine();
                                break;
                        case 3:
                            Console.WriteLine("Player runs for his life...");
                            Console.ReadLine();
                            run = false;
                                break;
                    }

                    //NPCs tur att agera mot spelaren

                    switch (computerChoice)
                    {
                        case 1:
                            if (choice == 2)
                            {
                                Console.WriteLine($"Player blocks the attack! NPC HP: {npc.HP}, player HP: {player.HP}");
                            }
                            else
                            {
                                Console.WriteLine($"NPC attacks with {npc.Power} power");
                                player.HP -= npc.Power;
                                Console.WriteLine($"NPCs HP är: {npc.HP}, player HP: {player.HP} !");
                                Console.ReadLine();
                            }
                            break;
                        case 2:
                            Console.WriteLine($"NPC blocks the attack! NPC HP: {npc.HP}, player HP: {player.HP}");
                            break;
                        case 3:
                            if (npc.HP < (npc.HP * 0.1))
                            {
                                Console.WriteLine("NPC runs for his life...");
                                run = false;
                            }
                            break;
                    }

                }

            }


        }

        public void Consume ()
        {

        }

        public void Wear ()
        {
            Console.WriteLine("Wearing");
        }
    }
}
