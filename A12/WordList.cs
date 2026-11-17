// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// WordsList.cs
// Manages the word lists used for word validation and random word selection.
// ------------------------------------------------------------------------------------------------

#region class WordsList ---------------------------------------------------------------------------
class WordList {
   #region Constructor ----------------------------------------------
   public WordList () {
      mValidWords = [.. File.ReadAllLines ("Validate.txt")];
      mGuessWords = File.ReadAllLines ("guess.txt");
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Returns a randomly selected word from the guessing word list</summary>
   public string GuessingWord => mGuessWords[Random.Shared.Next (mGuessWords.Length)];
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Checks whether the given word exists in the valid word list</summary>
   public bool IsValid (string word) => mValidWords.Contains (word);
   #endregion

   #region Private Data ---------------------------------------------
   readonly HashSet<string> mValidWords;
   readonly string[] mGuessWords;
   #endregion
}
#endregion