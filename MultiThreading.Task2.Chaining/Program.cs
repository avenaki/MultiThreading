/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        public static Random Random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            Task<int[]> firstTask = Task.Run(() => GenerateRandomNumbersArray());
            Task<int[]> secondTask = firstTask.ContinueWith(x => MultiplyArrayByRandomNumber(firstTask.Result));
            Task<int[]> thirdTask = secondTask.ContinueWith(x => SortArray(secondTask.Result));
            Task<string> fourthTask = thirdTask.ContinueWith(x => CalculateAverageValue(thirdTask.Result));
            Console.WriteLine(fourthTask.Result);
            Console.ReadLine();
        }

        static int[] GenerateRandomNumbersArray()
        {
            var array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = Random.Next(0, 100);
                Console.WriteLine($"First task generated number:{i+1} it's value {array[i]}");
            }

            return array;
        }

        static int[] MultiplyArrayByRandomNumber(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int multiplier = Random.Next(0,50);
                array[i] = array[i] * multiplier;
                Console.WriteLine($"Second task multiplied {i+1} number by value {multiplier}. Result: {array[i]}");
            }

            return array;
        }

        static int[] SortArray(int[] array)
        {
            var sortedArray = array.OrderBy(n => n).ToArray();

            foreach(int item in sortedArray)
            {
                Console.WriteLine($"Third task sorted array:{item}.");
            }

            return sortedArray;
        }

        static string CalculateAverageValue(int[] array)
        {
            var average = array.Average();
            return $"Fourth task calculated average value:{average}";
        }

    }
}
