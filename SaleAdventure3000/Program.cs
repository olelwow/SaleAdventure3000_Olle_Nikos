using SaleAdventure3000;
using SaleAdventure3000.Entities;
using System.Runtime.CompilerServices;

Game game = new Game();
Player player = new Player(" 0 ");

game.FillGameBoard();
game.DrawGameBoard();


while (player.Quit)
{
    player.MovePlayer(game.gameBoard, 5, 5);
    //Console.WriteLine($" NPC{npc.Symbol}, HP{npc.HP}, Power{npc.Power}");
}


