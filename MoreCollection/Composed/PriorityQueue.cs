//**********************************************************
//* PriorityQueue                                          *
//* Copyright (c) Julian M Bucknall 2004                   *
//* All rights reserved.                                   *
//* This code can be used in your applications, providing  *
//*    that this copyright comment box remains as-is       *
//**********************************************************
//* .NET priority queue class (heap algorithm)             *
//**********************************************************
//Adapted by David Desmaisons : generic + IComparer + initial size


using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MoreCollection.Infra;

namespace MoreCollection.Composed
{
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private int _Count;
        private int _Capacity;
        private int _Version;
        private readonly IComparer<T> _ItemComparer;
        private T[] _Heap;

        public IComparer<T> ItemComparer => _ItemComparer;

        public PriorityQueue(IComparer<T> iItemComparer = null, int iInitialCapacity = 15) 
        {
            var currentMinimalCapacity = new SecondDegreeSolver(a: 1, b: 1, c: -iInitialCapacity * 2).GetMaxSolution();

            if (!currentMinimalCapacity.HasValue)
                throw new ArgumentException("iInitialCapacity");

            var almost = (int) Math.Truncate(currentMinimalCapacity.Value);
            var adjust = (currentMinimalCapacity.Value % 1 == 0) ? almost : almost + 1;

            _Capacity = ((adjust) * (adjust + 1)) / 2; // 15 is equal to 4 complete levels
            _Heap = new T[_Capacity];
            _ItemComparer = iItemComparer ?? Comparer<T>.Default;
        }

        public T Dequeue() 
        {
            if (_Count == 0)
                throw new InvalidOperationException();

            var result = _Heap[0];
            _Count--;
            TrickleDown(0, _Heap[_Count]);
            _Heap[_Count] = default(T);
            _Version++;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Peek() 
        {
            if (_Count == 0)
                throw new InvalidOperationException();

            return _Heap[0];
        }

        public void Enqueue(T item) 
        {
            if (_Count == _Capacity)
                GrowHeap();
            _Count++;
            BubbleUp(_Count - 1, item);
            _Version++;
        }

        private void BubbleUp(int index, T he) 
        {
            var parent = getParent(index);
            // note: (index > 0) means there is a parent

            while ((index > 0) && (_ItemComparer.Compare(_Heap[parent], he) < 0)) 
            {
                _Heap[index] = _Heap[parent];
                index = parent;
                parent = getParent(index);
            }
            _Heap[index] = he;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetLeftChild(int index)
        {
            return (index * 2) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int getParent(int index) 
        {
            return (index - 1) / 2;
        }

        private void GrowHeap() 
        {
            _Capacity = (_Capacity * 2) + 1;
            var newHeap = new T[_Capacity];
            System.Array.Copy(_Heap, 0, newHeap, 0, _Count);
            _Heap = newHeap;
        }

        private void TrickleDown(int index, T he) {
            int child = GetLeftChild(index);
            while (child < _Count) 
            {
                if (((child + 1) < _Count) && (_ItemComparer.Compare(_Heap[child], _Heap[child + 1]) < 0)) 
                    child++;

                _Heap[index] = _Heap[child];
                index = child;
                child = GetLeftChild(index);
            }
            BubbleUp(index, he);
        }

        #region IEnumerable<T> implementation

        public IEnumerator GetEnumerator() 
        {
            return new PriorityQueueEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new PriorityQueueEnumerator(this);
        }

        #endregion

        public int Count => _Count;

        public void CopyTo(Array array, int index) {
            System.Array.Copy(_Heap, 0, array, index, _Count);
        }

        #region Priority Queue enumerator

        private sealed class PriorityQueueEnumerator : IEnumerator<T> 
        {
            private int _Index;
            private readonly PriorityQueue<T> _PriorityQueue;
            private int _Version;

            public PriorityQueueEnumerator(PriorityQueue<T> priorityQueue) 
            {
                _PriorityQueue = priorityQueue;
                Reset();
            }

            private void CheckVersion() 
            {
                if (_Version != _PriorityQueue._Version)
                    throw new InvalidOperationException();
            }

            #region IEnumerator<T> Members

            public void Reset() 
            {
                _Index = -1;
                _Version = _PriorityQueue._Version;
            }

            public object Current 
            {
                get 
                {
                    CheckVersion();
                    return _PriorityQueue._Heap[_Index];
                }
            }

            public bool MoveNext() 
            {
                CheckVersion();
                if (_Index + 1 == _PriorityQueue._Count)
                    return false;
                _Index++;
                return true;
            }

            T IEnumerator<T>.Current 
            {
                get 
                {
                    CheckVersion();
                    return _PriorityQueue._Heap[_Index];
                }
            }

            public void Dispose() 
            {
            }
            #endregion
        }

        #endregion
    }
}
