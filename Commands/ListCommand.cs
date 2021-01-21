using harbor.Shell;

namespace harbor.Commands
{
    public class ListCommand : BaseCommand
    {       
        public async System.Threading.Tasks.Task HandleAsync()
        {
            await this._docker.PrintContainersAsync();
        }
    }
}