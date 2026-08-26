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
      List<int[]> allSolutions = [];
      List<int[]> uniqueSolutions = [];
      int[] rows = new int[n];
      Write ("Processing...  ");
      FindSolutions (0);
      SetCursorPosition (0, 1);
      if (allSolutions.Count == 0) {
         WriteLine ($"No solution exists for {n} Queens.");
         Write ("Press any key to exit... ");
         ReadKey (true);
         return;
      }
      Write ("Press (P) to see all possible solutions or (U) to see only unique solutions: ");
      ConsoleKey response;
      while ((response = ReadKey (true).Key) is not ConsoleKey.P and not ConsoleKey.U) { }
      WriteLine (response);
      if (response == ConsoleKey.P) {
         WriteLine ($"Total possible solutions for {n} queens: {allSolutions.Count}");
         DisplaySolution (allSolutions);
      } else {
         foreach (int[] solution in allSolutions)
            if (IsUnique (solution)) uniqueSolutions.Add (solution);
         WriteLine ($"Total unique solutions for {n} queens: {uniqueSolutions.Count}");
         DisplaySolution (uniqueSolutions);
      }

      // Finds all possible N-Queens solutions
      void FindSolutions (int row) {
         for (int col = 0; col < n; col++)
            if (IsSafe (row, col)) {
               rows[row] = col;
               if (row == n - 1) AddSolution ([.. rows]);
               else FindSolutions (row + 1);
            }

         // Adds a solution to the list of all solutions.
         void AddSolution (int[] solution) => allSolutions.Add (solution);

         // Checks whether a queen can be safely placed at the given position.
         bool IsSafe (int row, int col) {
            for (int i = 0; i < row; i++) {
               int placedCol = rows[i];
               if (placedCol == col || Math.Abs (col - placedCol) == row - i) return false;
            }
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
      int[] Mirror (int[] solution) => [.. solution.Reverse ()];

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
                                                                            or ConsoleKey.Escape)) ;
            if (key == ConsoleKey.Escape) break;
            else if ((key == ConsoleKey.RightArrow) && currSoln < solutions.Count - 1) currSoln++;
            else if ((key == ConsoleKey.LeftArrow) && currSoln > 0) currSoln--;
            Clear ();
         }
      }

      // Displays the board for the given solution.
      void DisplayBoard (int[] solution) {
         OutputEncoding = new UnicodeEncoding ();
         int num = solution.Length;
         WriteLine (TOPLEFT + string.Join ("", Enumerable.Repeat (TOPEDGE, num - 1)) + TOPRIGHT);
         for (int i = 0; i < num; i++) {
            WriteLine (VERT + string.Join ("", Enumerable.Range (1, num)
                                                         .Select (j => solution[i] == j - 1 ? QUEEN
                                                                                        : EMPTY)));
            if (i < num - 1)
               WriteLine (MIDLEFT + string.Join ("", Enumerable.Repeat (MIDEDGE, num - 1))
                          + MIDRIGHT);
         }
         WriteLine (BOTTOMLEFT + string.Join ("", Enumerable.Repeat (BOTTOMEDGE, num - 1))
                    + BOTTOMRIGHT);
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
   const string TOPLEFT = "┌", TOPEDGE = "────┬", TOPRIGHT = "────┐", MIDLEFT = "├",
                MIDEDGE = "────┼", MIDRIGHT = "────┤", BOTTOMLEFT = "└", BOTTOMEDGE = "────┴",
                BOTTOMRIGHT = "────┘", VERT = "│", EMPTY = "    │", QUEEN = " ♕  │";
   #endregion
}
#endregion