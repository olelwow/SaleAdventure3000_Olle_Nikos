using Pastel;

namespace SaleAdventure3000
{
    public class Dice
    {
        // Properties för värden ihopkopplade med tärningskastet, samt utskrifter för respektive värde.
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

        // Rullar tärningen och returnerar resultatet från Dictionary ovan som stämmer överens med resultat för tärningskastet.
        public double Roll(int range)
        {
            int result = this.Die.Next(1, range);
            Console.WriteLine(messages[result].Pastel(colors[result]));
            return Values[result];
        }
    }
}