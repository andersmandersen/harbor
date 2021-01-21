using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace harbor.Helpers
{    
    public class CommandsCollector
    {
        /// <summary>
        /// Auto register services within the namespace harbor.Services
        /// </summary>
        /// <returns></returns>
        public List<string> GetServices()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes()
                    .Where(type => type.Namespace == "harbor.Commands" && type.Name != "BaseCommand")
                    .Select(type => type.Name.ToString())
                    .ToList();
        }
    }
}