using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EfPooling
{
    public class RunTests
    {
        public long RequestsProcessed;
        public static long ContextInstances;

        private readonly TimeSpan _duration = TimeSpan.FromSeconds(10);
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private async Task SimulateContinualRequests(IServiceProvider serviceProvider)
        {
            while (_stopwatch.IsRunning)
            {
                using (var serviceScope = serviceProvider.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<BloggingContext>();
                    await context.Blogs.FirstAsync();
                }
                Interlocked.Increment(ref RequestsProcessed);
            }
        }

        private async void MonitorResults()
        {
            var lastInstanceCount = (long)0;
            var lastRequestCount = (long)0;
            var lastElapsed = TimeSpan.Zero;

            _stopwatch.Start();

            while (_stopwatch.Elapsed < _duration)
            {
                var thisInstanceCount = ContextInstances;
                var thisRequestCount = RequestsProcessed;
                var thisElapsed = _stopwatch.Elapsed;

                var currentElapsed = thisElapsed - lastElapsed;
                var currentRequests = thisRequestCount - lastRequestCount;

                Console.WriteLine("{0}{1}", $"Context creations: {thisInstanceCount - lastInstanceCount} | ", $"Requests per second: {Math.Round(currentRequests / currentElapsed.TotalSeconds)}");

                lastInstanceCount = thisInstanceCount;
                lastRequestCount = thisRequestCount;
                lastElapsed = thisElapsed;
            }

            Console.WriteLine("");
            Console.WriteLine($"Total context creations: {ContextInstances}");
            Console.WriteLine($"Requests per second:     {Math.Round(RequestsProcessed / _stopwatch.Elapsed.TotalSeconds)}");

            _stopwatch.Stop();
        }

        public void Start(IServiceProvider serviceProvider)
        {
            RequestsProcessed = 0;
            ContextInstances = 0;

            SetupDatabase.Run(serviceProvider);

            MonitorResults();

            const int threads = 32;
            var tasks = new Task[threads];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = SimulateContinualRequests(serviceProvider);
            }
            Task.WhenAll(tasks).Wait();
        }
    }
}