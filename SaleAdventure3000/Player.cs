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

        public string[,] ChangePosition(string[,] gameBoard, int posX, int posY)
        {
            gameBoard[posX, posY] = Symbol;
            return gameBoard;
        }

        public void MovePlayer(string[,] gameBoard,int firstX, int firstY)
        {
            
            this.PosX = firstX;
            this.PosY = firstY;
            gameBoard[this.PosX, this.PosY] = this.Symbol;
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();
                gameBoard[this.PosX, this.PosY] = " - ";
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


            }
        }

    }
}
