using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue, add items with different priorities, and dequeue.
    // Expected Result: The item with the highest priority ("Tim") should be returned.
    // Defect(s) Found: The Dequeue method finds the highest priority item but never removes it from the queue.
    // Subsequent calls to Dequeue will return the same item over and over.
    public void TestPriorityQueue_BasicDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 2);
        priorityQueue.Enqueue("Tim", 5);
        priorityQueue.Enqueue("Sue", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Tim", result);
    }

    [TestMethod]
    // Scenario: Add multiple items with the same highest priority.
    // Expected Result: The first item added with the highest priority ("Tim") should be returned, following the FIFO rule for ties.
    // Defect(s) Found: The code uses '>=' to compare priorities, which causes it to select the LAST item
    // with the highest priority, not the FIRST. It returned "George" instead of "Tim".
    public void TestPriorityQueue_TieBreaking()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 2);
        priorityQueue.Enqueue("Tim", 5);
        priorityQueue.Enqueue("Sue", 3);
        priorityQueue.Enqueue("George", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Tim", result);
    }

    [TestMethod]
    // Scenario: Call Dequeue on an empty queue.
    // Expected Result: An InvalidOperationException should be thrown with the message "The queue is empty."
    // Defect(s) Found: None. This part of the code works correctly.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("An exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
    
    [TestMethod]
    // Scenario: Add items where the highest priority item is the last one in the queue.
    // Expected Result: The last item ("Last"), which has the highest priority, should be returned.
    // Defect(s) Found: The loop in Dequeue checks 'index < _queue.Count - 1', so it never inspects the
    // very last element. The test fails because it doesn't find the item with the highest priority.
    public void TestPriorityQueue_HighestPriorityAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("Last", 5);
        
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Last", result);
    }
}