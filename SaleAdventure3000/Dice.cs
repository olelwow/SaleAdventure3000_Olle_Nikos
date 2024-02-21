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
            {4, 0.95 },
            {5, 1.50 },
            {6, 2.00 },
        };
        public double Roll(int range)
        {
            int result = this.Die.Next(1, range);
            if (result == 1)
            {
                Console.WriteLine("No effects on the attack");
            }
            else if (result < 6 && result > 1)
            {
                Console.WriteLine($"The attack is {100 * powerLuck[result]} % stronger");
            }
            else if (result == 6)
            {
                Console.WriteLine($"The attack does {100 * (int)powerLuck[result]} % critical damage. WOW!!!");
            }
            return powerLuck[result];
        }
    }
}