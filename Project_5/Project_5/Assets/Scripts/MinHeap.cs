using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class MinHeap<T>
    {
        public T[] Data { get; set; }
        private int CurrentWrite;
        private int Root;
        private Func<T, T, bool> Comparator;

        public MinHeap(int size, Func<T, T, bool> Comparator)
        {
            Data = new T[size];
            CurrentWrite = 0;
            Root = 0;
            this.Comparator = Comparator;
        }

        public void Add(T element)
        {
            Data[CurrentWrite] = element;

            if (Data.Length == 1)
            {
                CurrentWrite++;
                return;
            }

            MinHeapify(CurrentWrite);
            CurrentWrite++;
        }

        public T GetRoot()
        {
            if (Data.Length == 0)
                throw new ArgumentOutOfRangeException("No elements in MinHeap");

            return Data[Root];
        }

        /*
         *  Propogate the element till you get to top or
         *  parent element that is less than the current element
         *  
         *  Propogate down until you get to bottom or child element
         *  that is greater than your current element
         */
        private void MinHeapify(int element)
        {
            bool upHeafied = false, downHeapified = false;

            while (!upHeafied)
            {
                upHeafied = PropogateUp(ref element);
            }

            while (!downHeapified)
            {
                downHeapified = PropogateDown(ref element);
            }
        }

        private bool PropogateUp(ref int element)
        {
            int parent = GetParent(element);

            if (element == parent)
                return true;

            if (Comparator(Data[element], Data[parent]))
            {
                T tmp = Data[element];
                Data[element] = Data[parent];
                Data[parent] = tmp;
                element = parent;

                return false;
            }

            return true;
        }

        private bool PropogateDown(ref int element)
        {
            int leftChild = GetLeftChild(element), rightChild = GetRightChild(element);

            if (leftChild < 0)
            {
                return true;
            }
            else if (rightChild < 0)
            {
                if (Comparator(Data[leftChild], Data[element]))
                {
                    T tmp = Data[leftChild];
                    Data[leftChild] = Data[element];
                    Data[element] = tmp;
                }

                return true;
            }
            else
            {
                if (Comparator(Data[leftChild], Data[element]))
                {
                    T tmp = Data[leftChild];
                    Data[leftChild] = Data[element];
                    Data[element] = tmp;

                    return false;
                }
                else if (Comparator(Data[rightChild], Data[element]))
                {
                    T tmp = Data[rightChild];
                    Data[rightChild] = Data[element];
                    Data[element] = tmp;

                    return false;
                }
                else
                    return true;
            }
        }

        public int GetLeftChild(int i) { return i == Data.Length - 1 ? -1 : i * 2; }

        public int GetRightChild(int i) { return i == Data.Length - 1 ? -1 : (i * 2) + 1; }

        public int GetParent(int i) { return i == 0 ? i : (int)Math.Floor((double)(i / 2)); }

    }
}
