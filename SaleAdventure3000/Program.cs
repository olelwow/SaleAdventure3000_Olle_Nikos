using SaleAdventure3000;

Game game = new Game();
Player player = new Player(" 0 ");

game.FillGameBoard();
game.DrawGameBoard();



while (true)
{
    player.MovePlayer(game.gameBoard, 5, 5);
}

