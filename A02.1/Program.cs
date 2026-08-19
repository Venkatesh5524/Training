// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess a number thought of by the user between 1 and 100 using binary search.
// ------------------------------------------------------------------------------------------------
using static System.Console;

class Program {
   static void Main () {
      int low = 1, high = 100, guess;
      Write ($"Think of a number between {low} and {high}: " +
             "\nH: If the number is greater than guess\nL: If the number is less than guess" +
             "\nY: If guessed correctly ");
      ConsoleKey response;
      while (low <= high) {
         guess = (low + high) / 2;
         Write ($"\nIs your number {guess,3} (Y)es (H)igh (L)ow: ");
         response = VaildGuess ();
         switch (response) {
            case ConsoleKey.Y:
               WriteLine ($"\nI guessed it, your number is {guess}.");
               return;
            case ConsoleKey.H:
               low = guess + 1; break;
            default:
               high = guess - 1; break;
         }
      }
      WriteLine ("\nHints are inconsistent");

      // Reads the user's input and returns a valid guess: Y (Yes), L (Low), or H (High).
      static ConsoleKey VaildGuess () {
         ConsoleKey key = 0;
         while (!(key is ConsoleKey.Y or ConsoleKey.L or ConsoleKey.H)) key = ReadKey(true).Key ;
         Write (key);
         return key;
      }
   }
}
