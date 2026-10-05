// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to convert string to Double
// ------------------------------------------------------------------------------------------------

using static System.Console;

#region Program -----------------------------------------------------------------------------------
class Program {
   static void Main () {
      string[] testCases = ["10.54e23.4e3", "-1.546234e-4", "0", "0.0", "12345", "0.00000325",
         "10.54e2", "1.5e2.5", "1.e3", "1.e+3", "12.54e3.", "12.", "12.e1", ".325", " +625 ",
         "6.25e0", "6.0e0", "6.25e-1", "+6.25e1", "*6.25", "10.625","15a1", "1.567*2", "+-12",
         "12.-5", ".e1", "-0.325", "  12.456    ", "12e1000"];
      WriteLine ($"{"Input", 10} {"CustomValue", 20} {"Original Value", 20}");
      foreach (string testCase in testCases) {
         double customValue = DoubleParse (testCase);
         double originalValue = double.TryParse (testCase, out double val) ? val : double.NaN;
         Write ($"{testCase, 10}");
         Write ($"{customValue, 20}");
         WriteLine ($"{originalValue, 20}");
      }
   }

   #region Implementation -------------------------------------------
   // Parses the input string and returns a double value.
   static double DoubleParse (string input) {
      input = input.Trim ();
      int index = 0, sign = 1, len = input.Length;
      // Validates that the input is not empty
      if (len == 0) return double.NaN;
      // Handles the double starting with '+' or '-'
      if (index < len && (input[index] == '+' || input[index] == '-')) {
         if (input[index] == '-') sign = -1;
         index++;
      }
      double result = 0;
      var (digit, digitCount) = ParseDigits ();
      result += digit;
      // Ensures that at least one digit is present before the decimal point
      if (index < len && digitCount == 0) return double.NaN;
      // Checks for a decimal point
      if (index < len && input[index] == '.') {
         index++;
         var (dec, decDigits) = ParseDigits ();
         // Ensures that the decimal point is followed by digits
         if (decDigits == 0) return double.NaN;
         result += dec / Math.Pow (10, decDigits);
      }
      // Checks for an exponent
      if (index < len && (input[index] == 'E' || input[index] == 'e')) {
         index++;
         int expoSign = 1;
         if (index < len && (input[index] == '+' || input[index] == '-')) {
            if (input[index] == '-') expoSign = -1;
            index++;
         }
         var (expo, expoDigits) = ParseDigits ();
         // Ensures that the exponent contains digits
         if (expoDigits == 0) return double.NaN;
         double power = Math.Pow (10, expo);
         result = expoSign == 1 ? result * power : result / power;
      }
      // Rejects the input when unprocessed characters remain
      if (index < len) return double.NaN;
      result *= sign;
      return result;

      // Extracts consecutive digits and returns the number along with its digit count
      (int digits, int count) ParseDigits () {
         int digits = 0, count = 0;
         while (index < len && char.IsDigit (input[index])) {
            digits = digits * 10 + (input[index] - '0');
            count++;
            index++;
         }
         return (digits, count);
      }
   }
   #endregion
}
#endregion