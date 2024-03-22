using Pastel;
using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public class Grid
    {
        public Entity[,] gameBoard = new Entity[22, 22];

        // Array som innehåller alla NPCs på kartan, samt deras positioner. Samma för Wearables samt Consumables nedan.
        public NPC[] npcs =
        [
            new("Nikos", 4, 1),
            new("Olle", 1, 10),
            new("Jonas", 10, 20),
            new("Ragnar", 16, 11),
            new("Nikos", 15, 20),
            new("Olle", 16, 5),
            new("Ragnar", 9, 18),
            new("Nikos", 19, 18),
            new("Olle", 16, 13),
            new("Ragnar", 5, 7),
            new("Jonas", 3, 12),
            new("Olle", 8, 9),
            new("Ragnar", 20, 5),
            new("Nikos", 6, 5),
            new("Tintin", 20, 8), 
        ];

        public Wearable[] wears =
        [
            new("Body", "Leather", 3, 7),
            new("Helmet", "Cloth", 3, 11),
            new("Legs", "Leather", 5, 1),
            new("Body", "Cloth", 18, 13),
            new("Helmet", "Leather", 20, 15),
            new("Legs", "Plate", 10, 17),
            new("Helmet", "Plate", 13, 1),
            new("Body", "Plate", 14, 7),
            new("Legs", "Cloth", 20, 1)
        ];
        public Consumable[] consumables =
        [
            new("Cheese", 6, 1),
            new("Bad-Apple", 3, 5),
            new("Pie", 12, 19),
            new("Egg", 18, 16),
            new("Cheese", 9, 1),
            new("Pie", 9, 9),
            new("Egg", 17, 3),
            new("Cheese", 12, 10),
            new("Pie", 18, 7),
            new("Egg", 1, 14),
        ];
        public Entity[] goal = // "Målet" som gör att man vinner spelet.
            [new Obstacle("[+]") { PosX = 20, PosY = 7, SymbolColor = "#000000" } ];
        
        // Koordinater för "labyrinten", key representerar rad, values representerar kolumn.
        public readonly Dictionary<int, int[]> obstacleCoordinates = new()
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
            {20, new[] {6, 12, 14, 19}}
        };

        public Grid(){ }

        public void FillGrid(Entity[,] gameBoard) // Fyller gameboard
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (i == 0 || i == 21)
                    {
                        gameBoard[i, j] = new Obstacle("===") {PosX = i, PosY = j };
                    }
                    else if (j == 0 || j == 21)
                    {
                        gameBoard[i, j] = new Obstacle(" | ") { PosX = i, PosY = j };
                    }
                    else
                    {
                        gameBoard[i, j] = new Obstacle("   ") { PosX = i, PosY = j };
                    }
                }
            }
            // Går igenom arrays av npcs, wearables samt consumables och sätter ut objekten på kartan.
            for (int i = 0; i < npcs.Length; i++)
            {
                gameBoard[npcs[i].PosX, npcs[i].PosY] = npcs[i];
            }
            for (int i = 0; i < wears.Length; i++)
            {
                gameBoard[wears[i].PosX, wears[i].PosY] = wears[i];
            }
            for (int i = 0; i < consumables.Length; i++)
            {
                gameBoard[consumables[i].PosX, consumables[i].PosY] = consumables[i];
            }
            
            // Loopar igenom Dictionary med koordinater och placerar ut Obstacles enligt dessa koordinater.
            foreach (var coord in obstacleCoordinates)
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
            gameBoard[20, 7] = goal[0]; // GOAL
        }
        // Metod som ritar upp det GameBoard man skickat in som parameter.
        public static void DrawGameBoard(Entity[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j].Symbol
                                 .Pastel(gameBoard[i,j].SymbolColor)
                                 .PastelBg(gameBoard[i, j].BackgroundColor));
                }
            }
        }
    }
}
