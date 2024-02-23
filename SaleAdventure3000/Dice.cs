using Pastel;

namespace SaleAdventure3000
{
    public class Dice
    {
        private readonly Random Die = new();
        private readonly Dictionary<int, double> Values = new()
        {
            {1, 0.00 },
            {2, 0.20 },
            {3, 0.45 },
            {4, 1.00 },
            {5, 1.50 },
            {6, 2.00 },
        };
        private readonly string[] messages = 
            ["", "The dice rolled a 1. \nThe attack had no effect XD",
             "The dice rolled a 2. \nThe attack only has 20% of its original power. :(",
             "The dice rolled a 3. \nThe attack only has 45% of its original power. :()",
             "The dice rolled a 4. \nThe attack is normal....",
             "The dice rolled a 5. \nThe attack does 150% critical damage!! WOW!",
             "The dice rolled a 6. \nThe attack does 200% critical damage!! WAOOOWWW!"];
        private readonly string[] colors = ["", "#9e1519", "#a7b811", "#a7b811", "#1ac4c9", "#1a8cc9", "#1fbd1c"];

        public double Roll(int range)
        {
            int result = this.Die.Next(1, range);
            //string message;
            //if (result == 1)
            //{
            //    message = $"The dice stayed on {result}.\n " +
            //        "No effects on the attack XD";
            //    Console.WriteLine(message.Pastel(ConsoleColor.Red));
            //}
            //else if (result < 4 && result > 1)
            //{
            //    message = $"The dice stayed on {result}.\n " +
            //        $"The attack is {100 * Values[result]} % weaker :(";
            //    Console.WriteLine(message.Pastel(ConsoleColor.Yellow));
            //}
            //else if (result == 4)
            //{
            //    message = $"The dice stayed on {result}.\n " +
            //        $"The attack is normal...";
            //    Console.WriteLine(message.Pastel(ConsoleColor.DarkGreen));
            //}
            //else if (result > 4)
            //{
            //    message = $"The dice stayed on {result}.\n " +
            //        $"The attack does {100 * (int)Values[result]} % critical damage. WOW!!!";
            //    Console.WriteLine(message.Pastel(ConsoleColor.Green));
            //}
            // Sorry Nikos <3
            Console.WriteLine(messages[result].Pastel(colors[result]));
            return Values[result];
        }
    }
}