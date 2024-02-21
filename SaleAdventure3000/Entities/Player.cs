using SaleAdventure3000.Items;
using System.Text.RegularExpressions;

namespace SaleAdventure3000.Entities
{
    public class Player : Creature
    {
        public bool Run = true;

        // Bag som lagrar objekt av typen Item som key, och en int som indikerar antal av
        // detta item som spelaren har i bagen.
        private Dictionary<Item, int> Bag = new Dictionary<Item, int>();
        
        public int score = 0;
        public Player(string name)
        {
            this.Symbol = " 0 ";
            this.Name = name;
            this.HP = 100;
            this.Power = 15;
            this.score = 0;
            this.BackgroundColor = "#968c4a";
        }
        
        public void MovePlayer(Entity[,] gameBoard, int firstX, int firstY, Player player, Grid grid)
        {
            // Startposition för spelaren anges när metoden anropas.
            player.PosX = firstX;
            player.PosY = firstY;
            gameBoard[PosX, PosY] = player;
            string color = "#968c4a";
            string bagChoice = "";
            

            Item chosenItem = new();
            while (Run)
            {
                bagChoice = "";
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                string line = "¤¤¤";
                
                if (gameBoard[PosX, PosY].CanPass == true)
                {
                    // Ersätter spelarens gamla position med ett -
                    gameBoard[PosX, PosY] = new Obstacle(" - ");
                    gameBoard[PosX, PosY].BackgroundColor = color;
                }
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    player.Run = false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.B)
                {
                    bagChoice = MenuOperations.
                        PrintBagMenuAndReturnChoice(player);
                    chosenItem = player.OpenBag(player, bagChoice);
                }
                else if ((keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W) && PosX > 1)
                {
                    if (gameBoard[PosX - 1, PosY].Symbol != line)
                    {
                        PosX--;

                        if (gameBoard[PosX - 1, PosY].Symbol == line ||
                            gameBoard[PosX - 1, PosY].Symbol == "===")
                        {
                            gameBoard[PosX - 1, PosY].BackgroundColor = "#000000";
                        }
                        else
                        {
                            gameBoard[PosX - 1, PosY].BackgroundColor = color;
                        }

                    }
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosX < 20)
                {
                    if (gameBoard[PosX + 1, PosY].Symbol != line)
                    {
                        PosX++;
                        if (gameBoard[PosX + 1, PosY].Symbol == line ||
                            gameBoard[PosX + 1, PosY].Symbol == "===")
                        {
                            gameBoard[PosX + 1, PosY].BackgroundColor = "#000000";
                        }
                        else
                        {
                            gameBoard[PosX + 1, PosY].BackgroundColor = color;
                        }
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosY < 20)
                {
                    if (gameBoard[PosX, PosY + 1].Symbol != line) 
                    {
                        PosY++;
                        if (gameBoard[PosX, PosY + 1].Symbol == line ||
                            gameBoard[PosX, PosY + 1].Symbol == " | ")
                        {
                            gameBoard[PosX, PosY + 1].BackgroundColor = "#000000";
                        }
                        else
                        {
                            gameBoard[PosX, PosY + 1].BackgroundColor = color;
                            
                        }
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosY > 1)
                {
                    if (gameBoard[PosX, PosY - 1].Symbol != line)
                    {
                        PosY--;
                        if (gameBoard[PosX, PosY - 1].Symbol == line ||
                            gameBoard[PosX, PosY - 1].Symbol == " | ")
                        {
                            gameBoard[PosX, PosY - 1].BackgroundColor = "#000000";
                        }
                        else
                        {
                            gameBoard[PosX, PosY - 1].BackgroundColor = color;
                        }
                    }
                }

                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.
                Mechanics.ControlCollision(gameBoard, player, grid);

                // Ändrar spelarens position och ritar upp gameBoard igen.
                gameBoard = Mechanics.ChangePosition(gameBoard, player);
                
                if (bagChoice == null || bagChoice == "")
                {
                    grid.DrawGameBoard(gameBoard, player);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player);
                }
                else
                {
                    grid.DrawGameBoard(gameBoard, player);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player, chosenItem);
                }
            }
        }

        public Item OpenBag (Player player, string bagChoice)
        {   
            // Gör det möjligt att välja Item med piltangenterna, och sparar valet man gör med enter.
            //string bagChoice = 
            //    MenuOperations
            //    .PrintBagMenuAndReturnChoice(player);

            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.Name) && item.Key.Wear == false)
                {
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                    //MenuOperations.PrintGameInfo(player, "Consume", item.Key);
                    player.Consume(player, item.Key);
                    return item.Key;
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == true)
                {
                    // Kollar ifall föremålet är wearable samt ifall det redan är equipped.
                    player.Unequip(player, item.Key);
                    return item.Key;
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == false)
                {
                    // Kollar ifall föremålet är wearable samt ifall det inte redan är equipped.
                    player.Equip(player, item.Key);
                    return item.Key;
                }
                else if (bagChoice.Equals("Close Bag"))
                {
                    return new Item();
                }
                
            }
            return new Item();
        }
        public void Consume(Player player, Item item)
        {
            player.HP += item.HealAmount;
            // Sköter vad som händer när man använder en consumable. Finns det fler av
            // samma item i bagen så minskar value med 1, äter man den sista tas både
            // key och value bort från spelarens bag.
            if (player.Bag.ContainsKey(item) && player.Bag[item] > 1)
            {
                player.Bag[item]--;
            }
            else if (player.Bag[item] <= 1)
            {
                player.Bag.Remove(item);
            }
        }

        public void Unequip (Player player, Item item)
        {
            //MenuOperations.PrintGameInfo(player, "Unequip", item);
            item.Equipped = false;
            player.HP -= item.HpBoost;
            player.Power -= item.PowerAdded;
        }

        public void Equip (Player player, Item item)
        {
            //MenuOperations.PrintGameInfo(player, "Equip", item);
            item.Equipped = true;
            player.HP += item.HpBoost;
            player.Power += item.PowerAdded;
        }
        
        // Getter för spelarens bag, eftersom bagen är private och inte synlig utanför
        // klassen player.
        public Dictionary<Item, int> GetBag ()
        {
            return this.Bag;
        }
    }
}