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
                    if (gameBoard[player.PosXGetSet, player.PosYGetSet] == consumables)
                    {
                        consumables.OnPickup(consumables, player);
                        break;
                    }
                }
                else if (entityArray[j] is Wearable wears)
                {
                    if (gameBoard[player.PosXGetSet, player.PosYGetSet] == wears)
                    {
                        wears.OnPickup(wears, player);
                        break;
                    }
                }
                else if (entityArray[j] is NPC npcs)
                {
                    if (gameBoard[player.PosXGetSet, player.PosYGetSet] == npcs)
                    {
                        Encounter(npcs, player);
                        break;
                    }
                }
                else if (entityArray[j] is Entity goal)
                {
                    if (gameBoard[player.PosXGetSet, player.PosYGetSet] == goal)
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
                ($"{item.Key.NameGetSet}" +
                 $"{spacerArray[item.Key.NameGetSet.Length + 4 - 2]}" +
                 $"{item.Key.WearGetSet}" +
                 $"{spacerArray[item.Key.WearGetSet.ToString().Length - 2]}" +
                 $"{item.Value}" +
                 $"{spacerArray[0]}" +
                 $"{item.Key.EquippedGetSet}"
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
            string? death = (npc.HPGetSet < 1 || player.HPGetSet < 1) ? Death(player, npc) : null;
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
                    $"{player.NameGetSet}:{player.HPGetSet}HP VERSUS " +
                    $"{npc.NameGetSet}:{npc.HPGetSet}HP \n");

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
                        Console.WriteLine($"{player.NameGetSet} blocks {npc.NameGetSet}'s attack!");
                        Console.ReadLine();
                    break;

                    case 3:
                        Console.WriteLine($"{player.NameGetSet} runs for his life...");
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
                        if (npc.HPGetSet < (npc.HPGetSet * 0.1))
                        {
                            Console.WriteLine($"{npc.NameGetSet} runs for his life...");
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
        gameBoard[player.PosXGetSet, player.PosYGetSet] = player;
        return gameBoard;
    }

    /* Attack metoderna tar in player stats och npc stats
     * och tar bort HP från resp karaktär.
     * Första Attack är spelarens metod och andra är Npcs metod 
     */
    public static string Attack(Player player, NPC npc)
    {
        if (player.HPGetSet > 0)
        {
            npc.HPGetSet -= player.PowerGetSet;
            return $"{player.NameGetSet} attacks with {player.PowerGetSet} power" +
               $"\n{player.NameGetSet}s HP: {player.HPGetSet}," +
               $" {npc.NameGetSet} HP:{npc.HPGetSet}!";
        }
        return "";
    }
    public static string Attack(NPC npc, Player player)
    {
        // Metod som sköter NPCs attack, ifall npc redan dött returneras en tom sträng.
        if (npc.HPGetSet > 0)
        {
            player.HPGetSet -= npc.PowerGetSet;
            return $"{npc.NameGetSet} attacks with {npc.PowerGetSet} power" +
               $"\n{player.NameGetSet} HP: {player.HPGetSet}," +
               $" {npc.NameGetSet} HP: {npc.HPGetSet}!";
        }
        return "";
    }
    //I Block metoderna gäller samma som Attack metoder fast de har ingen påverkan
    public static string Block(NPC npc, Player player)
    {
        return $"{npc.NameGetSet} blocks the attack! " +
               $"\n{player.NameGetSet} HP: {player.HPGetSet}, " +
               $"{npc.NameGetSet} HP: {npc.HPGetSet}";
    }
    public static string Block(Player player, NPC npc)
    {
        return $"{player.NameGetSet} blocks the attack! " +
               $"\n{player.NameGetSet} HP: {player.HPGetSet}, " +
               $"{npc.NameGetSet} HP: {npc.HPGetSet}";
    }
    //Härr kotntrollerar vi om spelaren eller npc är död och avsluta deras tur och tillbaka till gameboard
    public static string Death(Player player, NPC npc)
    {
        if (player.HPGetSet < 1)
        {
            player.HPGetSet = 0;
            return $"{player.NameGetSet} died and {npc.NameGetSet} has {npc.HPGetSet} HP left.";
        }
        else if (npc.HPGetSet < 1)
        {
            npc.HPGetSet = 0;
            player.score += 10;
            MenuOperations.ScoreBoardReg(player);
            return $"{npc.NameGetSet} died and player has {player.HPGetSet} HP left.";
        }
        return "";
    }


}