using System.Data.Entity;
using Example.Models;
using Example.Models.Mappings;

namespace Example
{
    /// <summary>
    /// 
    /// </summary>
    public class FtsSampleContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoteMap());
        }
    }
}
