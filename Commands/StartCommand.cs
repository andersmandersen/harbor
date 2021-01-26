using System.Collections.Generic;
using System.Threading.Tasks;
using harbor.Helpers;

namespace harbor.Commands
{    
    public class StartCommand : BaseCommand
    {
        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task HandleAsync(string[] args)
        {        
            if (args.Length <= 1) {
                ConsoleHelper.PrintError("Please pass a valid container ID. Or use the --all flag");
                return;
            }

            var containers = await this._docker.GetContainersAsync();

            // Stops all
            if (args[1] == "--all") {
                await this.StartAll(containers);
                return;
            }

            string container_id = args[1];
            if (!containers.ContainsKey(container_id)) {
                ConsoleHelper.PrintError("Please pass a valid container ID.");
                return;
            }

            await this.StartSingle(container_id);
        }
        
        /// <summary>
        /// Stops a single container
        /// </summary>
        /// <param name="container_id"></param>
        /// <returns></returns>
        private async Task StartSingle(string container_id)
        {
            await this._docker.StartAsync(container_id);
            ConsoleHelper.PrintSuccess("The service has been started!");
        }

        /// <summary>
        /// Stops all containers
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        private async Task StartAll(Dictionary<string, string> containers)
        {
            foreach(KeyValuePair<string, string> container in containers)
            {
                await this._docker.StartAsync(container.Key);
            }

            ConsoleHelper.PrintSuccess("All services has been started!");
        }
    }
}