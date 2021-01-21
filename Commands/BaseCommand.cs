using harbor.Shell;

namespace harbor.Commands
{
    public class BaseCommand
    {
        protected Docker _docker { get; set; }        

        public BaseCommand()
        {
            this._docker = new Docker();
        }
    }
}