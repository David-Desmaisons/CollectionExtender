using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MoreCollection.Composed;
using MoreCollection.Extensions;
using Xunit;

namespace MoreCollectionTest.Composed 
{

    public class PriorityQueueTest 
    {
      
        private class CompareMyObject : IComparer<MyObject> 
        {
            public int Compare(MyObject x, MyObject y)
            {
                return x.Value - y.Value;
            }
        }

        private readonly PriorityQueue<MyObject> _PriorityQueue;

        public PriorityQueueTest() 
        {
            _PriorityQueue = new PriorityQueue<MyObject>();
        }

        [Fact]
        public void TestComparer() 
        {
            _PriorityQueue.ItemComparer.Should().Be(Comparer<MyObject>.Default);
        }

        [Fact]
        public void TestEnqueueing() 
        {
            _PriorityQueue.Count.Should().Be(0, "Enqueuing first item should set count to 1");

            _PriorityQueue.Enqueue(new MyObject("item one", 12));
            _PriorityQueue.Count.Should().Be(1, "Enqueuing first item should set count to 1");

            _PriorityQueue.Enqueue(new MyObject("item two", 5));
            _PriorityQueue.Count.Should().Be(2, "Enqueuing second item should set count to 2");

            _PriorityQueue.Enqueue(new MyObject("item three", 5));
            _PriorityQueue.Count.Should().Be(3, "Enqueuing third item should set count to 3");
        }


