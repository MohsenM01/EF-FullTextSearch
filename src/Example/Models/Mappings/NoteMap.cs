using System.Data.Entity.ModelConfiguration;

namespace Example.Models.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class NoteMap : EntityTypeConfiguration<Note>
    {
        /// <summary>
        /// 
        /// </summary>
        public NoteMap()
        {
            // Primary Key
            HasKey(t => t.Id);
        }
    }
}
