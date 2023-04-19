/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task parent = Task.Run(() =>
            {
                Console.WriteLine("Parent task started.");
            });

            parent.ContinueWith(taskA =>
            {
                Console.WriteLine("Continuation task executed regardless of the result of the parent task.");
                throw new Exception();
            });

            parent.ContinueWith(taskB =>
            {
                Console.WriteLine("Continuation task executed when the parent task was completed without success.");
                throw new Exception();
            }, TaskContinuationOptions.NotOnRanToCompletion);

            parent.ContinueWith(taskC =>
            {
                Console.WriteLine("Continuation task executed when the parent task failed and parent task thread should be reused for continuation.");
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            parent.ContinueWith(taskD =>
            {
                Console.WriteLine("Continuation task executed outside of the thread pool when the parent task is cancelled.");
            }, new CancellationToken(),
            TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning, TaskScheduler.Default);

            Task.WaitAll(parent);

            Console.ReadLine();
        }
    }
}
