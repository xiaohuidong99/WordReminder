using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WordReminder.Data.Model;

namespace WordReminder.Data
{
    public class WordReminderContext : DbContext
    {
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<KeywordMeaning> KeywordMeanings { get; set; }
        public DbSet<KeywordMeaningSentence> KeywordMeaningSentences { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
