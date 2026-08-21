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
      char[] letters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
      Dictionary<string, int> result = [];
      foreach (string word in File.ReadAllLines ("words.txt"))
         if (IsValid (word) == true)
            result[word] = GetScore (word);
      int total = 0;
      foreach (var ans in result.OrderByDescending (x => x.Value).ThenBy (x => x.Key)) {
         if (IsPanagram (ans.Key) == true) ForegroundColor = ConsoleColor.Green;
         else ResetColor ();
         total += ans.Value;
         WriteLine ($"{ans.Value, 3}: {ans.Key}");
      }
      WriteLine ("----");
      WriteLine ($"{total, 3}: Total");

      // Checks validity of the word
      bool IsValid (string word)
         => word.Length > 3 && word.Contains (letters[0]) && word.All (letters.Contains);

      // Calculates the score of the word
      int GetScore (string word)
         => word.Length == 4 ? 1 : IsPanagram (word) ? word.Length + 7 : word.Length;

      // Checks if the word is a panagram
      bool IsPanagram (string word) => letters.All (word.Contains);
   }
}