        [Fact]
        public void TestPeek_Exception() 
        {
            Action ac = () => _PriorityQueue.Peek();
            ac.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void TestPeek() 
        {
            var io = new MyObject("item one", 12);
            var it = new MyObject("item two", 5);
            _PriorityQueue.Enqueue(io);
            _PriorityQueue.Enqueue(it);

            MyObject s = _PriorityQueue.Peek();
            s.Should().Be(it, "Peeking should retrieve second item");
            _PriorityQueue.Count.Should().Be(2, "Peek item should set count to 2");

            s = _PriorityQueue.Dequeue();
            s.Should().Be(it, "Dequeuing should retrieve second item");
            _PriorityQueue.Count.Should().Be(1, "Dequeuing item should set count to 1");

            MyObject s2 = _PriorityQueue.Peek();
            s2.Should().Be(io, "Dequeuing should retrieve first item");
            _PriorityQueue.Count.Should().Be(1, "Dequeuing item should set count to 0");

            s2 = _PriorityQueue.Dequeue();
            s2.Should().Be(io, "Dequeuing should retrieve first item");
            _PriorityQueue.Count.Should().Be(0, "Dequeuing item should set count to 0");

            Action ac = () => _PriorityQueue.Dequeue();
            ac.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void TestDequeueing() 
        {
            var io = new MyObject("item one", 12);
            var it = new MyObject("item two", 5);
            _PriorityQueue.Enqueue(io);
            _PriorityQueue.Enqueue(it);

            var s = _PriorityQueue.Dequeue();
            s.Should().Be(it, "Dequeuing should retrieve second item");
            _PriorityQueue.Count.Should().Be(1, "Dequeuing item should set count to 1");

            var s2 = _PriorityQueue.Dequeue();
            s2.Should().Be(io, "Dequeuing should retrieve first item");
            _PriorityQueue.Count.Should().Be(0, "Dequeuing item should set count to 0");

            Action ac = () => _PriorityQueue.Dequeue();
            ac.ShouldThrow<InvalidOperationException>();

        }

        [Fact]
        public void TestGrowingQueue() 
        {
            var pqlocal = new PriorityQueue<MyObject>(new CompareMyObject());
            for (int i = 0; i < 15; i++) {
                pqlocal.Enqueue(new MyObject("item: " + i, i * 2));
            }

            pqlocal.Count.Should().Be(15, "Enqueued 15 items, so there should be 15 there");

            pqlocal.Enqueue(new MyObject("trigger", 15));
            pqlocal.Count.Should().Be(16, "Enqueued 15 items, so there should be 15 there");

            MyObject found = null;


            for (int i = 14; i > 7; i--) 
            {
                found = pqlocal.Dequeue();
                string expectedStr = "item: " + i;
                found.Name.Should().Be(expectedStr, "Dequeueing problem");
            }

            found = pqlocal.Dequeue();
            found.Name.Should().Be("trigger", "Dequeueing problem");

            for (int i = 7; i >= 0; i--) 
            {
                found = pqlocal.Dequeue();
                string expectedStr = "item: " + i.ToString();
                found.Name.Should().Be(expectedStr, "Dequeueing problem");
            }
        }

        [Fact]
        public void TestGrowingQueue2() 
        {
            var pqlocal = new PriorityQueue<MyObject>(new CompareMyObject(), 2);
            for (int i = 0; i < 15; i++) 
            {
                pqlocal.Enqueue(new MyObject("item: " + i.ToString(), i * 2));
            }

            pqlocal.Count.Should().Be(15, "Enqueued 15 items, so there should be 15 there");

            pqlocal.Enqueue(new MyObject("trigger", 15));
            pqlocal.Count.Should().Be(16, "Enqueued 15 items, so there should be 15 there");

            MyObject found = null;

            for (int i = 14; i > 7; i--) 
            {
                found = pqlocal.Dequeue();
                string expectedStr = "item: " + i.ToString();
                found.Name.Should().Be(expectedStr, "Dequeueing problem");
            }

            found = pqlocal.Dequeue();
            found.Name.Should().Be("trigger", "Dequeueing problem");

            for (int i = 7; i >= 0; i--)
            {
                found = pqlocal.Dequeue();
                string expectedStr = "item: " + i.ToString();
                found.Name.Should().Be(expectedStr, "Dequeueing problem");
            }
        }

        [Fact]
        public void TestWrongCapacity() 
        {
            PriorityQueue<MyObject> pqlocal = null;
            Action ac = () => pqlocal = new PriorityQueue<MyObject>(null, -1);

            ac.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TestEnumerator() {
            var cp = new CompareMyObject();
            var pqlocal = new PriorityQueue<MyObject>(cp);

            pqlocal.ItemComparer.Should().Be(cp);
            //string s;
            // use a hashtable to check contents of PQ
            var ht = new HashSet<MyObject>();
            for (int i = 0; i < 5; i++) {
                var ob = new MyObject("item: " + i.ToString(), i * 2);

                ht.Add(ob);
                pqlocal.Enqueue(ob);
            }

            pqlocal.Should().BeEquivalentTo(ht, "Enumerable PriorityQueue");
        }

        [Fact]
        public void TestEnumeratorWithEnqueue() 
        {
            var f = new MyObject("one", 42);
            _PriorityQueue.Enqueue(f);
            var ie = _PriorityQueue.GetEnumerator();
            ie.MoveNext();
            ie.Current.Should().Be(f);
            _PriorityQueue.Enqueue(new MyObject("one", 42));

            Action wf = () => ie.MoveNext();                 // should fail
            wf.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void TestCopyTo()
        {
            var ht = new HashSet<MyObject>();
            for (int i = 0; i < 5; i++)
            {
                var ob = new MyObject("item: " + i.ToString(), i * 2);
                ht.Add(ob);
                _PriorityQueue.Enqueue(ob);
            }

            var heArray = new MyObject[6];

            _PriorityQueue.CopyTo(heArray, 1);

            heArray.Skip(1).Should().BeEquivalentTo(_PriorityQueue);
        }

        [Fact]
        public void TestPriorityType() 
        {
            var pqlocal = new PriorityQueue<MyObject>(new CompareMyObject().Revert());
            var io = new MyObject("item one", 12);
            var it = new MyObject("item two", 5);
            pqlocal.Enqueue(io);
            pqlocal.Enqueue(it);

            var s = pqlocal.Dequeue();
            s.Should().Be(it, "Dequeuing should retrieve highest priority item");
            pqlocal.Count.Should().Be(1, "Dequeuing item should set count to 1");

            s = pqlocal.Dequeue();
            s.Should().Be(io, "Dequeuing should retrieve highest priority item");
            pqlocal.Count.Should().Be(0, "Dequeuing item should set count to 0");
        }

        [Fact]
        public void TestPriorityType2() 
        {
            var pqlocal = new PriorityQueue<MyObject>(new CompareMyObject());
            var io = new MyObject("item one", 12);
            var it = new MyObject("item two", 5);
            pqlocal.Enqueue(io);
            pqlocal.Enqueue(it);

            var s = pqlocal.Dequeue();
            s.Should().Be(io, "Dequeuing should retrieve highest priority item");

            pqlocal.Count.Should().Be(1, "Dequeuing item should set count to 1");

            s = pqlocal.Dequeue();
            s.Should().Be(it, "Dequeuing should retrieve highest priority item");
            pqlocal.Count.Should().Be(0, "Dequeuing item should set count to 0");
        }

        [Fact]
        public void GCTesting() 
        {
            var pqlocal = new PriorityQueue<MyObject>(new CompareMyObject());
            var io = new MyObject("item one", 12);
            var mio = new WeakReference<MyObject>(io);

            MyObject res = null;
            mio.TryGetTarget(out res).Should().BeTrue();
            res.Should().Be(io);
            io = null;

            {
                pqlocal.Enqueue(io);
                res = pqlocal.Dequeue();
                res = null;
            }

            pqlocal.Count.Should().Be(0);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            res = null;
            mio.TryGetTarget(out res).Should().BeFalse();
            res.Should().BeNull();
        }
    }
}