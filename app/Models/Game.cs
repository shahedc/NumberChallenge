using System;

namespace NumberChallenge.Models
{
    public class Game
    {
        public string Title { get; set; }
        public int MyGuess { get; set; }
        public int SecretNumber { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
    }
}