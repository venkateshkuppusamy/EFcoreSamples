using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFcoreSamples.DataLayer
{
    public class BlogContext: DbContext
    {
        public ILoggerFactory loggerFactory;
        public BlogContext(ILoggerFactory loggerFactory) : base()
        {
            this.loggerFactory = loggerFactory;
        }

        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging().UseSqlServer(@"data source=(localdb)\ProjectsV13; initial catalog=BlogDB;Integrated Security = True;");
        }
        
    }
    
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public List<Blog> Blogs { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public List<Comment> Comments { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}
