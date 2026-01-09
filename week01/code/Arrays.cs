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
        // TODO Problem 1 Start
  
        // Step 1: Create a new array of doubles with the size equal to the 'length' parameter.
        // This array will hold the multiples of the 'number' parameter.

        // Step 2: Use a for loop to iterate through each index of the array (from 0 to length-1).
        // For each index i, calculate the multiple as 'number' multiplied by (i + 1),
        // because the first element should be number * 1, the second number * 2, and so on.

        // Step 3: Assign the calculated multiple to the corresponding index in the array.

        // Step 4: After the loop completes, return the populated array.

         var multiples = new double[length];

         for(var i = 0; i < length; i++){
             multiples[i] = number * (i + 1);
         }

        return multiples; // replace this return statement with your own
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
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: First, check for special situations where we don't need to rotate at all.
        // For example, if the list is empty, null, or if rotating by a full cycle
        // (like 9 times for a list of 9 items) would just bring us back to the original list.

        // Step 2: Make sure the rotation amount makes sense by reducing it to a smaller number 
        // if it's bigger than the list size.
        // This is done by using the remainder when dividing the amount by the list's length.

        // Step 3: Figure out how many positions we actually need to rotate after simplifying the amount.
        // effectiveAmount is the remainder when we divide the original rotation amount by the list size.
        // This is important because rotating a list by its full size (or multiples of it) brings it back to the start,
        // so we only care about the leftover amount. For example, rotating a list of 9 items by 10 positions
        // is the same as rotating by 1 position (since 10 divided by 9 is 1 with a remainder of 1).
        // effectiveAmount tells us exactly how many items to move from the end to the front of the list.

        // Step 4: Split the list into two pieces: the part that will move to the front (the last few items)
        // and the part that will stay at the end (the first part of the original list).

        // Step 5: Put together a new version of the list by placing the moved-to-front part first,
        // followed by the part that was originally at the beginning.

        // Step 6: Replace the contents of the original list with this newly arranged list.

        
        if (data == null || data.Count == 0 || amount % data.Count == 0)
        {
            return;
        }

        int effectiveAmount = amount % data.Count;

        
        List<int> firstRange = data.GetRange(data.Count - effectiveAmount, effectiveAmount);

        List<int> secondRange = data.GetRange(0, data.Count - effectiveAmount);

        List<int> RotatedList = new List<int>();
        RotatedList.AddRange(firstRange);
        RotatedList.AddRange(secondRange);

        data.Clear();
        data.AddRange(RotatedList);
    }
}
