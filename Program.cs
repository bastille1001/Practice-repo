using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Nutshell.Study
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ExcelTask>();


            string text = "hello world";
            Test().Write(Encoding.UTF8.GetBytes(text), 0, text.Length);

            new Thread(LocalVar).Start();
            LocalVar();

            bool done = false;
            new Thred(SharedVar).Start();
            SharedVar();
            void SharedVar()
            {
                if (!done) { done = true; Console.WriteLine("Done") ;}
            }

            ThreadTest tt = new();
            new Thread(tt.Go).Start();
            tt.Go();
        }

        static FileStream Test()
        {
            using FileStream fs = new("test.txt", FileMode.Create);
            return fs; // System.ObjectDisposedException : 'Cannot access a closed file'
        }

        static void LocalVar()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine('?');
            }

            // a separate copy created on each Thread
            // output -> 10 '?'
        }
    }

    class ThreadTest
    {
        bool _done;

        public void Go()
        {
            if (!_done) { _done = true; Console.WriteLine("Done"); }
        }
    }
}
