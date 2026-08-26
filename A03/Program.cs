// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to find valid words from a given set of letters and
// calculate their scores based on specific rules.
// ------------------------------------------------------------------------------------------------
using static System.Console;

class Program {
   static void Main () {
      const int PANGRAMBONUS = 7;
      const int MINLENGTH = 4;
      char[] letters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
      Dictionary<string, (int Score, bool IsPangram)> result = [];
      foreach (string word in File.ReadLines ("words.txt"))
         if (IsValid (word)) result[word] = (GetScoreandPangramStatus (word));
      foreach (var ans in result.OrderByDescending (x => x.Value).ThenBy (x => x.Key)) {
         if (ans.Value.IsPangram) ForegroundColor = ConsoleColor.Green;
         else ResetColor ();
         WriteLine ($"{ans.Value.Score, 3}: {ans.Key}");
      }
      WriteLine ("----");
      WriteLine ($"{result.Sum (x => x.Value.Score), 3}: Total");

      // Checks validity of the word
      bool IsValid (string word)
         => word.Length >= MINLENGTH && word.Contains (letters[0]) && word.All (letters.Contains);

      // Calculates the score of the word
      (int, bool) GetScoreandPangramStatus (string word) {
         bool isPangram = letters.All (word.Contains);
         int score = word.Length == MINLENGTH ? 1 : isPangram ? word.Length + PANGRAMBONUS
                                                              : word.Length;
         return (score, isPangram);
      }
   }
}
