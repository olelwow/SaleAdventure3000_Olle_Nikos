using SaleAdventure3000;
using SaleAdventure3000.Entities;

Game game = new Game();
Player player = new Player(" 0 ");

game.FillGameBoard();
game.DrawGameBoard();



while (true)
{
    player.MovePlayer(game.gameBoard, 5, 5);
    //Console.WriteLine($" NPC{npc.Symbol}, HP{npc.HP}, Power{npc.Power}");
}


