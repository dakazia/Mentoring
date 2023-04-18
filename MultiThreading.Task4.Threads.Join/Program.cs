/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(0, 10);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            int state = 10;

            RecursiveThread(state);

            semaphore.Release();
            RecursiveThreadPool(state);


            Console.ReadLine();
        }

        static void RecursiveThread(int state)
        {
            Console.WriteLine($"Recursive Thread {Thread.CurrentThread.ManagedThreadId} with state {state}");

            if (state > 0)
            {
                Thread thread = new Thread(() => RecursiveThread(state - 1));
                thread.Start();
                thread.Join();
            }
        }

        static void RecursiveThreadPool(int state)
        {
            

            Console.WriteLine($"Recursive Thread Pool {Thread.CurrentThread.ManagedThreadId} with state {state}");

            if (state > 0)
            {
                ThreadPool.QueueUserWorkItem(_ => RecursiveThreadPool(state - 1));
            }

            semaphore.Release();

            semaphore.WaitOne();
        }
    }
}
