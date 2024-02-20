using SaleAdventure3000.Items;
using System.Security.Cryptography.X509Certificates;

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
            this.SymbolGetSet = " 0 ";
            this.NameGetSet = name;
            this.HPGetSet = 100;
            this.PowerGetSet = 15;
            this.score = 0;
        }
        
        public void MovePlayer(Entity[,] gameBoard, int firstX, int firstY, Player player, Grid grid)
        {
            // Startposition för spelaren anges när metoden anropas.
            player.PosXGetSet = firstX;
            player.PosYGetSet = firstY;
            gameBoard[PosXGetSet, PosYGetSet] = player;
            
            while (Run)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                string line = "¤¤¤";
                if (gameBoard[PosXGetSet, PosYGetSet].CanPass == true)
                {
                    // Ersätter spelarens gamla position med ett -
                    gameBoard[PosXGetSet, PosYGetSet] = new Obstacle(" - ");
                    //gameBoard[PosXGetSet, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
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
                    keyInfo.Key == ConsoleKey.W) && PosXGetSet > 1)
                {
                    if (gameBoard[PosXGetSet - 1, PosYGetSet].SymbolGetSet != line)
                    {
                        PosXGetSet--;

                        //if (gameBoard[PosXGetSet - 1, PosYGetSet].SymbolGetSet == line ||
                        //    gameBoard[PosXGetSet - 1, PosYGetSet].SymbolGetSet == "===")
                        //{
                        //    gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#000000";
                        //}
                        //else
                        //{
                        //    gameBoard[PosXGetSet + 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet + 1].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet - 1].BackgroundColorGetSet = "#70cfcf";
                        //}

                    }
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosXGetSet < 20)
                {
                    if (gameBoard[PosXGetSet + 1, PosYGetSet].SymbolGetSet != line)
                    {
                        PosXGetSet++;
                        //if (gameBoard[PosXGetSet + 1, PosYGetSet].SymbolGetSet == line ||
                        //    gameBoard[PosXGetSet + 1, PosYGetSet].SymbolGetSet == "===")
                        //{
                        //    gameBoard[PosXGetSet + 1, PosYGetSet].BackgroundColorGetSet = "#000000";
                        //}
                        //else
                        //{
                        //    gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet + 1].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet - 1].BackgroundColorGetSet = "#70cfcf";
                        //}
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosYGetSet < 20)
                {
                    if (gameBoard[PosXGetSet, PosYGetSet + 1].SymbolGetSet != line) 
                    {
                        PosYGetSet++;
                        //if (gameBoard[PosXGetSet, PosYGetSet + 1].SymbolGetSet == line ||
                        //    gameBoard[PosXGetSet, PosYGetSet + 1].SymbolGetSet == " | ")
                        //{
                        //    gameBoard[PosXGetSet, PosYGetSet + 1].BackgroundColorGetSet = "#000000";
                        //}
                        //else
                        //{
                        //    gameBoard[PosXGetSet + 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet - 1].BackgroundColorGetSet = "#70cfcf";
                        //}
                    }
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosYGetSet > 1)
                {
                    if (gameBoard[PosXGetSet, PosYGetSet - 1].SymbolGetSet != line)
                    {
                        PosYGetSet--;
                        //if (gameBoard[PosXGetSet, PosYGetSet - 1].SymbolGetSet == line ||
                        //    gameBoard[PosXGetSet, PosYGetSet - 1].SymbolGetSet == " | ")
                        //{
                        //    gameBoard[PosXGetSet, PosYGetSet - 1].BackgroundColorGetSet = "#000000";
                        //}
                        //else
                        //{
                        //    gameBoard[PosXGetSet + 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                        //    gameBoard[PosXGetSet, PosYGetSet + 1].BackgroundColorGetSet = "#70cfcf";
                        //}
                    }
                }

                //List<int[]> skit = new List<int[]>() { { [ -1, 0] }, { [1, 0] }, { [0, -1] }, { [0, 1] }, { [-1, 0] }, { [0, -1] }, { [0, 1] }, { [1, 0] } };

                //if ((gameBoard[PosXGetSet - 1, PosYGetSet].SymbolGetSet == line) ||
                //    (gameBoard[PosXGetSet + 1, PosYGetSet].SymbolGetSet == line) ||
                //    (gameBoard[PosXGetSet, PosYGetSet - 1].SymbolGetSet == line) ||
                //    (gameBoard[PosXGetSet, PosYGetSet + 1].SymbolGetSet == line) ||
                //    (gameBoard[PosXGetSet - 1, PosYGetSet].SymbolGetSet == "===") ||
                //    (gameBoard[PosXGetSet, PosYGetSet - 1].SymbolGetSet == " | ") ||
                //    (gameBoard[PosXGetSet, PosYGetSet + 1].SymbolGetSet == " | ") ||
                //    (gameBoard[PosXGetSet + 1, PosYGetSet].SymbolGetSet == "==="))
                //{
                //    for (int i = 0; i < skit.Count(); i++) 
                //    { 
                //        for (int j = 0; j < 2; j++)
                //        {
                //            gameBoard[PosXGetSet + skit[i][j], PosYGetSet + skit[i][j]].BackgroundColorGetSet = "#000000";
                //        }
                //    }
                //    //gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#000000";
                //}
                //else
                //{
                //    //for (int i = 0; i < skit.Count(); i++)
                //    //{
                //    //    for (int j = 0; j < 2; j++)
                //    //    {
                //    //        gameBoard[PosXGetSet + skit[i][j], PosYGetSet + skit[i][j]].BackgroundColorGetSet = "#70cfcf";
                //    //    }
                //    //}
                    
                //    gameBoard[PosXGetSet + 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                //    gameBoard[PosXGetSet - 1, PosYGetSet].BackgroundColorGetSet = "#70cfcf";
                //    gameBoard[PosXGetSet, PosYGetSet + 1].BackgroundColorGetSet = "#70cfcf";
                //    gameBoard[PosXGetSet, PosYGetSet - 1].BackgroundColorGetSet = "#70cfcf";
                //}

                // Tar endast in player objektet istället för PosX, PosY 
                // Eftersom man kommer åt dessa genom player. Samma för ChangePosition.
                Mechanics.ControlCollision(gameBoard, player, grid);

                // Ändrar spelarens position och ritar upp gameBoard igen.
                gameBoard = Mechanics.ChangePosition(gameBoard, player);
                Console.WriteLine($"                             {player.NameGetSet}'s HP: {player.HPGetSet}");
                grid.DrawGameBoard(gameBoard, player.PosXGetSet, player.PosYGetSet);
            }
        }

        public static void OpenBag (Player player)
        {   
            // Gör det möjligt att välja Item med piltangenterna, och sparar valet man gör med enter.
            string bagChoice = Mechanics.PrintBagMenuAndReturnChoice(player);

            foreach (var item in player.Bag)
            {
                if (bagChoice.Contains(item.Key.NameGetSet) && item.Key.WearGetSet == false)
                {
                    // Kollar ifall föremålets namn stämmer överens med valet, isåfall så
                    // äter man upp föremålet och det tas bort ifrån bagen med metoden Consume().
                    Console.WriteLine($"{player.NameGetSet} eats a {item.Key.NameGetSet}. It heals for {item.Key.HealAmountGetSet}.");
                    player.HPGetSet += item.Key.HealAmountGetSet;
                    player.Consume(player, item.Key);
                }
                else if ((bagChoice.Contains(item.Key.NameGetSet) && item.Key.WearGetSet == true) && item.Key.EquippedGetSet == true)
                {
                    // Kollar ifall föremålet är wearable samt ifall det redan är equipped.
                    player.Unequip(player, item.Key);
                }
                else if ((bagChoice.Contains(item.Key.NameGetSet) && item.Key.WearGetSet == true) && item.Key.EquippedGetSet == false)
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
            Console.WriteLine($"{player.NameGetSet} unequips " +
                              $"{item.NameGetSet}, losing {item.HpBoostGetSet} HP " +
                              $"and {item.PowerAddedGetSet} power.");
            item.EquippedGetSet = false;
            player.HPGetSet -= item.HpBoostGetSet;
            player.PowerGetSet -= item.PowerAddedGetSet;
        }

        public void Equip (Player player, Item item)
        {
            Console.WriteLine($"{player.NameGetSet} equips " +
                              $"{item.NameGetSet}, gaining {item.HpBoostGetSet} HP " +
                              $"and {item.PowerAddedGetSet} power.");
            item.EquippedGetSet = true;
            player.HPGetSet += item.HpBoostGetSet;
            player.PowerGetSet += item.PowerAddedGetSet;
        }
        // Getter för spelarens bag, eftersom bagen är private och inte synlig utanför
        // klassen player.
        public Dictionary<Item, int> GetBag ()
        {
            return this.Bag;
        }
    }
}
