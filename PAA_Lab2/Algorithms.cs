using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PAA_Lab2
{
    class Algorithms
    {
        //Prints out the statistics of algorithms applied on the random array of the given size
        public void AlgorithmsDiagnostics()
        {
            int size = 100;
            for (int i = 0; i < 6; i++)
            {
                RunDiagnostics(size);
                size = size*10;
            }
        }

        public void QuickDiagnostics(int[] array)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            QuickSort(array, 0, array.Length - 1);
            s.Stop();
            Console.WriteLine("Quick sort for array of size: " + array.Length + " is executed in: " + s.Elapsed);
        }


        public void InsertionDiagnostics(int[] array)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            int[] tempArr = InsertionSort(array);
            s.Stop();
            Console.WriteLine("Insertion sort for array of size: " + array.Length + " is executed in: " + s.Elapsed);
        }


        public void BucketDiagnostics(int[] array)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            int[] tempArr = BucketSort(array);
            s.Stop();
            Console.WriteLine("Bucket sort for array of size: " + array.Length + " is executed in: " + s.Elapsed);
        }


        public void RunDiagnostics(int size)
        {
            Process currentProcess;// = System.Diagnostics.Process.GetCurrentProcess();
            long totalBytesOfMemoryUsedInsertion = long.MaxValue;
            long totalBytesOfMemoryUsedBucket;
            long totalBytesOfMemoryUsedQuick;
            int timeInsertion = int.MaxValue;
            int timeBucket;
            int timeQuick;

            // = currentProcess.WorkingSet64;

            Stopwatch s = new Stopwatch();
            int[] rndArr = CreateAnArray(size);
            int[] sortedArr = new int[size];

            Console.WriteLine("Size of array: " + size);

            if(size < 100000)
            {
                s.Start();
                sortedArr = InsertionSort(rndArr);
                currentProcess = Process.GetCurrentProcess();
                totalBytesOfMemoryUsedInsertion = currentProcess.WorkingSet64;
                Console.WriteLine("Current Process status: " + totalBytesOfMemoryUsedInsertion);
                s.Stop();
                timeInsertion = s.Elapsed.Milliseconds;
                Console.WriteLine("Insertion sort: " + s.Elapsed);
            }

            s.Restart();
            sortedArr = BucketSort(rndArr);
            currentProcess = Process.GetCurrentProcess();
            totalBytesOfMemoryUsedBucket = currentProcess.WorkingSet64;
            Console.WriteLine("Current Process status: " + totalBytesOfMemoryUsedBucket);
            s.Stop();
            timeBucket = s.Elapsed.Milliseconds;
            Console.WriteLine("Bucket sort: " + s.Elapsed);

            s.Restart();
            QuickSort(rndArr, 0, size - 1);
            currentProcess = Process.GetCurrentProcess();
            totalBytesOfMemoryUsedQuick = currentProcess.WorkingSet64;
            Console.WriteLine("Current Process status: " + totalBytesOfMemoryUsedQuick);
            s.Stop();
            timeQuick = s.Elapsed.Milliseconds;
            Console.WriteLine("Quick sort: " + s.Elapsed);
            

            
            long minMemUsage = Math.Min(totalBytesOfMemoryUsedQuick, totalBytesOfMemoryUsedBucket);
            minMemUsage = Math.Min(minMemUsage, totalBytesOfMemoryUsedInsertion);

            if (minMemUsage == totalBytesOfMemoryUsedBucket)
                Console.WriteLine("Minimal memory consumption: Bucket sort : " + totalBytesOfMemoryUsedBucket + " bytes.");
            else if (minMemUsage == totalBytesOfMemoryUsedInsertion)
                Console.WriteLine("Minimal memory consumption: Insertion sort " + totalBytesOfMemoryUsedInsertion + " bytes.");
            else Console.WriteLine("Minimal memory consumption: Bucket sort " + totalBytesOfMemoryUsedInsertion + " bytes.");
         
           

            Console.WriteLine();


        }
        
       //Generating a Random array
       public int[] CreateAnArray(int size)
        {
            int[] retArray = new int[size];

            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                retArray[i] = rnd.Next(0, 10000);
            }

            return retArray;

        }

        //Swap function
        public void Swap(ref int value1, ref int value2)
        {
            int temp = value1;
            value1 = value2;
            value2 = temp;
        }

        //Insertion sort
        public int[] InsertionSort(int[] array)
        {
            int j;

            for (int i = 1; i < array.Length; i++)
            {
                j = i - 1;

                while (j >= 0)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j],ref array[j + 1]);
                        j--;
                    }
                    else
                    {
                        j = -1;
                    }
                }
            }

            return array;

        }

        //Quick sort
        public void QuickSort(int[] array, int start, int end)
        {
            int min = start, max = end, pivot = array[(start + end) / 2], swap;

            while (min <= max)
            {
                while (array[min] < pivot) min++;
                while (array[max] > pivot) max--;
                if (min > max) break;

                swap = array[min];
                array[min] = array[max];
                array[max] = swap;
                min++; max--;
            }
            if (start < max) QuickSort(array, start, max);
            if (min < end) QuickSort(array, min, end);
        }

        //Bucket sort
        public int[] BucketSort(int[] array)
        {
            int minValue = array[0];
            int maxValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                    maxValue = array[i];
                if (array[i] < minValue)
                    minValue = array[i];
            }

            int numOfBuckets = maxValue - minValue + 1;

            List<int> sortedArray = new List<int>();
            List<int>[] buckets = new List<int>[numOfBuckets];
            for (int i = 0; i < numOfBuckets; i++)
            {
                buckets[i] = new List<int>();
            }
            //rasporedjivanje promenljivih u buckete
            
            for (int i = 0; i < array.Length; i++)
            {
                int bucket = (array[i] - minValue);
                buckets[bucket].Add(array[i]);
            }
            //primena insertion sorta za sortiranje elemenata unutar bucketa

            for (int i = 0; i < numOfBuckets; i++)
            {
                int[] temparr = buckets[i].ToArray();
                int[] temp = InsertionSort(temparr);
                sortedArray.AddRange(temp);
            }
            return sortedArray.ToArray();
        }
    }

 }

