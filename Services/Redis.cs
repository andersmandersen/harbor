using System.Collections.Generic;
using harbor.Dto;

namespace harbor.Services
{    
    public class Redis : BaseService
    {            
        public override string DisplayName { get; set; } = "redis";
        public override string Image { get; set; } = "redis";

        public override List<ConsoleQuestion> Questions { get; set; }

        public override string DockerTemplate { get; set; } = "-p $port$:6379 -v $volume$:/data $image_name$:$tag$";

        public Redis()
        {
            this.Questions = new List<ConsoleQuestion>{                
                new ConsoleQuestion {
                    Shortname = "volume",
                    Question = "Docker volume name? (Leave blank to use default)",
                    Response = "redis_data"
                }
            };
        }
    }
}