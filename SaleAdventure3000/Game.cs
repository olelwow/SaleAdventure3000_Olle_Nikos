using SaleAdventure3000.Entities;

namespace SaleAdventure3000
{
    public class Game
    {
        public Game() { }

        // Skapar random antal NPCs och föremål
        public void StartGame (string username)
        {
            Console.Clear();
            Grid gameGrid = new Grid();

            Player player = new Player(username);
            // Skapar nytt player objekt med det valda namnet, måste vara minst 3 tecken.
            //int randomStartLocationX = new Random().Next(1,10);
            //int randomStartLocationY = new Random().Next(1,10);
            // Ger spelaren en random startposition på gameBoard.

            gameGrid.FillGrid(gameGrid.gameBoard);
            gameGrid.DrawGameBoard(gameGrid.gameBoard);
            // Fyller gameBoard, initierar random NPCs och items och ritar upp gameBoard.

            while (player.Quit)
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
