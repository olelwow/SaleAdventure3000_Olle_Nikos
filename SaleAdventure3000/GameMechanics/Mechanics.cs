using SaleAdventure3000;
using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using Spectre.Console;

public abstract class Mechanics
{
    public static void ControlCollision(Entity[,] gameBoard, Player player, SaleAdventure3000.Entities.Grid grid)
    {
        // Tar in arrays från Grid som innehåller alla objekt och deras positioner.
        List<Entity[]> entities = new List<Entity[]>()
        {
                {grid.npcs },
                {grid.wears },
                {grid.consumables },
                {grid.goal}
        };

        for (int i = 0; i < entities.Count; i++)
        {   
            // array som skrivs över vid varje varv, tar en array i taget från listan entities.
            Entity[] entityArray = entities[i];

            for (int j = 0; j < entityArray.Length; j++)
            {   
                // Kontroll som kollar vilken typ den nuvarande arrayen är av.
                if (entityArray[j] is Consumable consumables)
                {   
                    // Kontrolleras om player träffas en specifik symbol från denna array
                    if (gameBoard[player.PosX, player.PosY] == consumables)
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
                else if (entityArray[j] is Entity goal)
                {
                    if (gameBoard[player.PosX, player.PosY] == goal)
                    {
                        GameWon(player);
                    }
                }
            }
        }
    }

    public static string PrintBagMenuAndReturnChoice(Player player)
    {
        string[] spacerArray = 
            ["        ", " ", "       ", "      ", " ",
             "      ", " ", "    ", "   ", " ", " ", " "
            ];
        // Tar in spelarens bag via GetBag, och skapar ny array av items för jämförelse längre ner.
        Dictionary<Item, int> bag = player.GetBag();
        Item[] items = new Item[bag.Count];

        // Skapar ny SelectionPrompt, med rubrikerna ovan.
        var showBag =
            new SelectionPrompt<string>()
            .Title("  Item     Wearable   Amount   Equipped")
            .PageSize(bag.Count + 3);
        
        int index = 0;

        foreach (var item in bag)
        {
            // Adderar nödvändig info till showBag. För att detta skulle funka var jag tvungen 
            // att ändra i Entity så att Name inte kan vara null.
            showBag.AddChoice
                ($"{item.Key.Name}" +
                 $"{spacerArray[item.Key.Name.Length + 4 - 2]}" +
                 $"{item.Key.Wear}" +
                 $"{spacerArray[item.Key.Wear.ToString().Length - 2]}" +
                 $"{item.Value}" +
                 $"{spacerArray[0]}" +
                 $"{item.Key.Equipped}"
                 );
            // Lägger till Item i arrayen.
            items[index] = item.Key;
            index++;
        }
        showBag.AddChoice("Close Bag");

        return AnsiConsole.Prompt(showBag);
    }
    public static void GameWon (Player player)
    {
        Console.Clear();


        //Console.WriteLine("                      /|\\                  ");
        //Console.WriteLine("                      \\|/                  ");
        //Console.WriteLine("                     '    '                 ");
        //Console.WriteLine("                   '        '               ");
        //Console.WriteLine("                '            '              ");
        //Console.WriteLine("           *  '       *       '   *         ");
        //Console.WriteLine("          /\\ '       / \\      '  / \\     ");
        //Console.WriteLine("         /  \\       /   \\       /   \\    ");
        //Console.WriteLine("        /    \\     /     \\     /     \\   ");
        //Console.WriteLine("       /      \\   /       \\   /       \\  ");
        //Console.WriteLine("                                            ");
        //Console.WriteLine("    ________________________________________");
        //Console.WriteLine("    ________________________________________");
        //Console.WriteLine("    ________________________________________");
        //Console.WriteLine("    |                                      |");
        //Console.WriteLine("    *     *     *      *     *    *    *   *");
        //Console.WriteLine("    |                                      |");
        //Console.WriteLine("    ________________________________________");

        Console.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\r\nMMMMMMMMMMMMMMMMWX0kk0XWMMMMMMMMMMMMMMMM\r\nMMMMMMMMMMMMMMMWk::cc::kWMMMMMMMMMMMMMMM\r\nMMMMMMMMMMMMMMMXo,lOOl,oXMMMMMMMMMMMMMMM\r\nMWWWMMMMMMMMMMMWO:'''':OWMMMMMMMMMMMWWWM\r\nOdookXWMMMMMMMMWO::ll::OWMMMMMMMMWXkoodO\r\n,cdl;lKMMMMMMMW0c:kNNk:c0WMMMMMMMKl;ldc,\r\n;coc'c0MMMMMMWKc;kNMMNk;lKWMMMMMM0c,co:;\r\n0d,..,o0NMMMMXl;dNMMMMNd;lXMMMMN0o,..,d0\r\nMNk;;ol:lONMXd;oNMMMMMMNo;dXMNOl:lo;;kNM\r\nMMXl;kX0o:cxo,oXMMMMMMMMXo,oxc:o0Xk;lXMM\r\nMMWx;lKMWKd:;lKMMMMMMMMMMKl;:d0WMKl;xWMM\r\nMMMKl;kMMMWXKNWMMMMMMMMMMWNKXWMMMk;lKMMM\r\nMMMWk,:dkkkkkkkkkkkkkkkkkkkkkkkkd:,kWMMM\r\nMMMMO;':llllllllllllllllllllllll:';OMMMM\r\nMMMMO:c0WWWWWWWWWWWWWWWWWWWWWWWW0c:OMMMM\r\nMMMMO;:x000000000000000000000000x:;OMMMM\r\nMMMMXdccllllllllllllllllllllllllccdXMMMM\r\nMMMMMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWMMMMM\r\nMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\r\n");

        //xd
        Console.WriteLine($"\nYou made it all the way to the goal! Congratulations!" +
                          $"\nYou got a score of {player.score}");
        Console.ReadLine();
        player.Run = false;
    }

    public static void Encounter(NPC npc, Player player)
    {
        bool run = true;

        while (run)
        {
            // Kontrollerar med hjälp av metoden Death i Operations ifall antingen spelare
            // eller NPC har dött och skickar tillbaka rätt sträng för utskrift.
            string? death = (npc.HP < 1 || player.HP < 1) ? Death(player, npc) : null;
            if (death != null)
            {
                Console.WriteLine(death);
                run = false;
                break;
            }
            else if (death == null)
            {
                // Ifall death är null så har varken NPC eller spelare dött, då fortsätter encounter.
                Console.Clear();
                Console.WriteLine(
                    $"{player.Name}:{player.HP}HP VERSUS " +
                    $"{npc.Name}:{npc.HP}HP \n");

                int choice = MenuOperations.PrintChoiceMenu("Punch", "Block", "Escape", "Use item");

                int computerChoice = new Random().Next(1, 4);
                switch (choice)
                {
                    case 1:
                        string playerResult =
                            (computerChoice == 2) ?
                            Block(npc, player) :
                            Attack(player, npc);

                        Console.WriteLine(playerResult);
                        Console.ReadLine();
                    break;

                    case 2:
                        Console.WriteLine($"{player.Name} blocks {npc.Name}'s attack!");
                        Console.ReadLine();
                    break;

                    case 3:
                        Console.WriteLine($"{player.Name} runs for his life...");
                        Console.ReadLine();
                        run = false;
                    break;

                    case 4:
                        Player.OpenBag(player);
                    break;
                }
                //NPCs tur att agera mot spelaren
                switch (computerChoice)
                {
                    case 1:
                        if (choice == 3)
                        {
                            break;
                        }
                        else
                        {
                            string computerResult = (choice == 2) ?
                            Block(player, npc) :
                            Attack(npc, player);
                            Console.WriteLine(computerResult);
                            Console.ReadLine();
                        }
                    break;

                    case 2:
                        Console.WriteLine(Block(npc, player));
                    break;

                    case 3:
                        if (npc.HP < (npc.HP * 0.1))
                        {
                            Console.WriteLine($"{npc.Name} runs for his life...");
                            player.score += 5;
                            MenuOperations.ScoreBoardReg(player);
                            run = false;
                        }
                    break;
                }
            }
        }
    }
    // Denna metod tar in gameboard samt position,
    // och returnerar ett nytt gameboard med spelarens nya position.
    public static Entity[,] ChangePosition(Entity[,] gameBoard, Player player)
    {
        gameBoard[player.PosX, player.PosY] = player;
        return gameBoard;
    }

    /* Attack metoderna tar in player stats och npc stats
     * och tar bort HP från resp karaktär.
     * Första Attack är spelarens metod och andra är Npcs metod 
     */
    public static string Attack(Player player, NPC npc)
    {
        if (player.HP > 0)
        {
            npc.HP -= player.Power;
            return $"{player.Name} attacks with {player.Power} power" +
               $"\n{player.Name}s HP: {player.HP}," +
               $" {npc.Name} HP:{npc.HP}!";
        }
        return "";
        // Metod som sköter spelarens attack.
    }
    public static string Attack(NPC npc, Player player)
    {
        if (npc.HP > 0)
        {
            player.HP -= npc.Power;
            return $"{npc.Name} attacks with {npc.Power} power" +
               $"\n{player.Name} HP: {player.HP}," +
               $" {npc.Name} HP: {npc.HP}!";
        }
        return "";
        // Metod som sköter NPCs attack, ifall npc redan dött returneras en tom sträng.
    }
    //I Block metoderna gäller samma som Attack metoder fast de har ingen påverkan
    public static string Block(NPC npc, Player player)
    {
        return $"{npc.Name} blocks the attack! " +
               $"\n{player.Name} HP: {player.HP}, " +
               $"{npc.Name} HP: {npc.HP}";
    }
    public static string Block(Player player, NPC npc)
    {
        return $"{player.Name} blocks the attack! " +
               $"\n{player.Name} HP: {player.HP}, " +
               $"{npc.Name} HP: {npc.HP}";
    }
    //Härr kotntrollerar vi om spelaren eller npc är död och avsluta deras tur och tillbaka till gameboard
    public static string Death(Player player, NPC npc)
    {
        if (player.HP < 1)
        {
            player.HP = 0;
            return $"{player.Name} died and {npc.Name} has {npc.HP} HP left.";
        }
        else if (npc.HP < 1)
        {
            npc.HP = 0;
            player.score += 10;
            MenuOperations.ScoreBoardReg(player);
            return $"{npc.Name} died and player has {player.HP} HP left.";
        }
        return "";
    }
}