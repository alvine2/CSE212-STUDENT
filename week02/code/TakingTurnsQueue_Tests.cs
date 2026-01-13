using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics; // For Debug.WriteLine

// TODO Problem 1 - Execute the provided tests and note any issues discovered
// in the comment section above each test. Do not change test logic.
// Update the implementation code so all tests succeed.

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Initialize a queue containing Bob (2 turns), Tim (5 turns), and Sue (3 turns),
    // then continue processing until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found:
    //      1 - New people were being inserted at the beginning of the collection,
    //          resulting in LIFO behavior instead of FIFO.
    //      2 - Participants with exactly one remaining turn were skipped,
    //          so they never completed their final turn.
    //      3 - This caused Bob to appear fewer times than expected and disrupted
    //          the correct ordering of turns.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        Debug.WriteLine("*** Test: FiniteRepetition ***");
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Debug.WriteLine($"Turn {i + 1}: Got {person.Name}, Expected {expectedResult[i].Name}, Queue size: {players.Length}");
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
        Debug.WriteLine($"Test completed: Processed {i} turns successfully");
    }

    [TestMethod]
    // Scenario: Begin with Bob (2), Tim (5), and Sue (3). After five turns,
    // insert George (3 turns) and continue until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found:
    //      1 - Players added after processing began were placed at the front of the queue,
    //          allowing them to take turns earlier than expected.
    //      2 - Players reaching a single remaining turn were skipped,
    //          resulting in missing turns for Bob and Sue.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        Debug.WriteLine("*** Test: AddPlayerMidway ***");
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Debug.WriteLine($"First phase, Turn {i + 1}: Got {person.Name}, Expected {expectedResult[i].Name}");
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        Debug.WriteLine("Adding George to the queue during processing");
        players.AddPerson("George", 3);

        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Debug.WriteLine($"Second phase, Turn {i + 1}: Got {person.Name}, Expected {expectedResult[i].Name}, Queue size: {players.Length}");
            Assert.AreEqual(expectedResult[i].Name, person.Name);

            i++;
        }
        Debug.WriteLine($"Test completed: Processed {i} turns successfully");
    }

    [TestMethod]
    // Scenario: Add Bob (2 turns), Tim (infinite turns represented by 0), and Sue (3 turns),
    // then process ten turns.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found:
    //      1 - Queue order was reversed due to incorrect insertion logic.
    //      2 - Final turns for players with limited turns were skipped.
    //      3 - Infinite-turn handling caused unexpected turn patterns.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        Debug.WriteLine("*** Test: ForeverZero ***");
        var timTurns = 0;

        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Debug.WriteLine($"Turn {i + 1}: Got {person.Name}, Expected {expectedResult[i].Name}, Tim's turns: {tim.Turns}");
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        var infinitePerson = players.GetNextPerson();
        Debug.WriteLine($"After 10 turns, Tim still has {infinitePerson.Turns} turns");
        Assert.AreEqual(timTurns, infinitePerson.Turns);
    }

    [TestMethod]
    // Scenario: Start with Tim (negative turns indicating infinite) and Sue (3 turns),
    // then process ten turns.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found:
    //      1 - Queue ordering was reversed during insertion.
    //      2 - Sue’s final turn was skipped when her remaining turns reached one.
    //      3 - Infinite-turn players were not consistently rotated as expected.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        Debug.WriteLine("*** Test: ForeverNegative ***");
        var timTurns = -3;
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [tim, sue, tim, sue, tim, sue, tim, tim, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Debug.WriteLine($"Turn {i + 1}: Got {person.Name}, Expected {expectedResult[i].Name}");
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        var infinitePerson = players.GetNextPerson();
        Debug.WriteLine($"After 10 turns, Tim still has {infinitePerson.Turns} turns");
        Assert.AreEqual(timTurns, infinitePerson.Turns);
    }

    [TestMethod]
    // Scenario: Attempt to retrieve a person when the queue is empty.
    // Expected Result: An InvalidOperationException should be raised.
    // Defect(s) Found:
    //      No issues observed. The queue correctly throws an exception
    //      with the message "No one in the queue."
    public void TestTakingTurnsQueue_Empty()
    {
        Debug.WriteLine("*** Test: Empty ***");
        var players = new TakingTurnsQueue();
        Debug.WriteLine($"Queue is empty. Initial length: {players.Length}");

        try
        {
            players.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
            Debug.WriteLine($"Caught expected InvalidOperationException: '{e.Message}'");
        }
    }
}
