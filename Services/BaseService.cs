using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using harbor.Dto;
using harbor.Helpers;
using harbor.Shell;

namespace harbor.Services
{
    public abstract class BaseService
    {
        public virtual string DisplayName { get; set; }
        public virtual string Image { get; set; }
        public string Tag { get; set; }
        public virtual string DockerTemplate { get; set; }
        public abstract List<ConsoleQuestion> Questions { get; set; }
        protected List<ConsoleQuestion> BaseQuestions = new List<ConsoleQuestion>{
            new ConsoleQuestion {
                Shortname = "port",
                Question = "Which port should the service run on?"
            },
            new ConsoleQuestion {
                Shortname = "tag",
                Question = "Which version/tag would you like to use? (Leave blank to use latest)",
                Response = "latest"
            },
        };

        public Dictionary<string, string> Responses = new Dictionary<string, string>();
        private readonly Docker _docker;

        public BaseService()
        {
            _docker = new Docker();
        }

        /// <summary>
        /// Entry point
        /// </summary>
        public async Task EnableAsync()
        {
            this.AskQuestions();
            await _docker.BootContainerAsync(this.ParseDockerTemplate(), this.ContainerName());            
        }

        /// <summary>
        /// Ask user for questions
        /// </summary>
        private void AskQuestions()
        {
            foreach (var question in this.BaseQuestions)
            {
                var x = ConsoleHelper.Ask(question.Question);
                if (x != "") {
                    question.Response = x;
                }   
                this.Responses[question.Shortname] = question.Response;       

                if (question.Shortname == "tag") {
                    this.Tag = question.Response;
                }
            }

            foreach (var question in this.Questions)
            {
                var x = ConsoleHelper.Ask(question.Question);
                if (x != "") {
                    question.Response = x;
                }   
                this.Responses[question.Shortname] = question.Response;             
            }
        }

        /// <summary>
        /// Parse docker template
        /// </summary>
        /// <returns></returns>
        private string ParseDockerTemplate()
        {
            this.DockerTemplate = this.DockerTemplate.Replace("$image_name$", this.Image);

            Regex re = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);            
            return re.Replace(this.DockerTemplate, match => this.Responses[match.Groups[1].Value]);
        }
        
        /// <summary>
        /// Get container name
        /// </summary>
        /// <returns></returns>
        private string ContainerName()
        {
            return "HARBOR-" + this.DisplayName + "-" + this.Tag;
        }
    }
}