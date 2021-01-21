using System;
using harbor.Helpers;

namespace harbor.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly ServicesCollector servicesCollector;

        public HelpCommand(ServicesCollector servicesCollector)
        {
            this.servicesCollector = servicesCollector;
        }

        public void Handle()
        {
            Console.WriteLine("\nUsage: harbor <command>");

            Console.WriteLine("\nCommands:");
            Console.WriteLine("\tenable {service} \tEnables a giving service");
            Console.WriteLine("\tdisable {service} \tDisable a giving service");
            Console.WriteLine("\tstart {container_id} \tStart a existing service");
            Console.WriteLine("\tstop {container_id} \tStop a existing service");
            Console.WriteLine("\tlog {container_id} \tDisplay log for a service");
            Console.WriteLine("\tlist                \tList all enabled services. Will provide you with the container id used for start/stopping a service");

            Console.WriteLine("\nServices:");            
            foreach(var service in this.servicesCollector.GetServices()) {
                Console.WriteLine($"\t{service}");
            }            
        }
    }
}