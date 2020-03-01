using System;
using System.Collections.Generic;

namespace PriorityQTest // Made a circular array priority queue that is a class
{
    class PriorityQ
    {
        string[] priorityQArray;
        int currentSize = 0;
        int maxSize;
        int rearPointer = 0;
        int frontPointer = 0;


        // Initialising
        public PriorityQ(int size)
        {
            priorityQArray = new string[size];
            maxSize = size;
        }
        // Is Q empty
        private bool IsEmpty()
        {
            if (currentSize == 0) { return true; }
            return false;
        }
        // Is Q full
        private bool IsFull()
        {
            if (currentSize == maxSize) { return true; }
            return false;
        }
        // Enqueue item
        public void EnQ(string item)
        {
            if (IsFull()) { Console.WriteLine("\nCan't enqueue an item, priority queue is full."); }
            else
            {
                priorityQArray[rearPointer] = item;
                currentSize += 1;
                rearPointer = (rearPointer + 1) % maxSize;

                if (currentSize > 1)
                {
                    string[] unsortedArray = new string[currentSize];
                    string[] sortedArray = new string[currentSize];

                    int addCount = 0;
                    int indexPointer = frontPointer;

                    while (addCount < currentSize)
                    {
                        unsortedArray[addCount] = priorityQArray[indexPointer];
                        indexPointer = (indexPointer + 1) % maxSize;
                        addCount += 1;
                    }
                    sortedArray = MergeSort(unsortedArray);

                    addCount = 0;
                    indexPointer = frontPointer;
                    while (addCount < currentSize)
                    {
                        priorityQArray[indexPointer] = sortedArray[addCount];
                        indexPointer = (indexPointer + 1) % maxSize;
                        addCount += 1;
                    }
                }

                //PrintQ();
            }
        }
        // Dequeue item
        public string DeQ()
        {
            if (IsEmpty()) { Console.WriteLine("\nCan't dequeue an item, priority queue is full"); }
            else
            {
                currentSize -= 1;
                int tempFrontPointer = frontPointer;
                frontPointer = (frontPointer + 1) % maxSize;
                return priorityQArray[tempFrontPointer];
            }
            return null;
        }
        // Whole merge sort recursive method
        private static string[] MergeSort(string[] unsortedArrayMain)
        {
            int unsortedArrayMainLength = unsortedArrayMain.Length;
            string[] sortedArray = new string[unsortedArrayMainLength];

            if (unsortedArrayMainLength < 2) { return unsortedArrayMain; } // Base case

            int midpoint = unsortedArrayMainLength / 2;
            string[] subArray1 = new string[midpoint];
            string[] subArray2;
            if (unsortedArrayMainLength % 2 == 0) { subArray2 = new string[midpoint]; }
            else { subArray2 = new string[midpoint + 1]; }

            for (int count = 0; count < midpoint; count++) { subArray1[count] = unsortedArrayMain[count]; }
            int subArray2Count = 0;
            for (int count = midpoint; count < unsortedArrayMainLength; count++) { subArray2[subArray2Count] = unsortedArrayMain[count]; subArray2Count += 1; }

            //Recursive sort subArray1
            subArray1 = MergeSort(subArray1);

            //Recursive sort subArray2
            subArray2 = MergeSort(subArray2);

            //Merging the sorted arrays
            sortedArray = Merge(subArray1, subArray2);

            return sortedArray;
        }
        // Merge 2 subArrays
        private static string[] Merge(string[] subArray1, string[] subArray2)
        {
            int subArray1Length = subArray1.Length;
            int subArray2Length = subArray2.Length;
            int numElements = subArray1Length + subArray2Length;

            string[] mergedArray = new string[numElements];
            int mergeArrayCount = 0;

            int subArray1Count = 0;
            int subArray2Count = 0;

            while (subArray1Length > subArray1Count || subArray2Length > subArray2Count)
            {
                if (subArray1Length > subArray1Count && subArray2Length > subArray2Count)
                {
                    // Perform check of A B C
                    if (subArray1[subArray1Count][0] < subArray2[subArray2Count][0])
                    {
                        mergedArray[mergeArrayCount] = subArray1[subArray1Count];
                        subArray1Count += 1;
                    }
                    else if (subArray1[subArray1Count][0] > subArray2[subArray2Count][0])
                    {
                        mergedArray[mergeArrayCount] = subArray2[subArray2Count];
                        subArray2Count += 1;
                    }
                    else
                    {
                        // Performs check of the randomNumber
                        int randomNumber1 = (subArray1[subArray1Count][1] - '0') * 10 + (subArray1[subArray1Count][2] - '0');
                        int randomNumber2 = (subArray2[subArray2Count][1] - '0') * 10 + (subArray2[subArray2Count][2] - '0');
                        //int randomNumber1 = int.Parse(subArray1[subArray1Count][1].ToString()) * 10 + int.Parse(subArray1[subArray1Count][2].ToString()) ;
                        //int randomNumber2 = int.Parse(subArray2[subArray2Count][1].ToString()) * 10 + int.Parse(subArray2[subArray2Count][2].ToString()) ;

                        if (randomNumber1 <= randomNumber2)
                        {
                            mergedArray[mergeArrayCount] = subArray1[subArray1Count];
                            subArray1Count += 1;
                        }
                        else
                        {
                            mergedArray[mergeArrayCount] = subArray2[subArray2Count];
                            subArray2Count += 1;
                        }
                    }
                    mergeArrayCount += 1;
                }
                else if (subArray1Length > subArray1Count) // Only elements in sublist1 remain
                {
                    while (mergeArrayCount < numElements)
                    {
                        mergedArray[mergeArrayCount] = subArray1[subArray1Count];
                        subArray1Count += 1;
                        mergeArrayCount += 1;
                    }
                }
                else // Only elements in sublist2 remain
                {
                    while (mergeArrayCount < numElements)
                    {
                        mergedArray[mergeArrayCount] = subArray2[subArray2Count];
                        subArray2Count += 1;
                        mergeArrayCount += 1;
                    }
                }
            }
            return mergedArray;
        }
        // Output Q
        public void PrintQ()
        {
            int printPointer = frontPointer;
            Console.WriteLine("\n-------------------------------------\nSorted:\n");
            for (int count = 0; count < currentSize; count++)
            {
                Console.WriteLine(priorityQArray[printPointer]);
                printPointer = (printPointer + 1) % maxSize;
            }
            Console.WriteLine("-------------------------------------\n");
        }
    }
    class Program
    {
        public static string[] PriorityAssignment(string[] contentsOfArray)
        {
            int contentsOfArrayLength = contentsOfArray.Length;
            string[] priorityAssignedArray = new string[contentsOfArrayLength];
            char[] priorityChar = { 'A', 'B', 'C' };

            Random random = new Random();
            for (int count = 0; count < contentsOfArrayLength; count++)
            {
                int randomPriorityChar = random.Next(0, 3);
                int randomPriorityNum = random.Next(10, 51);

                priorityAssignedArray[count] = priorityChar[randomPriorityChar] + randomPriorityNum.ToString() + " " + contentsOfArray[count];
            }
            return priorityAssignedArray;
        }

