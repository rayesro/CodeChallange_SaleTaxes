using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ConsoleApp : IHostedService
    {
        Ticketizonator _ticketizonator;
        public ConsoleApp(Ticketizonator ticketizonator)
        {
            _ticketizonator = ticketizonator;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            List<string> input = new List<string>();

            //input data
            Console.WriteLine("Input your products...Enter x to finish");
            do
            {
                var cmdInput = Console.ReadLine();
                if (cmdInput == "X" || cmdInput == "x")
                    break;
                input.Add(cmdInput);
            } while (true);

            
            var receipt = await _ticketizonator.Run(input);
            //printing results
            Console.WriteLine(receipt);

            //return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
