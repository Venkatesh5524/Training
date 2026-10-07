// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Testing the queue implementation.
// ------------------------------------------------------------------------------------------------

#region TestCases ---------------------------------------------------------------------------------
/// <summary>Tests the generic queue implementation</summary>
class TestCases {
   #region Method ---------------------------------------------------
   /// <summary>Executes all test cases</summary>
   public static void Run () {
      Test1 ();
      Test2 ();
      Test3 ();
      Test4 ();
      Test5 ();
      Test6 ();
   }
   #endregion

   #region Implementations ------------------------------------------
   // Displays the test result with a corresponding status message
   static void DisplayStatus (string message, bool status) {
      if (status) {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.Write ("[Pass] ");
      } else {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.Write ("[Fail] ");
      }
      Console.ResetColor ();
      Console.WriteLine (message);
   }

   // Dequeue from an empty queue
   static void Test1 () {
      TQueue<int> queue = new ();
      bool isPassed = false;
      try {
         queue.Dequeue ();
      } catch (InvalidOperationException) {
         isPassed = true;
      }
      DisplayStatus ("Dequeue from an empty queue", isPassed);
   }

   // Verify single-element queue and empty state
   static void Test2 () {
      TQueue<string> queue = new ();
      queue.Enqueue ("A");
      bool isPassed = queue.Dequeue () == "A" && queue.IsEmpty;
      DisplayStatus ("Single-element queue and empty state", isPassed);
   }

   // Verify First In First Out (FIFO) order
   static void Test3 () {
      TQueue<char> queue = new ();
      bool isPassed = true;
      queue.Enqueue ('A');
      queue.Enqueue ('B');
      queue.Enqueue ('C');
      for (int i = 0; !queue.IsEmpty; i++)
         if (queue.Dequeue () != (char)('A' + i)) isPassed = false;
      DisplayStatus ("FIFO order", isPassed);
   }

   // Check if the queue reuses the empty space at the beginning (Circular Wrap around)
   static void Test4 () {
      TQueue<char> queue = new ();
      bool isPassed = true;
      queue.Enqueue ('A');
      queue.Enqueue ('B');
      queue.Enqueue ('C');
      queue.Dequeue ();
      queue.Dequeue ();
      queue.Enqueue ('D');
      queue.Enqueue ('E');
      for (int i = 0; !queue.IsEmpty; i++)
         if (queue.Dequeue () != (char)('C' + i)) isPassed = false;
      DisplayStatus ("Reuses the empty space at the beginning", isPassed);
   }

   // Verify resize when completely full
   static void Test5 () {
      TQueue<char> queue = new ();
      bool isPassed = true;
      for (int i = 0; i < 5; i++) queue.Enqueue ((char)('A' + i));
      for (int j = 0; !queue.IsEmpty; j++)
         if (queue.Dequeue () != (char)('A' + j)) isPassed = false;
      DisplayStatus ("Resize when completely full", isPassed);
   }

   // Verify resize after circular wrap around
   static void Test6 () {
      TQueue<char> queue = new ();
      bool isPassed = true;
      for (int i = 0; i < 4; i++) queue.Enqueue ((char)('A' + i));
      queue.Dequeue ();
      queue.Dequeue ();
      queue.Enqueue ('E');
      queue.Enqueue ('F');
      queue.Enqueue ('G');
      for (int j = 0; !queue.IsEmpty; j++)
         if (queue.Dequeue () != (char)('C' + j)) isPassed = false;
      DisplayStatus ("Resize after circular wrap around", isPassed);
   }
   #endregion
}
#endregion