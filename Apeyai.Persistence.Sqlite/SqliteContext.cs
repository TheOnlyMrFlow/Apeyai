using System;
using System.IO;
using Apeyai.Persistence.Sqlite.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite
{
    public class SqliteContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<SchemaDbEntity> Schemas { get; set; }

        public DbSet<TextAttributeDbEntity> TextAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbFilePath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Apeyai", "Persistence", "Sqlite", "apeyai.db");

            options.UseSqlite(@$"Data source={dbFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SchemaDbEntity.OnModelCreating(modelBuilder);
            TextAttributeDbEntity.OnModelCreating(modelBuilder);
        }
    }
}
