using Pastel;

namespace SaleAdventure3000
{
    public class Dice
    {
        private Random Die = new Random();

        public Dictionary<int, double> powerLuck = new()
        {
            {1, 0.00 },
            {2, 0.20 },
            {3, 0.45 },
            {4, 1.00 },
            {5, 1.50 },
            {6, 2.00 },
        };
        public double Roll(int range)
        {
            string message;
            int result = this.Die.Next(1, range);
            if (result == 1)
            {
                message = $"The dice stayed on {result}.\n " +
                    "No effects on the attack XD";
                Console.WriteLine(message.Pastel(ConsoleColor.Red));
            }
            else if (result < 4 && result > 1)
            {
                message = $"The dice stayed on {result}.\n " +
                    $"The attack is {100 * powerLuck[result]} % weaker :(";
                Console.WriteLine(message.Pastel(ConsoleColor.Yellow));
            }
            else if (result == 4)
            {
                message = $"The dice stayed on {result}.\n " +
                    $"The attack is normal...";
                Console.WriteLine(message.Pastel(ConsoleColor.DarkGreen));
            }
            else if (result > 4)
            {
                message = $"The dice stayed on {result}.\n " +
                    $"The attack does {100 * (int)powerLuck[result]} % critical damage. WOW!!!";
                Console.WriteLine(message.Pastel(ConsoleColor.Green));
            }
            return powerLuck[result];
        }
    }
}