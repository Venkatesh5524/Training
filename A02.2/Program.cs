// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A02.2: Program to guess a number thought of by the user between 1 and 100 by determining it's
// binary digits from least significant bit to most significant bit.
// ------------------------------------------------------------------------------------------------

using static System.Console;

#region Program -----------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   static void Main () {
      int num = MAXVALUE;
      WriteLine ($"Think of a number between 1 and {num}");
      int rem = 0;
      for (int divisor = 1; divisor <= num; divisor *= 2) {
         Display ($"The number when divided by {divisor * 2,3} is remainder {rem,3} " +
                  $"(Y)es or (N)o: ", ConsoleColor.Yellow);
         ConsoleKey response;
         do response = ReadKey (true).Key;
         while (response is not (ConsoleKey.Y or ConsoleKey.N));
         WriteLine (response);
         if (response == ConsoleKey.N) rem += divisor;
      }
      Display ($"I guessed it, your number is {rem}", ConsoleColor.Green);
   }

   // Displays the message in the specified color.
   static void Display (string message, ConsoleColor color) {
      ForegroundColor = color;
      Write (message);
      ResetColor ();
   }
   #endregion

   #region const ----------------------------------------------------
   const int MAXVALUE = 100;
   #endregion
}
#endregion