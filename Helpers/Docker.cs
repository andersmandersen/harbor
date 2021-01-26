using System;
using System.Diagnostics;
using harbor.Helpers;
using CliWrap;
using CliWrap.Buffered;
using System.Threading.Tasks;
using System.Collections.Generic;
using CliWrap.EventStream;

namespace harbor.Shell
{
    public class Docker
    {
        
        /// <summary>
        /// Boot container
        /// </summary>
        /// <param name="command"></param>
        /// <param name="container_name"></param>
        public async Task BootContainerAsync(string command, string container_name)
        {
            var exitingContainers = await this.GetContainersAsync();
            if (exitingContainers.ContainsValue(container_name)) {
                ConsoleHelper.PrintError("A service with the inputted options are alredy exiting. Run 'harbor list' to display existing services.");  
                Environment.Exit(0);
            }

            ConsoleHelper.PrintInfo("Enabling service....");            
            
            var result = await Cli.Wrap("docker").WithArguments(string.Format("run -d --name {0} {1}", container_name, command)).ExecuteBufferedAsync();    

            ConsoleHelper.PrintSuccess("Service has been enabled and started!");
        }

        /// <summary>
        /// Destroy a giving container id
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        public async Task DestroyAsync(string container_id)
        {
            var result = await Cli.Wrap("docker").WithArguments($"rm --force -v {container_id}").ExecuteBufferedAsync();            
        }

        /// <summary>
        /// Stops a giving container
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        public async Task StopAsync(string container_id)
        {
            var result = await Cli.Wrap("docker").WithArguments($"stop {container_id}").ExecuteBufferedAsync();   
        }

        /// <summary>
        /// Stars a giving container
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        public async Task StartAsync(string container_id)
        {
            var result = await Cli.Wrap("docker").WithArguments($"start {container_id}").ExecuteBufferedAsync();   
        }

        /// <summary>
        /// Prints log from container
        /// </summary>
        /// <returns></returns>
        public async Task LogAsync(string container_id)
        {
            var cmd = Cli.Wrap("docker").WithArguments($"logs -f {container_id}");
            await foreach (var cmdEvent in cmd.ListenAsync())
            {
                switch (cmdEvent)
                {
                    case StartedCommandEvent started:
                        Console.WriteLine($"Press CTRL + C to exist log");
                        break;
                    case StandardOutputCommandEvent stdOut:
                        Console.WriteLine($"Out> {stdOut.Text}");
                        break;
                    case StandardErrorCommandEvent stdErr:
                        Console.WriteLine($"Err> {stdErr.Text}");
                        break;
                    case ExitedCommandEvent exited:
                        Console.WriteLine($"Process exited; Code: {exited.ExitCode}");
                        break;
                }
            }
        }

        /// <summary>
        /// Print all harbor containers
        /// </summary>
        /// <returns></returns>
        public async Task PrintContainersAsync()
        {            
            var result = await Cli.Wrap("docker").WithArguments("ps -a --filter \"name=HARBOR-\" --format \"table {{.ID}}\\t{{.Names}}\\t{{.Status}}\"").ExecuteBufferedAsync();
            Console.WriteLine(result.StandardOutput);
        } 

         /// <summary>
        /// Print all harbor containers
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetContainersAsync()
        {            
            var result = await Cli.Wrap("docker").WithArguments("ps -a --filter \"name=HARBOR-\" --format \"table {{.ID}}|{{.Names}}|{{.Status}}\"").ExecuteBufferedAsync();
            var resultString = result.StandardOutput.Split("\n");
            
            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach(var r in resultString) {
                if (r.Contains("CONTAINER ID") || r.Trim() == "") {
                    continue;
                }

                var x = r.Split("|");                                
                list[x[0]] = x[1];                
            }
            return list;
        } 
    }
}