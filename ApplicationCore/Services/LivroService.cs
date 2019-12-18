using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class LivroService : ILivroService
    {
        private readonly IAsyncRepository<Livro> _livroRepository;
        public LivroService(IAsyncRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public Task AddLivro(int id, int isbn, string nome, double preco, DateTime dataPublicacao, string ImagemCapa)
        {
            return null;
           // return _livroRepository.AddAsync(new Livro { Id = id, ISBN = isbn, Nome = nome, Preco = preco, DataPublicacao = dataPublicacao, ImagemCapa = ImagemCapa });
        }

        public async Task Delete(int LivroID)
        {
            var livro = await _livroRepository.GetByIdAsync(LivroID);
            await _livroRepository.DeleteAsync(livro);
        }

        public async Task GetAll()
        {
            await _livroRepository.ListAllAsync();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
