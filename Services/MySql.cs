using System.Collections.Generic;
using harbor.Dto;

namespace harbor.Services
{    
    public class MySQL : BaseService
    {            
        public override string DisplayName { get; set; } = "mysql";
        public override string Image { get; set; } = "mysql";

        public override List<ConsoleQuestion> Questions { get; set; }

        public override string DockerTemplate { get; set; } = "-p $port$:3306 -e MYSQL_ROOT_PASSWORD=$root_password$ -v $volume$:/var/lib/mysql $image_name$:$tag$ --default-authentication-plugin=mysql_native_password";

        public MySQL()
        {
            this.Questions = new List<ConsoleQuestion>{
                new ConsoleQuestion {
                    Shortname = "root_password",
                    Question = "What will the root password be? (Leave blank to use 'secret')",
                    Response = "secret"
                },
                new ConsoleQuestion {
                    Shortname = "volume",
                    Question = "Docker volume name? (Leave blank to use default)",
                    Response = "mysql_volume"
                }
            };
        }
    }
}