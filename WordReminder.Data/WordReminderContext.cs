using Microsoft.EntityFrameworkCore;
using WordReminder.Data.Model;



namespace WordReminder.Data
{
    public class WordReminderContext : DbContext
    {
        public WordReminderContext(DbContextOptions<WordReminderContext> options) : base(options)
        {

        }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<KeywordMeaning> KeywordMeanings { get; set; }
        public DbSet<KeywordMeaningSentence> KeywordMeaningSentences { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Keyword>().ToTable("Keyword");
            modelBuilder.Entity<KeywordMeaning>().ToTable("KeywordMeaning");
            modelBuilder.Entity<KeywordMeaningSentence>().ToTable("KeywordMeaningSentence");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
