using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SaleAdventure3000
{
    internal class Player : Creature
    {
        public Player(string Symbol) 
        { 
            this.Symbol = Symbol;
        }
        // När man skapar ett spelar-objekt så lägger man även till symbol.

        

        public string[,] ChangePosition(string[,] gameBoard, int posX, int posY)
        {
            gameBoard[posX, posY] = this.Symbol;
            return gameBoard;
        }
        // Denna funktion tar in gameboard samt position och returnerar ett nytt gameboard med spelarens nya position.

        public void MovePlayer(string[,] gameBoard,int firstX, int firstY)
        {
            
            this.PosX = firstX;
            this.PosY = firstY;
            gameBoard[this.PosX, this.PosY] = this.Symbol;
            // Startposition för spelaren anges när metoden anropas.
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[this.PosX, this.PosY] = " - ";
                // Ersätter spelarens gamla position med ett -
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.W)
                {
                    this.PosX--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.S)
                {
                    this.PosX++;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow ||
                    keyInfo.Key == ConsoleKey.D)
                {
                    this.PosY++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.A)
                {
                    this.PosY--;
                }
                
                gameBoard = ChangePosition(gameBoard, this.PosX, this.PosY);
               
                DrawGameBoard(gameBoard);
                // Ändrar spelarens position och ritar upp gameBoard igen.


            }
        }

    }
}
