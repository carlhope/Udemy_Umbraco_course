using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmbracoTutorial.Core.Models
{
    public class GithubUserDTO
    {
        [JsonPropertyName("githubUsername")]
        public string UserName { get; set; }

        [JsonPropertyName("githubPrefferedLanguage")]
        public string PreferredProgrammingLanguage { get; set; }
    }
}
