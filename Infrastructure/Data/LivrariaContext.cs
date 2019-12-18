using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class LivrariaContext: DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> options): base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=LIVRARIA.db");
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Livro>(ConfigureLivro);
        }

        private void ConfigureLivro(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("LIVRO");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired();
           
        }
    }
}
