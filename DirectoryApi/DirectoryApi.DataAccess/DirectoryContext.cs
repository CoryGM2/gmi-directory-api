using System;

using Microsoft.EntityFrameworkCore;

namespace DirectoryApi.DataAccess
{
    public class DirectoryContext : DbContext
    {
        public DirectoryContext(DbContextOptions<DirectoryContext> options)
            : base(options)
        { }

        public DbSet<PersonPg> People { get; set; }
    }
}
