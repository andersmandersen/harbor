using System;
using harbor.Commands;
using harbor.Helpers;

namespace harbor
{
    public class App
    {
        private readonly ServicesCollector services;
        private readonly EnableCommand enableCommand;
        private readonly ListCommand listCommand;
        private readonly StopCommand stopCommand;
        private readonly StartCommand startCommand;
        private readonly HelpCommand helpCommand;
        private readonly DisableCommand disableCommand;
        private readonly LogCommand logCommand;

        public App(ServicesCollector services,
                   EnableCommand enableCommand,
                   ListCommand listCommand,
                   StopCommand stopCommand,
                   StartCommand startCommand,
                   HelpCommand helpCommand,
                   DisableCommand disableCommand,
                   LogCommand logCommand)
        {
            this.services = services;
            this.enableCommand = enableCommand;
            this.listCommand = listCommand;
            this.stopCommand = stopCommand;
            this.startCommand = startCommand;
            this.helpCommand = helpCommand;
            this.disableCommand = disableCommand;
            this.logCommand = logCommand;
        }

        public async System.Threading.Tasks.Task RunAsync(string[] args)
        {
            if (args[0] == "enable") {
                await this.enableCommand.HandleAsync(args);
                return;
            }

            if (args[0] == "list") {
                await this.listCommand.HandleAsync();
                return;
            }

            if (args[0] == "stop") {
                await this.stopCommand.HandleAsync(args);
                return;
            }

            if (args[0] == "start") {
                await this.startCommand.HandleAsync(args);
                return;
            }

            if (args[0] == "disable") {
                await this.disableCommand.HandleAsync(args);
                return;
            }

            if (args[0] == "log") {
                await this.logCommand.HandleAsync(args);
                return;
            }

            if (args[0] == "help") {
                this.helpCommand.Handle();  
                return;
            }    
        }
    }
}