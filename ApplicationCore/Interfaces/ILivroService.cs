using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ILivroService
    {
        Task AddLivro(int id, int isbn, string nome, double preco, DateTime dataPublicacao, string ImagemCapa);
        Task Delete(int LivroID);
        Task GetAll();
        Task Update(Livro livro);
    }
}
//public int LivroID { get; set; }
//public int ISBN { get; set; }
//public string Nome { get; set; }
//public double Preco { get; set; }
//public List<Autor> Autores { get; set; }
//public DateTime DataPublicacao { get; set; }
//public string ImagemCapa { get; set; }
    