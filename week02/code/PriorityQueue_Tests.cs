using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities (low, high, medium) and dequeue once.
    // Expected Result: The item with the highest priority ("B", priority 5) is returned, regardless
    // of the order it was added in.
    // Defect(s) Found: The Dequeue() loop bound was "index < _queue.Count - 1", which skipped the
    // last item in the list entirely, so it could never be selected as the highest priority even
    // when it should have been. Additionally, Dequeue() never removed the item from the internal
    // list (no RemoveAt call), so the item would incorrectly remain in the queue after being
    // "dequeued".
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Enqueue two items that share the same highest priority ("B" then "C", both
    // priority 5), along with lower-priority items, then dequeue twice.
    // Expected Result: "B" is returned first since it is closer to the front of the queue (FIFO
    // tie-breaking rule), followed by "C".
    // Defect(s) Found: Dequeue() used ">=" instead of ">" when comparing priorities. Because the
    // search scans from front to back, using ">=" let a later item with an equal priority overwrite
    // an earlier one, so "C" was incorrectly returned before "B", violating the FIFO tie-breaking
    // requirement.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("B", first);
        Assert.AreEqual("C", second);
    }

    [TestMethod]
    // Scenario: Enqueue a few items with varying priorities (not added in priority order) and
    // inspect the queue's contents via ToString() before any Dequeue() call.
    // Expected Result: Items appear in the queue in the exact order they were added ("Low", "High",
    // "Mid"), proving Enqueue() always adds to the back of the queue regardless of priority.
    // Defect(s) Found: None related to Enqueue() - it already added items to the back of the list
    // correctly in all cases tested.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Mid", 5);

        Assert.AreEqual("[Low (Pri:1), High (Pri:10), Mid (Pri:5)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Call Dequeue() on a PriorityQueue that has never had anything added to it.
    // Expected Result: An InvalidOperationException is thrown with the message
    // "The queue is empty."
    // Defect(s) Found: None - this case already worked correctly before any fixes were made.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }
}
