using SaleAdventure3000;
using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using Spectre.Console;
using System.Numerics;

public class Mechanics
{
    public Mechanics () { }

    public static void ControlCollision(Entity[,] gameBoard, Player player, SaleAdventure3000.Entities.Grid grid)
    {
        //Samlas en lista av objekt 
        List<Entity[]> entities = new List<Entity[]>()
            {
                {grid.npcs },
                {grid.wears },
                {grid.consumables },
                {grid.goal}
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
        Dictionary<Item, int> bag = player.GetBag();
        Item[] items = new Item[bag.Count];
        // Array av items för jämförelse längre ner.
        var showBag =
            new SelectionPrompt<string>()
            .Title("  Item     Wearable   Amount   Equipped")
            .PageSize(bag.Count + 3);
        // Skapar ny SelectionPrompt, med rubrikerna ovan.
        int index = 0;

        foreach (var item in bag)
        {
            showBag.AddChoice
                ($"{item.Key.Name}" +
                 $"{spacerArray[item.Key.Name.Length + 4 - 2]}" +
                 $"{item.Key.Wear}" +
                 $"{spacerArray[item.Key.Wear.ToString().Length - 2]}" +
                 $"{item.Value}" +
                 $"{spacerArray[0]}" +
                 $"{item.Key.Equipped}"
                 );

            // Adderar nödvändig info till showBag. För att detta skulle funka var jag tvungen 
            // att ändra i Entity så att Name inte kan vara null.
            items[index] = item.Key;
            index++;
            // Lägger till Item i arrayen.
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
        player.Quit = false;
    }

    public static void Encounter(NPC npc, Player player)
    {
        bool run = true;

        while (run)
        {
            string? death = (npc.HP < 1 || player.HP < 1) ? Operations.Death(player, npc) : null;
            if (death != null)
            {
                Console.WriteLine(death);
                run = false;
                break;
            }
            // Kontrollerar med hjälp av metoden Death i Operations ifall antingen spelare
            // eller NPC har dött och skickar tillbaka rätt sträng för utskrift.
            else if (death == null)
            {
                Console.Clear();
                Console.WriteLine(
                    $"{player.Name}:{player.HP}HP VERSUS " +
                    $"{npc.Name}:{npc.HP}HP \n");

                //Spelaren kan välja mellan 4 olika alt
                int choice = Operations.PrintChoiceMenu("Punch", "Block", "Escape", "Use item");

                int computerChoice = new Random().Next(1, 4);
                switch (choice)
                {
                    case 1:
                        string playerResult =
                            (computerChoice == 2) ?
                            Operations.Block(npc, player) :
                            Operations.Attack(player, npc);

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
                            Operations.Block(player, npc) :
                            Operations.Attack(npc, player);
                            Console.WriteLine(computerResult);
                            Console.ReadLine();
                        }
                        break;

                    case 2:
                        Console.WriteLine(Operations.Block(npc, player));
                        break;

                    case 3:
                        if (npc.HP < (npc.HP * 0.1))
                        {
                            Console.WriteLine($"{npc.Name} runs for his life...");
                            player.score += 5;
                            Operations.ScoreBoardReg(player);
                            run = false;
                        }
                        break;
                }
            }
        }
        

    }
    public static Entity[,] ChangePosition(Entity[,] gameBoard, Player player)
    {
        gameBoard[player.PosX, player.PosY] = player;
        return gameBoard;
    }
    // Denna metod tar in gameboard samt position,
    // och returnerar ett nytt gameboard med spelarens nya position.
}