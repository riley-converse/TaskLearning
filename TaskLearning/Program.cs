using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace TaskLearning
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Asynchronous, doesn't hold up thread.
            Console.WriteLine("Hello, ");
            //await PrintConcurrentText();
            await PrintConcurrentTextAtOnce();
            Console.WriteLine("World");
        }

        static async Task PrintConcurrentText()
        {
            await Print("First",500);
            await Print("Second", 500);
            await Print("Third", 500);
        }

        static async Task Print(string str, int sleep)
        {
            Thread.Sleep(sleep);
            Console.WriteLine(str);
            await Task.Yield();
        }
        static async Task FindMatch(int numberOfTasks)
        {
            
            using (var cts  = new CancellationTokenSource()) 
            {
                Task[] tasks = await TaskFactory(numberOfTasks, cts);

                foreach(Task task in tasks)
                {
                    
                }
                var completedTask = await Task.WhenAny(tasks);
               
                //var result = await completedTask;
            }
            

            
        }

        static async Task<Task[]> TaskFactory(int numerOfTasks, CancellationTokenSource token)
        {
            Task[] tasks = new Task[numerOfTasks];

            for (int i = 0;i < numerOfTasks;i++)
            {
                await (tasks[i] = new Task(async () => await CheckText( await GenerateChar()),token.Token));
            }

            return tasks;
        }

        static async Task<char> GenerateChar()
        {
            var rnd = new Random();
            char c = (char)((byte)rnd.Next(97,122));
            await Task.Yield();
            return c;
        }

        static async Task<bool> CheckText(char targetChar)
        {
            string exampleText = "Hello world and everyone who can read this text.";
            
            foreach (char c in exampleText)
            {
                if (c == targetChar)
                {
                    return true;
                }
            }
            return false;
          
        }
    }
}
