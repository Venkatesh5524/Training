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
      List<int[]> solutions = [];
      HashSet<string> symmetries = [];
      int[] rows = new int[N];
      Write ("Would you like to see (A)ll solutions or (U)nique solutions only? ");
      ConsoleKey response;
      while ((response = ReadKey (true).Key) is not ConsoleKey.A and not ConsoleKey.U) { }
      WriteLine (response);
      bool showUniqueSolution = response == ConsoleKey.U;
      Write ("Processing...  ");
      FindSolutions (0);
      Clear ();
      if (solutions.Count == 0) {
         WriteLine ($"No solution exists for {N} Queens.");
         Write ("Press any key to exit... ");
         ReadKey (true);
         return;
      }
      WriteLine ($"Total {(showUniqueSolution ? "Unique" : "Possible")} solutions for {N} " +
                 $"Queens: {solutions.Count}");
      Write ("Press any key to print the solution");
      ReadKey ();
      DisplaySolution ();

      // Finds all possible N-Queens solutions
      void FindSolutions (int row) {
         for (int col = 0; col < N; col++)
            if (IsSafe (row, col)) {
               rows[row] = col;
               if (row == N - 1) AddSolution ([.. rows]);
               else FindSolutions (row + 1);
            }

         // Checks whether a queen can be safely placed at the given position.
         bool IsSafe (int row, int col) {
            for (int i = 0; i < row; i++) {
               int placedCol = rows[i];
               if (placedCol == col || Math.Abs (col - placedCol) == row - i) return false;
            }
            return true;
         }

         // Adds the solution directly or only if it is unique, based on the selected mode.
         void AddSolution (int[] solution) {
            if (!showUniqueSolution || IsUnique(solution)) {
               solutions.Add (solution);
               if (showUniqueSolution) AddSymmetries (solution);
            }
         }

         // Checks whether the given configuration is not already present in the set of symmetries.
         bool IsUnique (int[] solution) => !symmetries.Contains (ConvertToString(solution));

         // Adds all rotated and mirrored configurations of the given solution to the set.
         void AddSymmetries (int[] solution) {
            for (int i = 0; i < ROTATIONS; i++) {
               solution = Rotate (solution);
               symmetries.Add (ConvertToString(solution));
               symmetries.Add (ConvertToString(Mirror (solution)));
            }
         }

         // Rotates the solution by 90 degrees.
         int[] Rotate (int[] solution) {
            int[] rotated = new int[N];
            for (int row = 0; row < N; row++) rotated[solution[row]] = N - 1 - row;
            return rotated;
         }

         // Creates a mirrored version of the given solution.
         int[] Mirror (int[] solution) => [.. solution.Reverse ()];

         // Converts the given array into string.
         string ConvertToString (int[] arr) => string.Join(",", arr);
      }

      // Displays the solutions and allows navigation between them.
      void DisplaySolution () {
         OutputEncoding = new UnicodeEncoding ();
         int currSoln = 0;
         Clear ();
         while (currSoln < solutions.Count) {
            Clear ();
            WriteLine ($"\nSolution {currSoln + 1} of {solutions.Count} ");
            DisplayBoard (solutions[currSoln]);
            Write ("\nPress \u2192 to see the next solution... " +
               "\nPress \u2190 to see the previous solution... \nPress esc to exit... ");
            ConsoleKey key;
            while ((key = ReadKey (true).Key) is not (ConsoleKey.RightArrow or ConsoleKey.LeftArrow
                                                                            or ConsoleKey.Escape)) ;
            if (key == ConsoleKey.Escape) break;
            if ((key == ConsoleKey.RightArrow) && currSoln < solutions.Count - 1) currSoln++;
            if ((key == ConsoleKey.LeftArrow) && currSoln > 0) currSoln--;
         }
      }

      // Displays the board for the given solution.
      void DisplayBoard (int[] solution) {
         WriteLine (Border (TOP));
         for (int i = 0; i < N; i++) {
            int placedQueen = solution[i];
            WriteLine (VERTICAL + string.Join (VERTICAL, Enumerable.Range (1, N)
                                        .Select (j => placedQueen == j - 1 ? QUEEN
                                                                           : EMPTY)) + VERTICAL);
            if (i < N - 1) WriteLine (Border (MIDDLE));
         }
         WriteLine (Border (BOTTOM));

         // Builds a horizontal border line from the given corner and joint characters.
         string Border (string pattern)
            => pattern[0] + string.Join (pattern[1], Enumerable.Repeat (HORIZONTAL, N)) + pattern[2];
      }
   }
   #endregion

   #region const ----------------------------------------------------
   const string TOP = "┌┬┐";
   const string MIDDLE = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int ROTATIONS = 4;
   const int N = 8;                          // Number of Queens
   #endregion
}
#endregion