        static void Main()
        {
            string[] placesArray = { "Old Kent Road", "Whitechapel Road", "King's Cross station", "The Angel", "Islington", "Euston Road", "Pentonville Road", "Pall Mall", "Whitehall", "Northumberland Avenue", "Marylebone station", "Bow Street", "Great Marlborough Street", "Vine Street", "Strand", "Fleet Street", "Trafalgar Square", "Fenchurch Street station", "Leicester Square", "Coventry Street", "Piccadilly", "Regent Street", "Oxford Street", "Bond Street", "Liverpool Street station", "Park Lane", "Mayfair" };

            string[] priorityBusRoutes = PriorityAssignment(placesArray);
            int priorityBusRoutesLength = priorityBusRoutes.Length;

            PriorityQ busQ = new PriorityQ(priorityBusRoutesLength);
            Random random = new Random();
            List<int> alreadyAddedBusRoutes = new List<int>();
            bool present;
            int busIndex;
            for (int count = 0; count < priorityBusRoutesLength; count++)
            {
                do
                {
                    busIndex = random.Next(0, priorityBusRoutesLength);
                    present = false;
                    foreach (int element in alreadyAddedBusRoutes) { if (element == busIndex) { present = true; break; } }
                } while (present);
                alreadyAddedBusRoutes.Add(busIndex);
                busQ.EnQ(priorityBusRoutes[busIndex]);
            }
            Console.WriteLine("\n-------------------------------------\nUnsorted:\n");
            foreach(string element in priorityBusRoutes) { Console.WriteLine(element); }
            Console.WriteLine("-------------------------------------\n");

            busQ.PrintQ();
            Console.ReadLine();
        }
    }
}