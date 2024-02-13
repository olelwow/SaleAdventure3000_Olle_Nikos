using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleAdventure3000.Entities;
using SaleAdventure3000.Items;


namespace SaleAdventure3000
{
    public class Game
    {
        public Game() { }

        //Original tanke
        //public string[,] gameBoard = new string[12, 12];
        //public static int itemsCount = new Random().Next(1, 4);
        //public static int npcCount = new Random().Next(4, 8);
        //public NPC[] npcs = new NPC[npcCount];
        //public Wearable[] wears = new Wearable[itemsCount];
        //public Consumable[] consumables = new Consumable[itemsCount];


        // Skapar random antal NPCs och föremål
        public void StartGame (string username)
        {
            Grid gameGrid = new Grid();
            //Console.WriteLine("What is your name, adventurer?");

            //string? chosenName = Console.ReadLine();


            //while (chosenName == null || chosenName.Length < 3)
            //{
            //    Console.WriteLine("Invalid name, must be at least 3 character long.");
            //    Console.WriteLine("What is your name, adventurer?");
            //    chosenName = Console.ReadLine();
            //}

            Player player = new Player(username);
            // Skapar nytt player objekt med det valda namnet, måste vara minst 3 tecken.
            int randomStartLocationX = new Random().Next(1,10);
            int randomStartLocationY = new Random().Next(1,10);
            // Ger spelaren en random startposition på gameBoard.

            gameGrid.FillGrid(gameGrid.gridBoard);
            gameGrid.DrawGridBoard(gameGrid.gridBoard);
            //FillGameBoard();
            //DrawGameBoard();
            // Fyller gameBoard, initierar random NPCs och items och ritar upp gameBoard.

            while (player.Quit)
            {
                player.MovePlayer
                    (gameGrid.gridBoard,
                     randomStartLocationX,
                     randomStartLocationY,
                     player,
                     gameGrid
                     );
            }
        }

        //Original tanke
        //private void FillGameBoard()
        //{

            //for (int i = 0; i < 12; i++)
            //{
            //    for (int j = 0; j < 12; j++)
            //    {
            //        if (i == 0 || i == 11)
            //        {
            //            this.gameBoard[i, j] = " _ ";
            //        }
            //        else if (j == 0 || j == 11)
            //        {
            //            this.gameBoard[i, j] = " | ";
            //        }
            //        else
            //        {
            //            this.gameBoard[i, j] = " - ";
            //        }
            //    }
            //}
            // Fyller gameBoard


        //    for (int i = 0; i < npcCount; i++)
        //    {   
        //        int posX = new Random().Next(1, 10);
        //        int posY = new Random().Next(1, 10);
        //        npcs[i] = new NPC();
        //        npcs[i].PosX = posX;
        //        npcs[i].PosY = posY;
        //        this.gameBoard[posX, posY] = npcs[i].Symbol;
        //    }
        //    // Initierar dessa NPCs och föremål och sätter ut dem på gameBoard.
        //    for (int i = 0; i < itemsCount; i++)
        //    {
        //        int posX = new Random().Next(1, 10);
        //        int posY = new Random().Next(1, 10);
        //        wears[i] = new Wearable();
        //        wears[i].PosX = posX;
        //        wears[i].PosY = posY;
        //        this.gameBoard[posX, posY] = wears[i].Symbol;
        //    }
        //    for (int i = 0; i < itemsCount; i++)
        //    {
        //        int posX = new Random().Next(1, 10);
        //        int posY = new Random().Next(1, 10);
        //        consumables[i] = new Consumable();
        //        consumables[i].PosX = posX;
        //        consumables[i].PosY = posY;
        //        this.gameBoard[posX, posY] = consumables[i].Symbol;
        //    }
        //}

        //OG tanke
        //private void DrawGameBoard()
        //{
        //    for (int i = 0; i < this.gameBoard.GetLength(0); i++)
        //    {
        //        Console.WriteLine("\n");
        //        for (int j = 0; j < this.gameBoard.GetLength(1); j++)
        //        {
        //            Console.Write(this.gameBoard[i, j]);
        //        }
        //    }
        //}
        // Ritar upp gameBoard för ett game objekt, private eftersom denna endast används
        // Vid start av spelet.

        //OG Tanke
        //public void DrawGameBoard(string[,] gameBoard)
        //{
        //    for (int i = 0; i < gameBoard.GetLength(0); i++)
        //    {
        //        Console.WriteLine("\n");
        //        for (int j = 0; j < gameBoard.GetLength(1); j++)
        //        {
        //            Console.Write(gameBoard[i, j]);
        //        }
        //    }
        //}
        // Ritar upp det gameBoard som man angett som parameter, public eftersom denna 
        // metod ska användas i player objektet.
    }
}
