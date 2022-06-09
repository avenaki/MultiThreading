/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static List<int> _numbers = new List<int>();
        static ManualResetEvent _readResetEvent = new ManualResetEvent(false);
        static ManualResetEvent _writeResetEvent = new ManualResetEvent(true);


        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            var readTask = Task.Factory.StartNew(Read);
            var writeTask = Task.Factory.StartNew(Write);

            Console.ReadLine();
        }

        static void Write()
        {
            for (int i = 0; i < 10; i++)
            {
                _writeResetEvent.WaitOne();
                _readResetEvent.Reset();

                _numbers.Add(i);

                _writeResetEvent.Reset();
                Thread.Sleep(100);
                _readResetEvent.Set();
            }
        }

        static void Read()
        {
            while (_numbers.Count < 10)
            {
                _readResetEvent.WaitOne();
                _writeResetEvent.Reset();

                Console.Write($"Total numbers: {_numbers.Count}. Current numbers: ");
                for (int i = 0; i < _numbers.Count; i++)
                {
                    Console.Write($"{_numbers[i]} ");
                }

                Console.WriteLine();
                _readResetEvent.Reset();

                _writeResetEvent.Set();
            }
        }
    }
}
