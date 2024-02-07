using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SaleAdventure3000
{
    internal class Game
    {
        public Game() { }

        public string[,] gameBoard = new string[10, 10];



        public void FillGameBoard ()
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    this.gameBoard[i, j] = " - ";
                }
            }
            
            
            int npcCount = new Random().Next(4, 8);
            NPCs[] npcs = new NPCs[npcCount];

            for (int i = 0; i < npcCount; i++)
            {   
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                npcs[i] = new NPCs();
                npcs[i].PosX = posX;
                npcs[i].PosY = posY;
                this.gameBoard[posX, posY] = npcs[i].Symbol;
            }



        }

        public void DrawGameBoard()
        {
            for (int i = 0; i < this.gameBoard.GetLength(0); i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < this.gameBoard.GetLength(1); j++)
                {
                    Console.Write(this.gameBoard[i, j]);
                }
            }
        }

        public void DrawGameBoard(string[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j]);
                }
            }
        }


    }
}
