using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TwitterApiDotNet.Models
{
    public class TwitterDbContext:DbContext
    {
        public TwitterDbContext(DbContextOptions options):base(options) {

        }

        public DbSet<Employee> Employees {get;set;}
        public DbSet<Tweets> Tweets {get; set;}
        public DbSet<TweetTag> TweetTags {get;set;}
    }
}