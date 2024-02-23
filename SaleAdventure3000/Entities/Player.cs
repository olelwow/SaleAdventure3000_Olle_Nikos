using SaleAdventure3000.Items;

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
            grid.DrawGameBoard(grid.gameBoard, player);
            Console.WriteLine();
            MenuOperations.PrintGameInfo(player);

            string color = "#968c4a";
            string bagChoice;
            (Item, double) chosenItem = (new(), 0.0);

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
                
                if (bagChoice == "")
                {
                    // Ifall bagChoice är tom så vet vi att vi inte valt något item från Bag.
                    grid.DrawGameBoard(gameBoard, player);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player);
                }
                else
                {
                    // Annars skickas det valda itemet in i metoden nedan.
                    grid.DrawGameBoard(gameBoard, player);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player, chosenItem);
                }
            }
        }
        // Ändrade denna metod till att returnera en Tuple av Item och value som representerar
        // Hur mycket HP man healade eftersom det inte går att gå över 100hp.
        public (Item item, double value) OpenBag (Player player, string bagChoice)
        {   
            // Vi får in det valda föremålet genom strängen bagChoice, som sedan kontrolleras
            // och matchas med ett item i bagen.
            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.Name) && item.Key.Wear == false)
                {
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                    double heal = player.Consume(player, item.Key);
                    return (item.Key, heal);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == true)
                {
                    // Kollar ifall föremålet är wearable samt ifall det redan är equipped.
                    player.Unequip(player, item.Key);
                    return (item.Key, 0.0);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == false)
                {
                    // Kollar ifall föremålet är wearable samt ifall det inte redan är equipped.
                    player.Equip(player, item.Key);
                    return (item.Key, 0.0);
                }
                // Returnerar ett tomt item ifall man väljer att stänga bagen.
                else if (bagChoice.Equals("Close Bag"))
                {
                    return (new Item(), 0.0);
                }
            }
            return (new Item(), 0.0);
        }
        public double Consume(Player player, Item item)
        {
            double healedHP = player.HP;
            if (player.HP <= 100)
            {
                player.HP = Math.Min(100, player.HP + item.HealAmount);
                healedHP = player.HP - healedHP;
            }
            else
            {
                healedHP = 0.0;
            }
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
            // returnerar antal HP som man healat, ifall det är mindre än föremålets
            // grundvärde för HealAmount.
            return healedHP;
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