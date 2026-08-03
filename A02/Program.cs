// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// -------------------------------------------------------------------------------------------------
// Program.cs
// Program to generate a random number between 1 and 100 and let the user guess it.
namespace A02;

class Program {
   static void Main (string[] args) {
      int num = new Random ().Next (1, 101), res = 0, count = 0;
      do {
         count++;
         Console.Write ("Enter your guess(1-100): ");
         res = ReadGuess ();
         if (res == num) break;
         Console.WriteLine ($"Your Guess is too {(res < num ? "Low" : "High")}");
      } while (res != num);
      Console.WriteLine ($"You Guessed Correctly in {count} tries.");

      int ReadGuess () {
         for (; ; ) {
            if (int.TryParse (Console.ReadLine (), out int result) && result >= 1 && result <= 100)
               return result;
            Console.Write ("Enter a valid Number: ");
         }
      }
   }
}
