using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using harbor.Helpers;
using harbor.Services;

namespace harbor.Commands
{    
    public class EnableCommand : BaseCommand
    {
        private readonly ServicesCollector _servicesCollector;

        public EnableCommand(ServicesCollector servicesCollector)
        {
            _servicesCollector = servicesCollector;
        }

        /// <summary>
        /// Enable handling
        /// </summary>
        /// <param name="argService"></param>
        public async Task HandleAsync(string[] args)
        {
            if (args.Length <= 1) {
                ConsoleHelper.PrintError("Please pass a service name.");
                return;
            }

            List<string> services = this._servicesCollector.GetServices();

            // Verfiy the service exist     
            string argService = args[1];
            if (!services.Contains(argService.ToLower())) {
                ConsoleHelper.PrintError("We wasn't able to find the service.");
                Environment.Exit(0);
            }
            
            // Todo optimize this.
            switch (argService.ToLower())
            {               
                case "mysql":
                    var servicemysql = new MySQL();
                    await servicemysql.EnableAsync();
                    break;
                case "mssql":
                    var serviceMssql = new MsSQL();
                    await serviceMssql.EnableAsync();
                    break;
                case "redis":
                    var serviceRedis = new Redis();
                    await serviceRedis.EnableAsync();
                    break;
                default:
                    ConsoleHelper.PrintError("We wasn't able to find the service.");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}