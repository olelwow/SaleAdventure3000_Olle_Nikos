using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleAdventure3000.Entities;


namespace SaleAdventure3000
{
    internal class Game
    {
        public Game() { }

        public string[,] gameBoard = new string[12, 12];



        public void FillGameBoard ()
        {

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i == 0 || i == 11)
                    {
                        this.gameBoard[i, j] = " _ ";
                    }
                    else if (j == 0 || j == 11)
                    {
                        this.gameBoard[i, j] = " | ";
                    }
                    else
                    {
                        this.gameBoard[i, j] = " - ";
                    }
                }
            }
            // Fyller gameBoard
            
            int npcCount = new Random().Next(4, 8);
            NPC[] npcs = new NPC[npcCount];
            // Skapar random antal NPCs

            for (int i = 0; i < npcCount; i++)
            {   
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                npcs[i] = new NPC();
                npcs[i].PosX = posX;
                npcs[i].PosY = posY;
                this.gameBoard[posX, posY] = npcs[i].Symbol;
            }
            // Initierar dessa NPCs och sätter ut dem på gameBoard.
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
        // Ritar upp gameBoard för ett game objekt.

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
        // Ritar upp det gameBoard som man angett som parameter.
    }
}
