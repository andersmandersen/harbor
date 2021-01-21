using System.Collections.Generic;
using System.Threading.Tasks;
using harbor.Helpers;

namespace harbor.Commands
{    
    public class StopCommand : BaseCommand
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

            var containers = await this._docker.GetContainersAsync();

            // Stops all
            if (args[1] == "--all") {
                await this.StopAll(containers);
                return;
            }

            string container_id = args[1];
            if (!containers.ContainsKey(container_id)) {
                ConsoleHelper.PrintInfo("Please pass a valid container ID.");
                return;
            }

            await this.StopSingle(container_id);
        }
        
        /// <summary>
        /// Stops a single container
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        private async Task StopSingle(string container_id)
        {
            await this._docker.StopAsync(container_id);
            ConsoleHelper.PrintInfo("The service has been stopped!");
        }

        /// <summary>
        /// Stops all containers
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        private async Task StopAll(Dictionary<string, string> containers)
        {
            foreach(KeyValuePair<string, string> container in containers)
            {
                await this._docker.StopAsync(container.Key);
            }

            ConsoleHelper.PrintInfo("All services has been stopped!");
        }
    }
}