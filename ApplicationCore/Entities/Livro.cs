﻿using System;
using Ardalis.GuardClauses;

using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Livro: BaseEntity
    {
        public Livro()
        {

        }
        public Livro(int isbm, string nome, double preco, string autor, DateTime dataPublicacao, string imagemCapa)
        {
            Guard.Against.Zero(isbm, nameof(isbm));
            Guard.Against.NullOrEmpty(nome, nameof(nome));
            Guard.Against.NullOrEmpty(autor, nameof(autor));
            ISBN = isbm;
            Nome = nome;
            Preco = preco;
            Autor = autor;
            DataPublicacao = dataPublicacao;
            ImagemCapa = imagemCapa;
        }
        public int ISBN { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string ImagemCapa { get; set; }

    }
}
