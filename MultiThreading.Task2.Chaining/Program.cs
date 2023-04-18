/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {

        const int IntegerAmount = 10;
        static Random Random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task<int[]> task = Task.Run(() =>
            {
                return CreateArray();
            });

            task.ContinueWith(t => 
            {
                return MultiplyArray(t.Result);
            });

            task.ContinueWith(t =>
            {
                return SortArray(t.Result);
            });

            task.ContinueWith(t =>
            {
                return CalculateAverage(t.Result);
            });

            Console.ReadLine();
        }

        static int[] CreateArray()
        {
            var arr = new int[IntegerAmount];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Random.Next();                
            }

            Console.WriteLine($"Array: {PrintArray(arr)}");

            return arr;
        }

        static int[] MultiplyArray(int[] arr) 
        {
            var randomInteger = Random.Next(1, 10);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] *= randomInteger;
            }

            Console.WriteLine($"Multiply Array: {PrintArray(arr)}");

            return arr;
        }

        static int[] SortArray(int[] arr)
        {
            Array.Sort(arr);

            Console.WriteLine($"Sort Array: {PrintArray(arr)}");

            return arr;
        }

        static double CalculateAverage(int[] arr)
        {
            var avr = arr.Average();

            Console.WriteLine($"Calculate Average of the Array: {avr}");

            return avr;
        }

        static string PrintArray (int[] arr)
        {
            return string.Join(", ", arr.Select(_ => _.ToString()).ToArray());
        }
    }
}
