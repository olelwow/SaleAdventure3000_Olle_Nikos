using Pastel;
using SaleAdventure3000.Items;
using Spectre.Console;

namespace SaleAdventure3000.Entities
{
    public class Grid
    {
        public Entity[,] gameBoard = new Entity[22, 22];

        

        public NPC[] npcs =
        {
            new("Nikos", 4, 1),
            new("Olle", 1, 10),
            new("Jonas", 10, 20),
            new("Ragnar", 16, 11),
            new("Nikos", 15, 20),
            new("Olle", 16, 5),
            new("Jonas", 1, 3),
            new("Ragnar", 9, 18),
            new("Nikos", 19, 18),
            new("Olle", 16, 13),
            new("Ragnar", 5, 7),
            new("Jonas", 3, 12),
        };

        public Wearable[] wears =
        {
            new("Boots", 3, 3),
            new("Hat", 3, 11),
            new("Necklace", 5, 1),
            new("Boots", 18, 12),
            new("Hat", 20, 15),
            new("Necklace", 10, 17),
        };
        public Consumable[] consumables =
        {
            new("Cheese", 6, 1),
            new("Bad-Apple", 3, 5),
            new("Pie", 12, 19),
            new("Egg", 18, 16),
            new("Cheese", 9, 1)
        };
        public Entity[] goal = 
        {
            new Entity() { Symbol = "[+]", PosX = 20, PosY = 7, SymbolColor = "#00b300" }
        };


        public Grid(){ }

        //public string empty ;
        //empty.ForeColor = System.Drawing.Color.Red;
        public void FillGrid(Entity[,] gameBoard)
        {
            
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (i == 0 || i == 21)
                    {
                        gameBoard[i, j] = new Entity() { Symbol = "===", PosX = i, PosY = j };
                    }
                    else if (j == 0 || j == 21)
                    {
                        gameBoard[i, j] = new Entity() { Symbol = " | ", PosX = i, PosY = j };
                    }
                    else
                    {
                        gameBoard[i, j] = new Entity() { Symbol = "   ", PosX = i, PosY = j };
                    }
                }
            }
            for (int i = 0; i < npcs.Length; i++)
            {
                gameBoard[npcs[i].PosX, npcs[i].PosY] = npcs[i];
            }
            //Initierar dessa NPCs och föremål och sätter ut dem på gameBoard.
            for (int i = 0; i < wears.Length; i++)
            {
                gameBoard[wears[i].PosX, wears[i].PosY] = wears[i];
            }
            for (int i = 0; i < consumables.Length; i++)
            {
                gameBoard[consumables[i].PosX, consumables[i].PosY] = consumables[i];
            }
            Dictionary<int, int[]> horizontalCoords = new Dictionary<int, int[]>()
            {
                {2, new[] {2, 4, 6, 7, 8, 10, 11, 12, 14, 15, 16, 17, 18, 19} },
                {3, new[] {2, 4, 6, 10, 14} },
                {4, new[] {2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19} },
                {5, new[] {2, 10} },
                {6, new[] {2, 4, 6, 8, 9, 10, 11, 12, 13, 14, 15, 17, 19} },
                {7, new[] {1, 2, 3, 4, 5, 6, 8, 17, 19} },
                {8, new[] {8, 10, 11, 12, 13, 14, 15, 17, 19} },
                {9, new[] {2, 4, 5, 6, 7, 8, 10, 17, 19} },
                {10, new[] {1, 2, 3, 4, 10, 12, 13, 14, 15, 16, 19} },
                {11, new[] {6, 7, 8, 9, 10, 12, 18, 19} },
                {12, new[] {2, 3, 4, 6, 12, 14, 15, 16, 17}},
                {13, new[] {2, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19} },
                {14, new[] {1, 2, 6, 14} },
                {15, new[] {1, 2, 3, 4, 6, 7, 8, 10, 11, 12, 14, 15, 16, 17, 18, 19} },
                {16, new[] {6, 10, 14} },
                {17, new[] {1, 2, 4, 6, 7, 8, 9, 10, 12,  14, 15, 16, 17, 18 } },
                {18, new[] {4, 6, 12, 14 }},
                {19, new[] {1, 2, 3, 4, 6, 7, 8, 9, 10, 12, 14, 15, 16, 17, 19} },
                {20, new[] {6, 12, 14, 19}},
                // Add more horizontal coordinates as needed
            };

            foreach (var coord in horizontalCoords)
            {
                for (int i = 0; i < coord.Value.Length; i++)
                {
                    gameBoard[coord.Key,coord.Value.ElementAt(i)] = new Obstacle("¤¤¤")
                    {
                        PosX = coord.Key,
                        PosY = coord.Value.ElementAt(i)
                    };
                }
            }
            // GOAL
            gameBoard[20, 7] = goal[0];
        }

        public void DrawGameBoard(Entity[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j].Symbol.Pastel(gameBoard[i,j].SymbolColor));
                }
            }
        }
    }
}
