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
        public DbSet<Autor> Autores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=LIVRARIA.db");
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Livro>(ConfigureLivro);
        //    builder.Entity<Autor>(ConfigureAutor);
        }

        private void ConfigureLivro(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("LIVRO");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsRequired();
           
        }
        private void ConfigureAutor(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("AUTOR");
        }
    }
}
