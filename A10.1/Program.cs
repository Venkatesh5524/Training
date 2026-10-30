// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Implements parsing of a string representation of a file path
// ------------------------------------------------------------------------------------------------

using System.Text;
using static EState;

#region class Program -----------------------------------------------------------------------------
class Program {
   #region Method ---------------------------------------------------
   static void Main () {
      string[] inputs = [@"D:\WORK\FILE.TXT", @"C:\A\B\C\D\E\F\G\FILE.TXT", @"C:\DIR\\FILE.TXT",
                         @"CZ:\AB\CD\E.TXT", @"C:README.TXT", @"C:\README.TXT", @"C:\AB\READ.",
                         @"c:\aBc\readme.txt", @"READ.TXT", @"C:\A\B", " ", "abc", @".\ABC",".abc",
                         @"\AB\READ.TXT", @"C:\A.B\DE\R.TXT", @"C:\AB:DC", @"C:\WORK\R.TXT#",
                         @"C:\DIR\ARCHIVE.TAR.GZ", @"C:\WORK23\R.TXT"];
      Console.WriteLine ($" {"Input", -30}| {"Drive", -5}| {"Directory", -15}| {"FileName", -10}" +
                         $"| {"Extension", -10}| {"Status", -10}");
      Console.WriteLine (new string ('-',100));
      foreach (string input in inputs) {
         var (drive, dir, fileName, ext, isPassed) = FileParse (input);
         Console.Write ($" {input, -30}| {drive, -5}| {dir, -15}| {fileName, -10}| {ext, -10}| ");
         Console.ForegroundColor = isPassed ? ConsoleColor.Green : ConsoleColor.Red;
         Console.WriteLine ($"{(isPassed ? "VALID" : "INVALID"), -10}");
         Console.ResetColor ();
      }
   }
   #endregion

   #region Implementation -------------------------------------------
   //Parses a file path into drive, directory, file name, extension and returns its parsing status
   static (string, string, string, string, bool) FileParse (string input) {
      input = input.Trim () + "~";
      EState s = A;
      Action none = () => { };
      Action todo;
      string dL = "";
      StringBuilder dir = new (), file = new(), ext = new();
      foreach (char c in input) {
         (s, todo) = (s, c) switch {
            (A, >= 'A' and <= 'Z') => (B, () => dL = c.ToString()),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => file.Append(c)),
            (E, '\\') => (F, () => { dir.Append (file); file.Clear (); }),
            (F or G, >= 'A' and <= 'Z') => (G, () => file.Append (c)),
            (G, '\\') => (F, () => { dir.Append (',').Append (file); file.Clear (); }),
            (G, '.') => (H, none),
            (H or I, >= 'A' and <= 'Z') => (I, () => ext.Append(c)),
            (I, '~') => (J, none),
            _ => (Z, none),
         };
         todo ();
      }
      bool isPass = s == J;
      if (isPass) return (dL, dir.ToString(), file.ToString(), ext.ToString(), isPass);
      return ("-", "-", "-", "-", isPass);
   }
   #endregion
}
#endregion

#region Enum EState -------------------------------------------------------------------------------
/// <summary>Represents the parser states for evaluating file paths</summary>
enum EState {
   A,    // Starting state
   B,    // Drive Letter
   C,    // Colon (:)
   D,    // Backslash (/) after colon
   E,    // Directory name
   F,    // Backslash after directory name
   G,    // Directory or file name
   H,    // Extension dot (.)
   I,    // File extension
   J,    // Success state
   Z     // Error state
};
#endregion