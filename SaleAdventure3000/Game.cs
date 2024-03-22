using SaleAdventure3000.Entities;

namespace SaleAdventure3000
{
    public abstract class Game
    {
        public static void StartGame (string username)
        {
            Console.Clear();
            Grid gameGrid = new();
            Player player = new(username); // Skapar nytt player objekt med det namn man loggat in med.

            gameGrid.FillGrid(gameGrid.gameBoard); // Fyller gameBoard och ritar upp gameBoard.

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
