using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using harbor.Helpers;

namespace harbor.Commands
{
    public class DisableCommand : BaseCommand
    {
        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task HandleAsync(string[] args)
        {
            if (args.Length <= 1) {
                ConsoleHelper.PrintInfo("Please pass a valid container ID. Or use the --all flag");
                return;
            }

            Dictionary<string, string> containers = await this._docker.GetContainersAsync();

            // Handle all
            if (args[1] == "--all") {
                if (!ConsoleHelper.Confirm($"Are you sure you want to disable all services? (y/n)")) {
                    Environment.Exit(0);
                }
                await this.DisableAll(containers);
                return;
            }

            // Handle single
            string container_id = args[1];
            if (!containers.ContainsKey(container_id)) {
                ConsoleHelper.PrintInfo("Please pass a valid container ID.");
                return;
            }

             string service_name = containers[container_id];            
            if (!ConsoleHelper.Confirm($"Are you sure you want to disable {service_name}? (y/n)")) {
                Environment.Exit(0);
            }

            await this.DisableSingle(container_id);       
        }

        /// <summary>
        /// Stops a single container
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        private async Task DisableSingle(string container_id)
        {
            await this._docker.DestroyAsync(container_id);
            ConsoleHelper.PrintInfo("The service has been disabled!");
        }

        /// <summary>
        /// Stops all containers
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        private async Task DisableAll(Dictionary<string, string> containers)
        {
            foreach(KeyValuePair<string, string> container in containers)
            {
                await this._docker.DestroyAsync(container.Key);
            }

            ConsoleHelper.PrintInfo("All services has been disabled!");
        }
    }
}