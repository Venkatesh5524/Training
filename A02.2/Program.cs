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
      WriteLine ($"Think of a number between {MINVALUE} and {MAXVALUE}");
      int guess = 0;
      for (int divisor = 1; divisor <= MAXVALUE; divisor *= 2) {
         Display ($"The number when divided by {divisor * 2,3} is remainder {guess,3} " +
                  $"(Y)es or (N)o: ", ConsoleColor.Yellow);
         ConsoleKey response;
         while ((response = ReadKey (true).Key) is not (ConsoleKey.Y or ConsoleKey.N)) { }
         WriteLine (response);
         if (response == ConsoleKey.N) guess += divisor;
      }
      if (guess < MINVALUE || guess > MAXVALUE)
         Display ("Hints are inconsistent. Please try again.", ConsoleColor.Red);
      else Display ($"I guessed it, your number is {guess}", ConsoleColor.Green);
   }

   // Displays the message in the specified color.
   static void Display (string message, ConsoleColor color) {
      ForegroundColor = color;
      Write (message);
      ResetColor ();
   }
   #endregion

   #region const ----------------------------------------------------
   const int MINVALUE = 1;
   const int MAXVALUE = 100;
   #endregion
}
#endregion