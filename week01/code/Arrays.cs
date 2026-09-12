public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // 1. Create a new array of doubles sized 'length' to hold the results.
        // 2. Loop through the array from index 0 to length - 1.
        // 3. For each index i, the multiple we want is (i + 1) times 'number',
        //    since index 0 should hold the 1st multiple, index 1 the 2nd multiple, etc.
        // 4. Store that calculated value in the array at index i.
        // 5. After the loop finishes, return the completed array.

        var multiples = new double[length];
        for (var i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // 1. Rotating right by 'amount' means the last 'amount' items in the list need
        //    to move to the front, and everything else shifts back to make room.
        // 2. Figure out where that "last amount items" section starts. Since the list
        //    is data.Count items long, that section starts at index (data.Count - amount).
        // 3. Use GetRange to pull out a copy of that tail section (the values that need
        //    to move to the front).
        // 4. Use RemoveRange to delete that tail section from the end of the original list,
        //    now that we've saved a copy of it.
        // 5. Use InsertRange to insert the saved tail section back into the list at index 0,
        //    putting it at the front. The remaining original items automatically shift
        //    right to make room, since we're modifying the list in place.

        var splitIndex = data.Count - amount;
        var tail = data.GetRange(splitIndex, amount);
        data.RemoveRange(splitIndex, amount);
        data.InsertRange(0, tail);
    }
}