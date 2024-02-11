﻿using SaleAdventure3000;
using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using System.Runtime.CompilerServices;

internal class Program
{
    
    private static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            Operations.Logo();
            Console.WriteLine("- - - Menu - - -\n" +
                "1. Register\n" +
                "2. Log in\n" +
                "3. Scoreboard\n" +
                "4. Quit");
            Console.Write(">");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Operations.RegisterPlayer();
                    break;
                case 2:
                    Operations.Login();
                break;
                case 3:
                break;
                case 4:
                run = false;
                break;
                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }

            //Game game = new Game();
            //game.StartGame();

        }

    }

    
}


//Console.WriteLine("What is your name?");
//string name = Console.ReadLine();
//Player player = new Player(" 0 ", name);


//Consumable consumable = new Consumable();
//Wearable item = new Wearable();
//game.FillGameBoard();
//game.DrawGameBoard();


//while (player.Quit)
//{
//    player.MovePlayer(game.gameBoard, 5, 5, player, game);
//    //Console.WriteLine($" NPC{consumable.Symbol}, HP{consumable.Wear}, Power{consumable.Name}");
//}


