using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1
        // Base case: If n is 0 or less, the sum is 0.
        if (n <= 0)
        {
            return 0;
        }
        
        // Recursive step: The sum up to n is n^2 plus the sum up to (n-1).
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2
        // Base case: If we have chosen enough letters (size is 0),
        // we have a complete permutation. Add it to the results list.
        if (size == 0)
        {
            results.Add(word);
            return;
        }

        // Recursive step: Iterate through all available letters.
        for (int i = 0; i < letters.Length; i++)
        {
            // Create a new string with the remaining letters (excluding the current one).
            string remainingLetters = letters.Remove(i, 1);
            // Recursively call the function with the chosen letter added to the word,
            // the remaining letters, and one less size to choose.
            PermutationsChoose(results, remainingLetters, size - 1, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // TODO Start Problem 3
        // Initialize the memoization dictionary on the first call.
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base cases for the recursion.
        if (s < 0)
        {
            return 0; // Overshot the stairs, this is not a valid way.
        }
        if (s == 0)
        {
            return 1; // Exactly at the top, this counts as one successful way.
        }

        // Memoization check: If we've already calculated the ways for 's', return it.
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Recursive step: Calculate ways by summing the possibilities from the 3 previous steps.
        decimal ways = CountWaysToClimb(s - 1, remember) + CountWaysToClimb(s - 2, remember) + CountWaysToClimb(s - 3, remember);
        
        // Store the result in the dictionary before returning.
        remember[s] = ways;
        
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    /// 
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4
        // Find the index of the first wildcard character '*'.
        int index = pattern.IndexOf('*');

        // Base case: If no wildcard is found, the pattern is a complete binary string.
        // Add it to the results and stop this recursive path.
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive step: If a wildcard is found, create two new patterns.
        // One with the wildcard replaced by '0' and another with '1'.
        // Then, make a recursive call for each new pattern.
        char[] patternChars = pattern.ToCharArray();
        
        patternChars[index] = '0';
        WildcardBinary(new string(patternChars), results);

        patternChars[index] = '1';
        WildcardBinary(new string(patternChars), results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        // TODO Start Problem 5
        
        // Add the current location (x, y) to the current path.
        currPath.Add((x, y));

        // Base Case: If the current location is the end of the maze,
        // we have found a valid solution.
        if (maze.IsEnd(x, y))
        {
            // Add the found path to the list of results.
            results.Add(currPath.AsString());
        }
        else
        {
            // Recursive Step: Explore all valid neighboring cells.

            // Try to move Right
            if (maze.IsValidMove(currPath, x + 1, y))
            {
                SolveMaze(results, maze, x + 1, y, currPath);
            }

            // Try to move Down
            if (maze.IsValidMove(currPath, x, y + 1))
            {
                SolveMaze(results, maze, x, y + 1, currPath);
            }

            // Try to move Left
            if (maze.IsValidMove(currPath, x - 1, y))
            {
                SolveMaze(results, maze, x - 1, y, currPath);
            }

            // Try to move Up
            if (maze.IsValidMove(currPath, x, y - 1))
            {
                SolveMaze(results, maze, x, y - 1, currPath);
            }
        }

        // Backtrack: Remove the current location from the path. This is crucial.
        // It allows us to "undo" our move and explore other potential paths.
        currPath.RemoveAt(currPath.Count - 1);
    }
}