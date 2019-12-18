using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
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
        public async Task<Livro> AddLivro(Livro livro)
        {
            var novoLivro = _livroRepository.ListAllAsync();
            Guard.Against.Null(novoLivro, nameof(novoLivro));

            return await _livroRepository.AddAsync(livro);
        }

        public async Task Delete(int LivroID)
        {
            var livro = await _livroRepository.GetByIdAsync(LivroID);
            await _livroRepository.DeleteAsync(livro);
        }

        public List<Livro> GetAll()
        {
            return (List<Livro>)_livroRepository.ListAllAsync().Result;
        }

        public async Task Update(Livro livro)
        {
            var entity = _livroRepository.GetByIdAsync(livro.Id);
            (entity.Result as Livro).ISBN = livro.ISBN;
            (entity.Result as Livro).Nome = livro.Nome;
            (entity.Result as Livro).Autor = livro.Autor;
            (entity.Result as Livro).Preco = livro.Preco;
            (entity.Result as Livro).ImagemCapa = livro.ImagemCapa;

            if (entity.IsCompletedSuccessfully )
            {
                await _livroRepository.UpdateAsync(entity.Result);
            }
        }
        public async Task<Livro> GetByID(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }
    }
}
