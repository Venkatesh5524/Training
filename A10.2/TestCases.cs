// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Testing the double ended queue implementation.
// ------------------------------------------------------------------------------------------------

using static System.Console;

#region TestCases ---------------------------------------------------------------------------------
/// <summary>Tests the double ended queue implementation</summary>
class TestCases {
   #region Methods --------------------------------------------------
   // Runs all deque test cases
   public static void Run () {
      TestEmptyDeque ();
      TestDeque ("EnqueueFront and DequeueRear order", count: 4, front: true);
      TestDeque ("EnqueueRear and DequeueFront order", count: 4, front: false);
      TestMixedOrder ();
      TestWrap ("Circular wrap at rear", skipStart: 4, skipCount: 2, count: 6, front: false);
      TestWrap ("Circular wrap at front", skipStart: 4, skipCount: 2, count: 6, front: true);
      TestDeque ("Resize using EnqueueRear", count: sData.Length, front: false);
      TestDeque ("Resize using EnqueueFront", count: sData.Length, front: true);
      TestWrap ("Resize after circular wrapping", skipStart: 4, skipCount: 2, count: sData.Length, front: true);
      TestIsEmpty ();
      TestCount ();
   }
   #endregion

   #region Implementations ------------------------------------------
   // Tests if dequeue operations throw an exception for an empty deque
   static void TestEmptyDeque () {
      TDeque<int> deque = new ();
      bool headPassed = ThrowsEmpty (deque.DequeueFront);
      bool tailPassed = ThrowsEmpty (deque.DequeueRear);
      PrintStatus ("DequeueFront on empty deque", headPassed);
      PrintStatus ("DequeueRear on empty deque", tailPassed);
   }

   static void TestDeque (string message, int count, bool front) {
      TDeque<int> deque = new ();
      Enqueue (deque, 0, count, front);
      bool passed = Compare (deque, 0, count, !front);
      PrintStatus (message, passed);
   }

   // Tests insertion and removal from both ends
   static void TestMixedOrder () {
      TDeque<int> deque = new ();
      Enqueue (deque, start: 1, count: 5, isFront: false, isMixed: true);
      bool passed = deque.DequeueFront () == 40;
      passed &= deque.DequeueRear () == 30;
      Enqueue (deque, start: 5, count: sData.Length, isFront: false, isMixed: true);
      passed &= deque.DequeueFront () == 60;
      passed &= deque.DequeueRear () == 50;
      passed &= deque.DequeueFront () == 20;
      passed &= deque.DequeueRear () == 10;
      PrintStatus ("Mixed Enqueue and Dequeue order", passed);
   }

   // Tests circular buffer wraparound by removing elements and reusing freed positions
   static void TestWrap (string message, int skipStart, int skipCount, int count, bool front) {
      TDeque<int> deque = new ();
      Enqueue (deque, 0, skipStart, front);
      for (int i = 0; i < skipCount; i++) {
         if (front) deque.DequeueRear ();
         else deque.DequeueFront ();
      }
      Enqueue (deque, skipStart, count, front);
      bool passed = Compare (deque, skipCount, count, !front);
      PrintStatus (message, passed);
   }

   // Tests whether the deque is empty
   static void TestIsEmpty () {
      TDeque<int> deque = new ();
      PrintStatus ("Initially empty", deque.IsEmpty);
      deque.EnqueueRear (10);
      PrintStatus ("Not empty after EnqueueRear", !deque.IsEmpty);
      deque.DequeueFront ();
      PrintStatus ("Empty after DequeueFront", deque.IsEmpty);
      deque.EnqueueFront (20);
      PrintStatus ("Not empty after EnqueueFront", !deque.IsEmpty);
      deque.DequeueRear ();
      PrintStatus ("Empty after DequeueRear", deque.IsEmpty);
   }

   // Tests whether Count is updated correctly
   static void TestCount () {
      TDeque<int> deque = new ();
      bool passed = deque.Count == 0;
      Enqueue (deque, start: 0, count: 3, isFront: false, isMixed: true);
      passed &= deque.Count == 3;
      deque.DequeueFront ();
      passed &= deque.Count == 2;
      deque.DequeueRear ();
      passed &= deque.Count == 1;
      deque.DequeueFront ();
      passed &= deque.Count == 0;
      PrintStatus ("Count updates correctly", passed);
   }

   // Displays the test status
   static void PrintStatus (string name, bool passed) {
      Write ("[");
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      Write (passed ? "PASS" : "FAIL");
      ResetColor ();
      WriteLine ($"] {name}");
   }

   // Checks whether a dequeue operation throws on an empty queue
   static bool ThrowsEmpty (Func<int> dequeue) {
      try {
         dequeue ();
      } catch (InvalidOperationException) {
         return true;
      }
      return false;
   }

   // Enqueues elements at the specified end of the deque
   static void Enqueue (TDeque<int> deque, int start, int count, bool isFront, bool isMixed = false) {
      Action<int> enqueue = isFront ? deque.EnqueueFront : deque.EnqueueRear;
      for (int i = start; i < count; i++) {
         enqueue (sData[i]);
         if (isMixed) {
            isFront = !isFront;
            enqueue = isFront ? deque.EnqueueFront : deque.EnqueueRear;
         }
      }
   }

   // Compares elements dequeued from the specified end of the deque
   static bool Compare (TDeque<int> deque, int start, int count, bool isFront) {
      bool isPassed = true;
      Func<int> dequeue = isFront ? deque.DequeueFront : deque.DequeueRear;
      for (int i = start; i < count; i++) isPassed &= dequeue () == sData[i];
      return isPassed && deque.IsEmpty;
   }
   #endregion

   #region Private data ---------------------------------------------
   static readonly int[] sData = { 0, 10, 20, 30, 40, 50, 60 };    // Test Values
   #endregion
}
#endregion