using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var novoLivro = _livroRepository.FilterByCondition(x => x.ISBN == livro.ISBN).FirstOrDefault();

            if (novoLivro != null)
            {
                throw new Exception("ISBN já existe!");
            }
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

        public List<Livro> GetLivrosPorFiltro(LivroFiltroDTO filtro)
        {
            var query = _livroRepository.FilterByCondition(x=> x.Id > 0);
            if (filtro.ISBN > 0 )
            {
                query = query.Where(x => x.ISBN == filtro.ISBN);
            }

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                query = query.Where(x => x.Nome.Contains(filtro.Nome));
            }

            if (!string.IsNullOrEmpty(filtro.Autor))
            {
                query = query.Where(x => x.Autor.Contains(filtro.Autor));
            }
            if (filtro.Preco > 0)
            {
                query = query.Where(x => x.Preco == filtro.Preco);
            }
           
            if (filtro.SortBy.ToUpper() == "ISBN")
            {
                return query.OrderBy(o => o.ISBN).ToList();
            }
            else if (filtro.SortBy.ToUpper() == "NOME")
            {
                return query.OrderBy(o => o.Nome).ToList();
            }
            else if (filtro.SortBy.ToUpper() == "AUTOR")
            {
                return query.OrderBy(o => o.Autor).ToList();
            }
            else if (filtro.SortBy.ToUpper() == "PRECO")
            {
                return query.OrderBy(o => o.Preco).ToList();
            }
            else if (filtro.SortBy.ToUpper() == "DATAPUBLICACAO")
            {
                return query.OrderBy(o => o.DataPublicacao).ToList();
            }
            //if (filtro.DataPublicacao))
            //{
            //    query = query.Where(x => x.DataPublicacao == filtro.DataPublicacao));
            //}

            return query.ToList();
        }
    }
}
