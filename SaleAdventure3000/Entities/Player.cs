using SaleAdventure3000.GameMechanics;
using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public class Player : Creature
    {
        public bool Run = true;
        private int evasion;
        private int armor;
        private int luck;

        // Bag som lagrar objekt av typen Item som key, och en int som indikerar antal av
        // detta item som spelaren har i bagen.
        private readonly Dictionary<Item, int> Bag = new();
        private int boostedHP = 0;
        public int score = 0;
        public Player(string name)
        {
            this.Symbol = " 0 ";
            this.Name = name;
            this.HP = 100;
            this.armor = 5;
            this.luck = 5;
            this.evasion = 5;
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
            Grid.DrawGameBoard(grid.gameBoard);
            Console.WriteLine();
            MenuOperations.PrintGameInfo(player);

            (Item, double) chosenItem = (new(), 0.0);
            (int, int) oldPos;
            string pathColor = "#968c4a";
            string blackColor = "#000000";
            string bagChoice;
            string maze = "¤¤¤";
            string horizontalWall = "===";
            string verticalWall = " | ";



            while (Run)
            {
                bagChoice = "";
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                
                oldPos = (player.PosX, player.PosY);
                if (gameBoard[PosX, PosY].CanPass == true)
                {
                    // Ersätter spelarens gamla position med ett -
                    gameBoard[PosX, PosY] = new Obstacle(" - ")
                    {
                        BackgroundColor = pathColor
                    };
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
                    if (bagChoice == "Close Bag")
                    {
                        bagChoice = "";
                    }
                    chosenItem = OpenBag(player, bagChoice);
                }
                else if ((keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W) && PosX > 1)
                {
                    if (gameBoard[PosX - 1, PosY].Symbol != maze)
                    {
                        PosX--;

                        if (gameBoard[PosX - 1, PosY].Symbol == maze ||
                            gameBoard[PosX - 1, PosY].Symbol == horizontalWall
                            )
                        {
                            gameBoard[PosX - 1, PosY].BackgroundColor = blackColor;
                        }
                        else
                        {
                            gameBoard[PosX - 1, PosY].BackgroundColor = pathColor;
                        }

                    }
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosX < 20)
                {
                    if (gameBoard[PosX + 1, PosY].Symbol != maze)
                    {
                        PosX++;
                        if (gameBoard[PosX + 1, PosY].Symbol == maze ||
                            gameBoard[PosX + 1, PosY].Symbol == horizontalWall
                            )
                        {
                            gameBoard[PosX + 1, PosY].BackgroundColor = blackColor;
                        }
                        else
                        {
                            gameBoard[PosX + 1, PosY].BackgroundColor = pathColor;
                        }
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosY < 20)
                {
                    if (gameBoard[PosX, PosY + 1].Symbol != maze)
                    {
                        PosY++;
                        if (gameBoard[PosX, PosY + 1].Symbol == maze ||
                            gameBoard[PosX, PosY + 1].Symbol == verticalWall)
                        {
                            gameBoard[PosX, PosY + 1].BackgroundColor = blackColor;
                        }
                        else
                        {
                            gameBoard[PosX, PosY + 1].BackgroundColor = pathColor;

                        }
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosY > 1)
                {
                    if (gameBoard[PosX, PosY - 1].Symbol != maze)
                    {
                        PosY--;
                        if (gameBoard[PosX, PosY - 1].Symbol == maze ||
                            gameBoard[PosX, PosY - 1].Symbol == verticalWall)
                        {
                            gameBoard[PosX, PosY - 1].BackgroundColor = blackColor;
                        }
                        else
                        {
                            gameBoard[PosX, PosY - 1].BackgroundColor = pathColor;
                        }
                    }
                }
                // Ifall man springer från ett encounter får man tillbaka siffran 3, som används
                // nedan för att ta tillbaka spelaren till sin gamla position och NPC står kvar.
                int playerEscape = Mechanics.ControlCollision(gameBoard, player, grid);
                if (playerEscape != 3)
                {
                    // Ändrar spelarens position och ritar upp gameBoard igen.
                    gameBoard = Mechanics.ChangePosition(gameBoard, player);
                }
                else
                {
                    // Hoppar tillbaka till gammal position.
                    player.PosX = oldPos.Item1;
                    player.PosY = oldPos.Item2;
                    gameBoard = Mechanics.ChangePosition(gameBoard, player);
                }
                if (bagChoice == "")
                {
                    // Ifall bagChoice är tom så vet vi att vi inte valt något item från Bag.
                    Grid.DrawGameBoard(gameBoard);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player);
                }
                else
                {
                    // Annars skickas det valda itemet in i metoden nedan.
                    Grid.DrawGameBoard(gameBoard);
                    Console.WriteLine("");
                    MenuOperations.PrintGameInfo(player, chosenItem);
                }
            }
        }
        // Ändrade denna metod till att returnera en Tuple av Item och value som representerar
        // Hur mycket HP man healade eftersom det inte går att gå över 100hp.
        public static (Item item, double value) OpenBag(Player player, string bagChoice)
        {
            // Vi får in det valda föremålet genom strängen bagChoice, som sedan kontrolleras
            // och matchas med ett item i bagen.
            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.Name) && item.Key.Wear == false)
                {
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                    double heal = Consume(player, item.Key);
                    return (item.Key, heal);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == true)
                {
                    // Kollar ifall föremålet är wearable samt ifall det redan är equipped.
                    Unequip(player, item.Key);
                    return (item.Key, 0.0);
                }
                else if ((bagChoice.Contains(item.Key.Name) && item.Key.Wear == true) && item.Key.Equipped == false)
                {
                    // Kollar ifall föremålet är wearable samt ifall det inte redan är equipped.
                    Equip(player, item.Key);
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
        public static double Consume(Player player, Item item)
        {
            double healedHP = player.HP;
            double maxHP = 100 + player.boostedHP;
            if (player.HP <= 100)
            {
                player.HP = Math.Min(100, player.HP + item.HealAmount);
                healedHP = player.HP - healedHP;
            }
            else if (player.HP < maxHP)
            {
                player.HP = Math.Min(maxHP, player.HP + item.HealAmount);
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
        public static void Unequip(Player player, Item item)
        {
            //MenuOperations.PrintGameInfo(player, "Unequip", item);
            item.Equipped = false;
            player.HP -= item.HpBoost;
            player.boostedHP -= item.HpBoost;
            player.Power -= item.PowerAdded;
            player.Evasion -= item.Evasion;
            player.Armor -= item.Armor;
            player.Luck -= item.Luck;
        }
        public static void Equip(Player player, Item item)
        {
            //MenuOperations.PrintGameInfo(player, "Equip", item);
            item.Equipped = true;
            player.HP += item.HpBoost;
            player.boostedHP += item.HpBoost;
            player.Power += item.PowerAdded;
            player.Evasion += item.Evasion;
            player.Armor += item.Armor;
            player.Luck += item.Luck;
        }

        // Getter för spelarens bag, eftersom bagen är private och inte synlig utanför
        // klassen player.
        public Dictionary<Item, int> GetBag()
        {
            return this.Bag;
        }
        public int BoostedHP
        {
            get { return this.boostedHP; }
            set { this.boostedHP = value;}
        }
        public int Evasion
        {
            get { return this.evasion; }
            set { this.evasion = value;}
        }
        public int Armor
        {
            get { return this.armor; }
            set { this.armor = value; }
        }
        public int Luck
        {
            get { return this.luck; }
            set { this.luck = value; }
        }
    }
}