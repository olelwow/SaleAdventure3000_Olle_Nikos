using SaleAdventure3000;

Game game = new Game();
Player player = new Player(" 0 ");
NPCs npc = new NPCs();

game.FillGameBoard();
game.DrawGameBoard();

while (true)
{
    player.MovePlayer(game.gameBoard, 5, 5);
    Console.WriteLine($" NPC{npc.Symbol}, HP{npc.HP}, Power{npc.Power}");
}


