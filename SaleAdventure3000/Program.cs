using SaleAdventure3000;
using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;
using System.Runtime.CompilerServices;

Game game = new Game();
Player player = new Player(" 0 ");

//Consumable consumable = new Consumable();
//Wearable item = new Wearable();
game.FillGameBoard();
game.DrawGameBoard();


while (player.Quit)
{
    player.MovePlayer(game.gameBoard, 5, 5, player, game);
    //Console.WriteLine($" NPC{consumable.Symbol}, HP{consumable.Wear}, Power{consumable.Name}");
}


