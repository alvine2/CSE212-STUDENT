using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

// TODO Problem 2 - Create test cases, execute them, and correct the implementation
// so that all requirements are satisfied.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add several items with varying priority values and remove one item.
    // Expected Result: The element with the greatest priority value should be removed first.
    // Defect(s) Found:
    //      1 - The comparison logic inside Dequeue() incorrectly uses '>=' instead of '>',
    //          which causes the algorithm to continue selecting later elements even when
    //          priorities are equal or already matched.
    //      2 - Because of this comparison, the queue may return the wrong item instead of
    //          the true highest-priority element.

    public void TestPriorityQueue_DequeueHighestPriority()
    {
        Debug.WriteLine("*** Test 1: DequeueHighestPriority ***");
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("LowPriorityItem", 1);
        priorityQueue.Enqueue("HighPriorityItem", 10);
        priorityQueue.Enqueue("MediumPriorityItem", 5);

        Debug.WriteLine(priorityQueue.ToString());

        var dequeuedItem = priorityQueue.Dequeue();
        Debug.WriteLine($"Dequeued Item: {dequeuedItem}");

        Assert.AreEqual("HighPriorityItem", dequeuedItem,
            "The item with the highest priority value should be returned first.");
    }

    [TestMethod]
    // Scenario: Add multiple items that all share the same priority value.
    // Expected Result: When priorities are equal, the queue should follow FIFO order.
    // Defect(s) Found:
    //      1 - Items with matching priorities are not dequeued in the order they were added.
    //      2 - The priority comparison logic treats equal values as higher priority,
    //          allowing newer items to incorrectly override earlier ones.
    //      3 - The removal logic does not consistently remove the correct index,
    //          leading to unexpected dequeue results.

    public void TestPriorityQueue_DequeueDuplicatePriorities()
    {
        Debug.WriteLine("*** Test 2: DequeueDuplicatePriorities ***");
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstItem", 5);
        priorityQueue.Enqueue("SecondItem", 5);
        priorityQueue.Enqueue("ThirdItem", 5);

        Debug.WriteLine(priorityQueue.ToString());

        var dequeuedItem = priorityQueue.Dequeue();
        Debug.WriteLine($"Dequeued Item: {dequeuedItem}");

        Assert.AreEqual("FirstItem", dequeuedItem,
            "For equal priorities, the earliest enqueued item should be dequeued first.");
    }

    [TestMethod]
    // Scenario: Attempt to remove an item from an empty priority queue.
    // Expected Result: An InvalidOperationException should be raised.
    // Defect(s) Found:
    //      No defects detected. The method correctly throws an exception
    //      with an appropriate error message.

    public void TestPriorityQueue_DequeueEmptyQueue()
    {
        Debug.WriteLine("*** Test 3: DequeueEmptyQueue ***");
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("An exception was expected but was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message,
                "The exception message should clearly indicate that the queue is empty.");
            Debug.WriteLine("Caught expected exception: " + e.Message);
        }
    }
}
