using System;

namespace NumberChallenge.Models
{
    public class Guess
    {
        public int GuessValue { get; set; }
        public int Comp { get; set; } // -1, 0, 1 (low, equal, high)
    }
}