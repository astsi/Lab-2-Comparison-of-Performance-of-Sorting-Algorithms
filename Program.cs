using System;

namespace PAA_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithms a = new Algorithms();


            /*

            int[] array10K = a.CreateAnArray(10000);
            int[] array1M = a.CreateAnArray(1000000);
            int[] array10M = a.CreateAnArray(10000000);


            a.InsertionSort(array10K);
            a.BucketSort(array10K);
            a.QuickSort(array10K, 0, array10K.Length - 1);

            a.BucketSort(array1M);
            a.QuickSort(array1M, 0, array1M.Length - 1);

            a.BucketSort(array10M);
            a.QuickSort(array10M, 0, array10M.Length - 1);


          */

            a.AlgorithmsDiagnostics();



        }
    }
}

