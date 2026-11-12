// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Deque.cs
// Double Ended Queue implementation with circular buffer.
// ------------------------------------------------------------------------------------------------

#region class Deque -------------------------------------------------------------------------------
/// <summary>Implementation of custom queue that adds and removes elements in FIFO order</summary>
class TDeque<T> {
   #region Property -------------------------------------------------
   /// <summary>Checks whether the queue contains no elements</summary>
   public bool IsEmpty => mCount == 0;

   /// <summary>Gets the number of elements in the deque</summary>
   public int Count => mCount;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds an element to the rear of the queue</summary>
   public void EnqueueRear (T value) => Enqueue (value, EState.Rear);

   /// <summary>Adds an element to the front of the queue</summary>
   public void EnqueueFront (T value) => Enqueue (value, EState.Front);

   /// <summary>Removes and returns the element from the front of the queue</summary>
   public T DequeueFront () => Dequeue(EState.Front);

   /// <summary>Removes and returns the element from the rear of the queue</summary>
   public T DequeueRear () => Dequeue(EState.Rear);
   #endregion

   #region Implementation -------------------------------------------
   // Doubles the queue capacity and rearranges the elements in queue order
   void Resize () {
      T[] temp = new T[mData.Length * 2];
      for (int i = 0; i < mCount; i++) temp[i] = mData[(mStart + i) % mData.Length];
      mData = temp;
      mStart = 0;
      mEnd = mCount;
   }

   // Inserts an element at the specified end of the deque
   void Enqueue (T value, EState state) {
      int capacity = mData.Length;
      if (mCount == capacity) Resize ();
      if(state is EState.Rear) {
         mData[mEnd] = value;
         mEnd = (mEnd + 1) % capacity;
      } else {
         mStart = (mStart + capacity - 1) % capacity;
         mData[mStart] = value;
      }
      mCount++;
   }

   // Removes and returns an element from the specified end of the deque
   T Dequeue (EState state) {
      if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
      int capacity = mData.Length;
      T value;
      if(state is EState.Rear) {
         mEnd = (mEnd + capacity - 1) % capacity;
         value = mData[mEnd];
         mData[mEnd] = default!;
      } else {
         value = mData[mStart];
         mData[mStart] = default!;
         mStart = (mStart + 1) % capacity;
      }
      mCount--;
      return value;
   }
   #endregion

   #region Fields ---------------------------------------------------
   T[] mData = new T[INITCAPACITY];    // Stores the queue elements
   int mEnd = 0;            // Index of the next element to be added or removed at the rear
   int mStart = 0;          // Index of the next element to be added or removed at the front
   int mCount = 0;          // Number of elements currently in the queue
   const int INITCAPACITY = 4;
   #endregion

   #region Enum EState
   enum EState { Front, Rear}
   #endregion
}
#endregion