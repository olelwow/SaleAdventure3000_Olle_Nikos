using SaleAdventure3000.Entities;

namespace SaleAdventure3000
{
    public abstract class Game
    {
        public static void StartGame (string username)
        {
            Console.Clear();
            Grid gameGrid = new Grid();
            // Skapar nytt player objekt med det namn man loggat in med.
            Player player = new Player(username);

            // Fyller gameBoard och ritar upp gameBoard.
            gameGrid.FillGrid(gameGrid.gameBoard);
            gameGrid.DrawGameBoard(gameGrid.gameBoard, 1, 1);
            
            // Loop som körs tills man trycker på q vilket ändrar variabeln Run till false.
            while (player.Run)
            {
                player.MovePlayer
                    (gameGrid.gameBoard,
                     1,
                     1,
                     player,
                     gameGrid
                     );
            }
        }
    }
}
