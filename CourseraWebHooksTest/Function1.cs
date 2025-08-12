using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace CourseraWebHooksTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);

            var data = System.Text.Json.JsonSerializer.Deserialize<WebhookPayload>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data != null && data.Pages != null && data.Pages.Count != 0)
            {
                var page = data.Pages.First();
                return new OkObjectResult($"Page is {page.Title}, Action is {page.Action}, Event Type is {req.Headers["x-github-event"]}");
            }
            else
            {
                return new BadRequestObjectResult("Invalid payload for Wiki event");
            }
        }
    }

    public class WebhookPayload
    {
        public List<Page> Pages { get; set; }
        public Repository Repository { get; set; }
        public Sender Sender { get; set; }
    }

    public class Page
    {
        public string Page_Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Action { get; set; }
        public string Sha { get; set; }
        public string Html_Url { get; set; }
    }

    public class Repository
    {
        public long Id { get; set; }
        public string Node_Id { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
        public bool Private { get; set; }
        public Owner Owner { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }
        public bool Fork { get; set; }
        public string Url { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Pushed_At { get; set; }
        public string Git_Url { get; set; }
        public string Ssh_Url { get; set; }
        public string Clone_Url { get; set; }
        public string Svn_Url { get; set; }
        public object Homepage { get; set; }
        public int Size { get; set; }
        public int Stargazers_Count { get; set; }
        public int Watchers_Count { get; set; }
        public object Language { get; set; }
        public bool Has_Issues { get; set; }
        public bool Has_Projects { get; set; }
        public bool Has_Downloads { get; set; }
        public bool Has_Wiki { get; set; }
        public bool Has_Pages { get; set; }
        public bool Has_Discussions { get; set; }
        public int Forks_Count { get; set; }
        public object Mirror_Url { get; set; }
        public bool Archived { get; set; }
        public bool Disabled { get; set; }
        public int Open_Issues_Count { get; set; }
        public object License { get; set; }
        public bool Allow_Forking { get; set; }
        public bool Is_Template { get; set; }
        public bool Web_Commit_Signoff_Required { get; set; }
        public List<object> Topics { get; set; }
        public string Visibility { get; set; }
        public int Forks { get; set; }
        public int Open_Issues { get; set; }
        public int Watchers { get; set; }
        public string Default_Branch { get; set; }
    }

    public class Owner
    {
        public string Login { get; set; }
        public long Id { get; set; }
        public string Node_Id { get; set; }
        public string Avatar_Url { get; set; }
        public string Gravatar_Id { get; set; }
        public string Url { get; set; }
        public string Html_Url { get; set; }
        public string Followers_Url { get; set; }
        public string Following_Url { get; set; }
        public string Gists_Url { get; set; }
        public string Starred_Url { get; set; }
        public string Subscriptions_Url { get; set; }
        public string Organizations_Url { get; set; }
        public string Repos_Url { get; set; }
        public string Events_Url { get; set; }
        public string Received_Events_Url { get; set; }
        public string Type { get; set; }
        public string User_View_Type { get; set; }
        public bool Site_Admin { get; set; }
    }

    public class Sender
    {
        public string Login { get; set; }
        public long Id { get; set; }
        public string Node_Id { get; set; }
        public string Avatar_Url { get; set; }
        public string Gravatar_Id { get; set; }
        public string Url { get; set; }
        public string Html_Url { get; set; }
        public string Followers_Url { get; set; }
        public string Following_Url { get; set; }
        public string Gists_Url { get; set; }
        public string Starred_Url { get; set; }
        public string Subscriptions_Url { get; set; }
        public string Organizations_Url { get; set; }
        public string Repos_Url { get; set; }
        public string Events_Url { get; set; }
        public string Received_Events_Url { get; set; }
        public string Type { get; set; }
        public string User_View_Type { get; set; }
        public bool Site_Admin { get; set; }
    }

}
