// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// -------------------------------------------------------------------------------------------------
// Program.cs
// Program to find the occurence of all letters in word.txt and display the top 7 letters with their occurences.

using static System.Console;
class Program {
   static void Main () {
      Dictionary<char, int> freq = [];
      foreach (string word in File.ReadAllLines ("words.txt")) {
         foreach (char c in word) {
            if (c >= 'A' && c <= 'Z') {
               if (freq.TryGetValue (c, out int value)) freq[c]++;
               else freq[c] = 0;
            }
         }
      }
      WriteLine ("Seven most frequently occuring letters and their occurences");
      WriteLine ("Letter: Occurences");
      foreach (var ch in freq.OrderByDescending (a => a.Value).Take (7))
         Console.WriteLine ($"{ch.Key, 3} {":", 3} {ch.Value} occurences");
   }
}
