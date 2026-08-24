// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A06: Program to solve the N-Queens problem and display all possible and unique solutions.
// ------------------------------------------------------------------------------------------------

using System.Text;
using static System.Console;

#region Program -----------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   static void Main () {
      OutputEncoding = new UnicodeEncoding ();
      Write ("Enter number of queens: ");
      int n = GetBoardSize ();
      List<int[]> allSolutions = [];
      List<int[]> uniqueSolutions = [];
      int[] rows = new int[n];
      Write ("Processing...  ");
      FindSolutions (0);
      foreach (int[] solution in allSolutions) {
         if (IsUnique (solution)) {
            uniqueSolutions.Add (solution);
         }
      }
      SetCursorPosition (0, 1);
      WriteLine ($"Total possible solutions for {n} queens: {allSolutions.Count}");
      WriteLine ($"Total unique solutions for {n} queens: {uniqueSolutions.Count}");
      Write ("Press (P) to see all possible solutions or (U) to see only unique solutions: ");
      ConsoleKey response;
      while ((response = ReadKey (true).Key) is not ConsoleKey.P and not ConsoleKey.U) { }
      WriteLine (response);
      DisplaySolution (response == ConsoleKey.P ? allSolutions : uniqueSolutions);

      // Finds all possible N-Queens solutions
      void FindSolutions (int row) {
         if (row == n) {
            allSolutions.Add ([.. rows]);
            return;
         }
         for (int col = 0; col < n; col++) {
            if (IsSafe (row, col)) {
               rows[row] = col;
               FindSolutions (row + 1);
            }
         }

         // Checks whether a queen can be safely placed at the given position.
         bool IsSafe (int row, int col) {
            for (int i = 0; i < row; i++)
               if (rows[i] == col || Math.Abs (col - rows[i]) == row - i) return false;
            return true;
         }
      }

      // Checks whether the solution is unique among rotations and reflections.
      bool IsUnique (int[] solution) {
         for (int i = 0; i < 4; i++) {
            if (SolutionExists (solution) || SolutionExists (Mirror (solution))) return false;
            solution = Rotate (solution);
         }
         return true;
      }

      // Rotates the solution by 90 degrees.
      int[] Rotate (int[] solution) {
         int[] rotated = new int[n];
         for (int row = 0; row < n; row++) rotated[solution[row]] = n - 1 - row;
         return rotated;
      }

      // Creates a mirrored version of the given solution.
      int[] Mirror (int[] solution) {
         int[] mirror = new int[n];
         for (int row = 0; row < n; row++) mirror[row] = n - 1 - solution[row];
         return mirror;
      }

      // Checks whether the given solution already exists.
      bool SolutionExists (int[] solution)
         => uniqueSolutions.Any (x => x.SequenceEqual (solution));

      // Displays the solutions and allows navigation between them.
      void DisplaySolution (List<int[]> solutions) {
         int currSoln = 0;
         Clear ();
         while (currSoln < solutions.Count) {
            SetCursorPosition (0, 0);
            WriteLine ($"\nSolution {currSoln + 1} of {solutions.Count} ");
            DisplayBoard (solutions[currSoln]);
            Write ("\nPress \u2192 to see the next solution... " +
               "\nPress \u2190 to see the previous solution... \nPress esc to exit... ");
            ConsoleKey key;
            while ((key = ReadKey (true).Key) is not (ConsoleKey.RightArrow or ConsoleKey.LeftArrow
                                                                            or ConsoleKey.Escape));
            if (key == ConsoleKey.Escape) break;
            currSoln = key == ConsoleKey.RightArrow ? currSoln < solutions.Count - 1
                                                    ? currSoln + 1 : currSoln : currSoln > 0
                                                    ? currSoln - 1 : 0;
            Clear ();
         }
      }

      // Displays the board for the given solution.
      void DisplayBoard (int[] solution) {
         int num = solution.Length;
         WriteLine ("┌" + string.Join ("", Enumerable.Repeat ("────┬", num - 1)) + "────┐");
         for (int i = 0; i < num; i++) {
            WriteLine ("│" + string.Join ("", Enumerable.Range (1, num).
                                                        Select (j => solution[i] == j - 1 ? " ♕  │" : "    │")));
            if (i < num - 1)
               WriteLine ("├" + string.Join ("", Enumerable.Repeat ("────┼", num - 1)) + "────┤");
         }
         WriteLine ("└" + string.Join ("", Enumerable.Repeat ("────┴", num - 1)) + "────┘");
      }

      // Reads and validates the number of queens entered by the user.
      int GetBoardSize () {
         for (; ; )
         {
            if (int.TryParse (ReadLine (), out int n) && n > 0) return n;
            Write ("Enter a positive number: ");
         }
      }
   }
   #endregion
}
#endregion