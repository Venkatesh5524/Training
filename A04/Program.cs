namespace A04;

class Program
{
    static void Main(string[] args)
    {
      string[] words = File.ReadAllLines (@"C:\Work\Training\A04\words 1.txt");
      Dictionary<char, int> freq = new ();
      foreach (string word in words) {
         foreach (char c in word) {
            if (c >= 'A' && c <= 'Z') {
               if (!freq.ContainsKey (c)) {
                  freq[c] = 0;
               } 
               freq[c]++;
            }
         }
      }
      int tries = 0;
      foreach (var ch in freq.OrderByDescending (a => a.Value)) {
         if (tries == 7) break;
         Console.WriteLine ($"{ch.Key} : {ch.Value}");
         tries++;
      }
      Console.WriteLine ("Press any key to exit....");
      Console.ReadKey ();
   }
}
