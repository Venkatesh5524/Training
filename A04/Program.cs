// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// -------------------------------------------------------------------------------------------------
// Program.cs
// Program to find the occurence of all letters in word.txt and display the top 7 letters with their occurences.
namespace A04;

class Program {
   static void Main () {
      string[] words = File.ReadAllLines ("words.txt");
      Dictionary<char, int> freq = new ();
      foreach (string word in words) {
         foreach (char c in word) {
            if (c >= 'A' && c <= 'Z') {
               if (!freq.ContainsKey (c)) {
                  freq[c] = 0;
               }
               freq[c]++;
            }
         }
      }
      int tries = 0;
      foreach (var ch in freq.OrderByDescending (a => a.Value)) {
         if (tries == 7) break;
         Console.WriteLine ($"{ch.Key} : {ch.Value}");
         tries++;
      }
   }
}
