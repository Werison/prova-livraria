using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Livro: BaseEntity
    {
        public int ISBN { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        //public List<Autor> Autores { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string ImagemCapa { get; set; }
    }
}
