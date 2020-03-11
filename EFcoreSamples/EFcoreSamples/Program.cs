using EFcoreSamples.DataLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace EFcoreSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            Console.WriteLine("Hello World!");

            Author author = new Author()
            {
                Name = "Sadish",
                Blogs = new List<Blog>() { new Blog(){ Name= "Blog 1",
                Posts = new List<Post>() { new Post() { Name ="Post 1",
                Comments = new List<Comment>() { new Comment(){ Content ="Excellent Thanks"
                } } }  }  } }
            };
            var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var authors = JsonConvert.DeserializeObject<List<Author>>(GetJsonData());
            using (BlogContext db = new BlogContext(loggerFactory))
            {
                
                db.Authors.AddRange(authors);
                db.SaveChanges();
            }
            Console.ReadLine();
        }

        static string GetJsonData()
        {
            return "[{'name':'Anandraj','Blogs':[{'name':'Blog 10','Posts':[{'name':'Post 10','Comments':[{'Content':'Good Post'}]}]}]},{'name':'Sravanthi','Blogs':[{'name':'Blog 1','Posts':[{'name':'Post 11','Comments':[{'Content':'Excellent Post'}]}]}]},{'name':'KP','Blogs':[{'name':'Blog 1','Posts':[{'name':'Post 11','Comments':[{'Content':'I am KP fan, please add me in KFC club.'}]}]}]}]";
        }
    }
}
