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
        }
        
        public void MovePlayer(Entity[,] gameBoard, int firstX, int firstY, Player player, Grid grid)
        {
            // Startposition för spelaren anges när metoden anropas.
            player.PosX = firstX;
            player.PosY = firstY;
            gameBoard[PosX, PosY] = player;
            
            while (Run)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                string line = "¤¤¤";
                if (gameBoard[PosX, PosY].CanPass == true)
                {
                    // Ersätter spelarens gamla position med ett -
                    gameBoard[PosX, PosY] = new Obstacle(" - ");
                }
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    player.Run = false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.B)
                {
                    OpenBag(player);
                }
                else if ((keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W) && PosX > 1)
                {
                    if (gameBoard[PosX - 1, PosY].Symbol != line)
                    {
                        PosX--;
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosX < 20)
                {
                    if (gameBoard[PosX + 1, PosY].Symbol != line)
                    {
                        PosX++;
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosY < 20)
                {
                    if (gameBoard[PosX, PosY +1].Symbol != line) 
                    {
                        PosY++;
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosY > 1)
                {
                    if (gameBoard[PosX, PosY - 1].Symbol != line)
                    {
                        PosY--;
                    }
                }
                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.
                Mechanics.ControlCollision(gameBoard, player, grid);

                // Ändrar spelarens position och ritar upp gameBoard igen.
                gameBoard = Mechanics.ChangePosition(gameBoard, player);
                Console.WriteLine($"                             {player.Name}'s HP: {player.HP}");
                grid.DrawGameBoard(gameBoard);
            }
        }

        public static void OpenBag (Player player)
        {   
            // Gör det möjligt att välja Item med piltangenterna, och sparar valet man gör med enter.
            string bagChoice = Mechanics.PrintBagMenuAndReturnChoice(player);

            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.Name) && item.Key.Wear == false)
                {
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                    Console.WriteLine($"{player.Name} eats a {item.Key.Name}. It heals for {item.Key.HealAmount}.");
                    player.HP += item.Key.HealAmount;
                    player.Consume(player, item.Key);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == true)
                {
                    // Kollar ifall föremålet är wearable samt ifall det redan är equipped.
                    player.Unequip(player, item.Key);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == false)
                {
                    // Kollar ifall föremålet är wearable samt ifall det inte redan är equipped.
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
        // Getter för spelarens bag, eftersom bagen är private och inte synlig utanför
        // klassen player.
        public Dictionary<Item, int> GetBag ()
        {
            return this.Bag;
        }
    }
}
