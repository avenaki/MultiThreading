/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

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


            // feel free to add your code

            while (true)
            {
                Console.WriteLine("Choose option to demonstrate.");
                var option = Console.ReadLine().ToLower();

                switch (option)
                {
                    case "a":
                        OptionA();
                        break;

                    case "b":
                        OptionB();
                        break;

                    case "c":
                        OptionC();
                        break;

                    case "d":
                        OptionD();
                        break;

                    default:
                        Console.WriteLine("Sorry, there is no such option. Choose between a - d.");
                        break;
                }

            }

        }

        static void OptionA()
        {
            Task<int> parentTask = Task.Run(() => Execute("a"));
            Task continuation = parentTask.ContinueWith(x => ExecuteContinuation(), TaskContinuationOptions.None);
        }

        static void OptionB()
        {
            Task<int> parentTask = Task.Run(() => Execute("b"));
            Task continuation = parentTask.ContinueWith(x => ExecuteContinuation(), TaskContinuationOptions.NotOnRanToCompletion);
        }

        static void OptionC()
        {
            Task<int> parentTask = Task.Run(() => Execute("c"));
            Task continuation = parentTask.ContinueWith(x => ExecuteContinuation(), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
        }

        static void OptionD()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            tokenSource.Cancel();
            var scheduler = TaskScheduler.Default;
            Task<int> parentTask = Task.Run(() => Execute("d"), token);
            Task continuation = parentTask.ContinueWith(x => ExecuteContinuation(), new CancellationToken(),TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning, scheduler);
        }

        static int Execute(string option)
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
            switch (option)
            {
                case "a":
                    Console.WriteLine("Parent task is going to throw an exception");
                    throw new Exception("This exception is expected!");
                case "b":
                    Console.WriteLine("Parent task is going to throw an exception");
                    throw new Exception("This exception is expected!");

                case "c":
                    Console.WriteLine("Parent task is going to throw an exception");
                    throw new Exception("This exception is expected!");

                case "d":
                    
                    Console.WriteLine("Parent task is going to be cancelled.");
                    Task.FromCanceled(new CancellationToken());
                    break;
            }

            Thread.Sleep(2000);
            Console.WriteLine("Parent task executed.");
            return 1;
        }

        static void ExecuteContinuation()
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Continuation task executed.");
        }
    }
}
