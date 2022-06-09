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
        private static int number = 10;
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        private static bool _quit = false;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code

            Console.WriteLine("Choose a or b option. Or enter q to quit.");
            var option = Console.ReadLine().ToLower();

            switch (option)
            {
                case "a":
                    Console.WriteLine("Option a was chosen");
                    var newThread = new Thread(() => CreateThreadsA());
                    newThread.Start();
                    break;

                case "b":
                    Console.WriteLine("Option b was chosen");
                    ThreadPool.QueueUserWorkItem(CreateThreadsB);
                    break;

                case "q":
                    _quit = true;
                    break;

                default:
                    Console.WriteLine("No such options.");
                    break;
            }

            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        static void CreateThreadsB(object state)
        {
            _semaphoreSlim.Wait();
            if (number > 0)
            {
                number--;
                Console.WriteLine($"Option B. Number is {number}.");
                var newThread = ThreadPool.QueueUserWorkItem(CreateThreadsB);
            }
            _semaphoreSlim.Release();

        }

        public static void CreateThreadsA()
        {
            if (number > 0)
            {
                number--;
                Console.WriteLine($"Option A. Number is {number}.");
                var newThread = new Thread(() => CreateThreadsA());
                newThread.Start();
                newThread.Join();
            }

        }
    }
}
