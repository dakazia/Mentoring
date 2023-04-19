/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static object lockObj = new object();
        static List<int> sharedList = new List<int>();
        static bool addFinished = false;

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Thread addThread = new Thread(AddItems);
            Thread printThread = new Thread(PrintItems);

            addThread.Start();
            printThread.Start();

            addThread.Join();
            printThread.Join();

            Console.ReadLine();
        }

        static void AddItems()
        {
            lock (lockObj)
            {
                for (int i = 1; i <= 10; i++)
                {
                    sharedList.Add(i);
                    Console.WriteLine($"Item {i} added to collection.");
                    Monitor.Pulse(lockObj);
                    if (i != 10)
                        Monitor.Wait(lockObj);
                }
                addFinished = true;
                Monitor.Pulse(lockObj);
            }
        }

        static void PrintItems()
        {
            lock (lockObj)
            {
                while (!addFinished || sharedList.Count > 0)
                {
                    Monitor.Wait(lockObj);
                    Console.Write("[");
                    for (int i = 0; i < sharedList.Count; i++)
                    {
                        Console.Write(sharedList[i]);
                        if (i < sharedList.Count - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine("]");
                    Monitor.Pulse(lockObj);
                }
            }
        }
    }
}
