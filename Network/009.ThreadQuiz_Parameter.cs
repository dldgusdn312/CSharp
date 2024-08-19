using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadQuiz02
{
    class Calculator
    {
        public void Plus()
        {
            int a = 500;
            int b = 200;
            Console.WriteLine(a + b);
        }

        //2단계
        public void Plus2(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        public void Plus3(object obj)
        {
            Value z = (Value)obj;
            Console.WriteLine(z.A + z.B);
        }
    }
    class Value
    {
        public int A { get; set; }
        public int B { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Thread t = new Thread(new ThreadStart(calculator.Plus));
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread( () => calculator.Plus2(500, 200) );
            t2.IsBackground = true;
            t2.Start();

            Value v = new Value();
            v.A = 500;
            v.B = 200;
            Thread t3 = new Thread(new ParameterizedThreadStart(calculator.Plus3));
            t3.IsBackground = true;
            t3.Start(v);
        }
    }
    
}
```
```
<ThreadSyncError>
using System;
using System.Threading;
namespace ThreadSyncError
{
    class Program
    {
        static int sharedValue = 0;
        private static readonly object lockObject = new object();
        

        static void Main(string[] args)
        {
            Thread incrementThread = new Thread(Increment);
            Thread decrementThread = new Thread(Decrement);

            // 스레드 시작
            incrementThread.Start();
            decrementThread.Start();

            // 스레드가 종료되기를 기다림
            incrementThread.Join();
            decrementThread.Join();

            Console.WriteLine($"최종 값: {sharedValue}");
        }

        static void Increment()
        {
            lock (lockObject)
            {
                for (int i = 0; i < 100000; i++)
                {
                    sharedValue++;
                }
            }
        }

        static void Decrement()
        {
            lock (lockObject)
            {
                for (int i = 0; i < 100000; i++)
                {
                    sharedValue--;
                }
            }
        }
    }
}
```
