// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
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
         "12.-5", ".e1", "-0.325", "  12.456    "];
      foreach (string testCase in testCases) {
         double customValue = DoubleParse (testCase.Trim ());
         double originalValue = double.TryParse (testCase, out double val) ? val : double.NaN;
         WriteLine ($"Input         : {testCase}");
         WriteLine ($"Custom value  : {customValue}");
         WriteLine ($"Original value: {originalValue}");
      }
   }


   #region Implementation -------------------------------------------
   static double DoubleParse (string input) {
      double result = 0;
      int index = 0, sign = 1, digitCount = 0, len = input.Length;
      if (index < len && (input[index] == '+' || input[index] == '-')) {
         if (input[index] == '-') sign = -1;
         index++;
      }
      while (index < len && char.IsDigit (input[index])) {
         result = (input[index] - '0') + result * 10;
         digitCount++;
         index++;
      }
      if (index < len && digitCount == 0) return double.NaN;
      if (index < len && input[index] == '.') {
         int decDigits = 0;
         int dec = 0;
         index++;
         while (index < len && char.IsDigit (input[index])) {
            dec = dec * 10 + (input[index] - '0');
            decDigits++;
            index++;
         }
         if (decDigits == 0) return double.NaN;
         result += dec / Math.Pow (10, decDigits);
      }
      if (index < len && (input[index] == 'E' || input[index] == 'e')) {
         index++;
         int expo = 0, expoSign = 1, expoDigits = 0;
         if (index < len && (input[index] == '+' || input[index] == '-')) {
            if (input[index] == '-') expoSign = -1;
            index++;
         }
         while (index < len && char.IsDigit (input[index])) {
            expo = expo * 10 + (input[index] - '0');
            expoDigits++;
            index++;
         }
         if (expoDigits == 0) return double.NaN;
         double power = Math.Pow (10, expo);
         if (expoSign == 1) result *= power;
         else result /= power;
      }
      if (index < len) return double.NaN;
      result *= sign;
      return result;
   }
   #endregion
}
#endregion