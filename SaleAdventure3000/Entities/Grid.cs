using SaleAdventure3000.Items;

namespace SaleAdventure3000.Entities
{
    public class Grid
    {
        public Entity[,] gameBoard = new Entity[12, 12];
        //public static string horizontalLine = "XXX";
        Obstacle obstacle = new Obstacle("XXX");
        //public static string verticalLine = " X ";
        public static int itemsCount = new Random().Next(2, 4);
        public static int npcCount = new Random().Next(4, 8);
        public NPC[] npcs = new NPC[npcCount];
        public Wearable[] wears = new Wearable[itemsCount];
        public Consumable[] consumables = new Consumable[itemsCount];

        public Grid(){ }

        public void FillGrid(Entity[,] gameBoard)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i == 0 || i == 11)
                    {
                        gameBoard[i, j] = new Entity() { Symbol = "===", PosX = i, PosY = j};
                    }
                    else if (j == 0 || j == 11)
                    {
                        gameBoard[i, j] = new Entity() { Symbol = " | ", PosX = i, PosY = j };
                    }
                    else
                    {
                        gameBoard[i, j] = new Entity() { Symbol = " - ", PosX = i, PosY = j };
                    }
                }
            }
            for (int i = 0; i < npcCount; i++)
            {
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                npcs[i] = new NPC();
                npcs[i].PosX = posX;
                npcs[i].PosY = posY;
                gameBoard[posX, posY] = npcs[i];
            }
            // Initierar dessa NPCs och föremål och sätter ut dem på gameBoard.
            for (int i = 0; i < itemsCount; i++)
            {
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                wears[i] = new Wearable();
                wears[i].PosX = posX;
                wears[i].PosY = posY;
                gameBoard[posX, posY] = wears[i];
            }
            for (int i = 0; i < itemsCount; i++)
            {
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                consumables[i] = new Consumable();
                consumables[i].PosX = posX;
                consumables[i].PosY = posY;
                gameBoard[posX, posY] = consumables[i];
            }


           
            for (int j = 0; j < 16; j++)
            {
                int posX = new Random().Next(1, 10);
                int posY = new Random().Next(1, 10);
                gameBoard[posX, posY] = new Obstacle(obstacle.Symbol)
                {
                    PosX = posX,
                    PosY = posY
                };
            }
            
                

            
            /* ---------------------
             * hej
             * __________________
             *                   |
             *                   |
             *         |         |
             *         |_________________         
             *        " ___ "   |        |   
             *                           |
             *                           |
             *           __  |        
             *                   
             *                   
             */
        }

        public void DrawGameBoard(Entity[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                
                Console.WriteLine("\n");
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j].Symbol);
                }
            }
        }

    }
}
