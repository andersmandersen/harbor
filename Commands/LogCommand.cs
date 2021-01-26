using System.Threading.Tasks;
using harbor.Helpers;

namespace harbor.Commands
{    
    public class LogCommand : BaseCommand
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

            string container_id = args[1];
            if (!containers.ContainsKey(container_id)) {
                ConsoleHelper.PrintError("Please pass a valid container ID.");
                return;
            }            

            await this._docker.LogAsync(container_id);         
        }
    }
}