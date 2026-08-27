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
      Write ("Enter number of queens: ");
      int n = GetBoardSize ();
      List<int[]> solutions = [];
      int[] rows = new int[n];
      Write ("Press (P) to see all possible solutions or (U) to see only unique solutions: ");
      ConsoleKey response;
      while ((response = ReadKey (true).Key) is not ConsoleKey.P and not ConsoleKey.U) { }
      WriteLine (response);
      bool showUniqueSolution = response == ConsoleKey.U;
      Write ("Processing...  ");
      FindSolutions (0);
      Clear ();
      if (solutions.Count == 0) {
         WriteLine ($"No solution exists for {n} Queens.");
         Write ("Press any key to exit... ");
         ReadKey (true);
         return;
      }
      WriteLine ($"Total {(showUniqueSolution ? "Unique" : "Possible")} solutions for {n} Queens: {solutions.Count}");
      Write ("Press any key to print the solution");
      ReadKey ();
      DisplaySolution ();

      // Finds all possible N-Queens solutions
      void FindSolutions (int row) {
         for (int col = 0; col < n; col++)
            if (IsSafe (row, col)) {
               rows[row] = col;
               if (row == n - 1) AddSolution ([.. rows]);
               else FindSolutions (row + 1);
            }

         // Adds the solution directly or only if it is unique, based on the selected mode.
         void AddSolution (int[] solution) {
            if (showUniqueSolution) {
               if (IsUnique (solution)) solutions.Add (solution);
            } else solutions.Add (solution);
         }

         // Checks whether a queen can be safely placed at the given position.
         bool IsSafe (int row, int col) {
            for (int i = 0; i < row; i++) {
               int placedCol = rows[i];
               if (placedCol == col || Math.Abs (col - placedCol) == row - i) return false;
            }
            return true;
         }
      }

      // Checks whether an equivalent solution exists through rotations or reflections.
      bool IsUnique (int[] solution) {
         for (int i = 0; i < ROTATIONS; i++) {
            solution = Rotate (solution);
            if (SolutionExists (solution) || SolutionExists (Mirror (solution))) return false;
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
      int[] Mirror (int[] solution) => [.. solution.Reverse ()];

      // Checks whether the given solution already exists.
      bool SolutionExists (int[] solution)
         => solutions.Any (x => x.SequenceEqual (solution));

      // Displays the solutions and allows navigation between them.
      void DisplaySolution () {
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
            if ((key == ConsoleKey.RightArrow) && currSoln < solutions.Count - 1) currSoln++;
            if ((key == ConsoleKey.LeftArrow) && currSoln > 0) currSoln--;
            Clear ();
         }
      }

      // Displays the board for the given solution.
      void DisplayBoard (int[] solution) {
         OutputEncoding = new UnicodeEncoding ();
         WriteLine (TOP[0] + string.Join (TOP[1], Enumerable.Repeat (HORIZONTAL, n)) + TOP[2]);
         for (int i = 0; i < n; i++) {
            int placedQueen = solution[i];
            WriteLine (VERTICAL + string.Join (VERTICAL, Enumerable.Range (1, n)
                                        .Select (j => placedQueen == j - 1 ? QUEEN
                                                                           : EMPTY)) + VERTICAL);
            if (i < n - 1)
               WriteLine (MID[0] + string.Join (MID[1], Enumerable.Repeat (HORIZONTAL, n))
                          + MID[2]);
         }
         WriteLine (BOTTOM[0] + string.Join (BOTTOM[1], Enumerable.Repeat (HORIZONTAL, n))
                    + BOTTOM[2]);
      }

      // Reads and validates the number of queens entered by the user.
      int GetBoardSize () {
         for (; ; ) {
            if (int.TryParse (ReadLine (), out int n) && n > 0) return n;
            Write ("Enter a positive number: ");
         }

      }
   }
   #endregion

   #region const ---------------------------------------------------
   const string TOP = "┌┬┐";
   const string MID = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int ROTATIONS = 4;
   #endregion
}
#endregion