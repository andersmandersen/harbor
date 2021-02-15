using System.Collections.Generic;
using harbor.Dto;

namespace harbor.Services
{    
    public class MeiliSearch : BaseService
    {            
        public override string DisplayName { get; set; } = "MeiliSearch";
        public override string Image { get; set; } = "getmeili/meilisearch";

        public override List<ConsoleQuestion> Questions { get; set; }

        public override string DockerTemplate { get; set; } = "-p $port$:7700 -v $volume$:/data.ms $image_name$:$tag$";

        public MeiliSearch()
        {
            this.Questions = new List<ConsoleQuestion>{                
                new ConsoleQuestion {
                    Shortname = "volume",
                    Question = "Docker volume name? (Leave blank to use default)",
                    Response = "meilisearch_data"
                }
            };
        }
    }
}