using System.Collections.Generic;
using harbor.Dto;

namespace harbor.Services
{    
    public class MsSQL : BaseService
    {                    
        public override string DisplayName { get; set; } = "mssql";
        public override string Image { get; set; } = "mcr.microsoft.com/mssql/server";

        public override List<ConsoleQuestion> Questions { get; set; }

        public override string DockerTemplate { get; set; } = "-e ACCEPT_EULA=Y -e SA_PASSWORD=$sa_password$ -p $port$:1433  $image_name$:$tag$";

        public MsSQL()
        {
            this.Questions = new List<ConsoleQuestion>{
                new ConsoleQuestion {
                    Shortname = "sa_password",
                    Question = "What will the sa password be (8 Characters long, uppercase, lowercase and 0 through 9)? (Leave blank to use 'HarborSecret1337')",
                    Response = "HarborSecret1337"
                }
            };
        }
    }
}