/// <summary>
/// Simple FIFO queue structure for managing Person objects
/// </summary>
public class PersonQueue
{
    // Internal list used to maintain queue order
    private readonly List<Person> _queue = new();

    /// <summary>
    /// Returns the current number of people in the queue
    /// </summary>
    public int Length => _queue.Count;

    /// <summary>
    /// Places a person at the end of the queue
    /// </summary>
    /// <param name="person">Person instance to be queued</param>
    public void Enqueue(Person person)
    {
        // Previously, people were inserted at index 0 which reversed the order.
        // Using Add() ensures correct FIFO queue behavior.
        _queue.Add(person);
    }

    /// <summary>
    /// Removes and returns the person at the front of the queue
    /// </summary>
    /// <returns>The next person in line</returns>
    public Person Dequeue()
    {
        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    /// <summary>
    /// Checks whether the queue contains any people
    /// </summary>
    /// <returns>True if the queue is empty; otherwise false</returns>
    public bool IsEmpty()
    {
        return Length == 0;
    }

    /// <summary>
    /// Returns a readable string showing the current queue order
    /// </summary>
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
