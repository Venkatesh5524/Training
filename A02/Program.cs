using System.Runtime.CompilerServices;

namespace A02;

class Program
{
    static void Main(string[] args)
    {
      int num = new Random ().Next (1, 101); ;
      int res;
      int count = 0;
      do {
         Console.Write ("Enter the number between 1 to 100 to be guessed: ");
         res = ReadInt ();
         if (res < num) {
            Console.WriteLine ("Your Guess is too Low");
         } else if (res > num) {
            Console.WriteLine ("Your Guess is too High");
         }
         if (count > 6 && res != num) {
            Console.WriteLine ("You are too slow");
         }
         count++;
      } while (res != num);


      int ReadInt () {
         for(; ; ) {
            if(int.TryParse (Console.ReadLine (), out int result) ) {
               if(result > 0 && result < 101) {
                  return result;
               }
            } 
            Console.WriteLine ("Enter a valid Number: ");
            
         }         
      }
      Console.WriteLine ("You Guessed Correctly");
   }
}
