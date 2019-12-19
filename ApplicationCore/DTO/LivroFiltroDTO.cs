using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO
{
    public class LivroFiltroDTO
    {
        public int ISBN { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string ImagemCapa { get; set; }
        public string SortBy { get; set; }
    }
}
