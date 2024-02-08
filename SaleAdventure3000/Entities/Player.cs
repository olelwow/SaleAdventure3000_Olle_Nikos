using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SaleAdventure3000;

namespace SaleAdventure3000.Entities
{
    internal class Player : Creature
    {
        public Player(string Symbol)
        {
            this.Symbol = Symbol;
        }
        // När man skapar ett spelar-objekt så lägger man även till symbol.

        public bool Quit = true;

        private Dictionary<string, int> bag = new Dictionary<string, int>();
        // Spelarens väska, lagrar föremåls namn som key
        // och value indikerar ifall det är en consumable eller wearable.

        public string[,] ChangePosition(string[,] gameBoard, int posX, int posY)
        {
            gameBoard[posX, posY] = Symbol;
            return gameBoard;
        }
        // Denna funktion tar in gameboard samt position,
        // och returnerar ett nytt gameboard med spelarens nya position.

        public void MovePlayer(string[,] gameBoard, int firstX, int firstY)
        {

            PosX = firstX;
            PosY = firstY;
            gameBoard[PosX, PosY] = Symbol;
            // Startposition för spelaren anges när metoden anropas.

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[PosX, PosY] = " - ";
                // Ersätter spelarens gamla position med ett -
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    this.Quit = false;
                    break;
                }
                else if ((keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W) && PosX > 1)
                {
                    PosX--;
                }
                else if ((keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S) && PosX < 10)
                {
                    PosX++;
                }
                else if ((keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D) && PosY < 10)
                {
                    PosY++;
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A) && PosY > 1)
                {
                    PosY--;
                }

                gameBoard = ChangePosition(gameBoard, PosX, PosY);

                DrawGameBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.


            }
        }

    }
}
