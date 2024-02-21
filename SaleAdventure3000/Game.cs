using SaleAdventure3000.Entities;
using Spectre.Console;

namespace SaleAdventure3000
{
    public abstract class Game
    {
        public static void StartGame (string username)
        {
            Console.Clear();
            Entities.Grid gameGrid = new Entities.Grid();
            // Skapar nytt player objekt med det namn man loggat in med.
            Player player = new Player(username);

            // Fyller gameBoard och ritar upp gameBoard.
            gameGrid.FillGrid(gameGrid.gameBoard);
            gameGrid.DrawGameBoard(gameGrid.gameBoard, player);
            Console.WriteLine("\n");

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
