
# EF-FullTextSearch
Full text search in Entity Framework

    PM> Install-Package EfFts

Enity framework does not support full-text search predicates still. For EFv6, you can make a workaround using interception.  
The idea is to wrap search text with some magic word during inside plain String.Contains code and use interceptor to unwrap it right before sql is executed in SqlCommand.

Provide Full-Text Search capabilities For Entity Framework Code First In Sql Server.
Sample code:
```cs
    public class Note
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
     
            /// <summary>
            /// 
            /// </summary>
            public string NoteText { get; set; }
        }


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
        
        /// <summary>
        /// 
        /// </summary>
        class Program
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="args"></param>
            static void Main(string[] args)
            {
                DbInterception.Add(new FtsInterceptor());
                const string searchTerm = "john";
                using (var db = new FtsSampleContext())
                {
                    var result1 = db.Notes.FreeTextSearch(a => a.NoteText, searchTerm).ToList();
                    //SQL Server Profiler result ===>>>
                    //exec sp_executesql N'SELECT 
                    //    [Extent1].[Id] AS [Id], 
                    //    [Extent1].[NoteText] AS [NoteText]
                    //    FROM [dbo].[Notes] AS [Extent1]
                    //    WHERE FREETEXT([Extent1].[NoteText], @p__linq__0)',N'@p__linq__0 
                    //char(4096)',@p__linq__0='(john)'
                    var result2 = db.Notes.ContainsSearch(a => a.NoteText, searchTerm).ToList();
                    //SQL Server Profiler result ===>>>
                    //exec sp_executesql N'SELECT 
                    //    [Extent1].[Id] AS [Id], 
                    //    [Extent1].[NoteText] AS [NoteText]
                    //    FROM [dbo].[Notes] AS [Extent1]
                    //    WHERE CONTAINS([Extent1].[NoteText], @p__linq__0)',N'@p__linq__0 
                    //char(4096)',@p__linq__0='(john)'
                }
                Console.ReadKey();
            }
        }
```

Reference:

[Microsoft's Full Text Search in Entity Framework 6](http://www.entityframework.info/Home/FullTextSearch)

:مقاله فارسی

[Entity Framework توسط Full text search استفاده از](https://www.dntips.ir/post/1846/)

 
