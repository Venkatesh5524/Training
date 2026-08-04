namespace A03;

class Program {
   static void Main (string[] args) {
      char[] letters = new char[] { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
      string[] words = File.ReadAllLines ("words.txt");
      Dictionary<string, int> result = new Dictionary<string, int> ();
      foreach (string word in words)
         if (Is_Valid (word) == true)
            result[word] = Score (word);

      var sorted_result = result.OrderByDescending (x => x.Value).ThenBy (x => x.Key);
      int total = 0;
      foreach (var ans in sorted_result) {
         if (Panagram (ans.Key) == true) Console.ForegroundColor = ConsoleColor.Green;
         else Console.ResetColor ();
         total += ans.Value;
         Console.WriteLine ($"{ans.Value,3}: {ans.Key}");
      }
      Console.WriteLine ("----");
      Console.WriteLine ($"{total,3}: Total");

      bool Is_Valid (string word) {
         if (word.Length < 4) return false;
         if (!word.Contains (letters[0])) return false;
         foreach (char h in word) {
            if (!letters.Contains (h)) return false;
         }
         return true;
      }

      int Score (string word) {
         int temp = 0;
         int len = word.Length;
         if (len == 4) temp++;
         else if (len > 4 && len < 7) temp += len;
         else if (len >= 7) {
            if (Panagram (word)) {
               temp = temp + len + 7;
            } else temp += len;
         }
         return temp;
      }

      bool Panagram (string word) {
         foreach (char ch in letters)
            if (!word.Contains (ch)) return false;
         return true;
      }
   }
}
