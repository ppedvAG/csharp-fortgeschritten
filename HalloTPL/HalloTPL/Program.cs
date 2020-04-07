using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo TPL");

            //Parallel.For(0, 10000000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i:000}"));
            //Parallel.Invoke(Zähle, () =>  Console.WriteLine("HALLO"), Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);

            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);

                throw new AccessViolationException();

                Console.WriteLine("T1 fertig");
            });

            t1.ContinueWith(t => { Console.WriteLine("T1 continue"); });
            t1.ContinueWith(t => { Console.WriteLine("T1 ok"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.ContinueWith(t => { Console.WriteLine($"T1 err: {t.Exception.InnerException.Message}"); }, TaskContinuationOptions.OnlyOnFaulted);

            Task<long> t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(600);
                Console.WriteLine("T2 fertig");
                return 3456789;
            });

            t1.Start();
            t2.Start();

            //t2.Wait();
            Console.WriteLine($"T2 Result: {t2.Result}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i:000}");
                //Console.WriteLine(string.Format("{0}: {1:000}", Thread.CurrentThread.ManagedThreadId, i));

                //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString() + i.ToString());

            }
        }
    }
}
