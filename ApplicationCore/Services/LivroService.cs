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
            try
            {
                var livro = await _livroRepository.GetByIdAsync(LivroID);
                await _livroRepository.DeleteAsync(livro);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public List<Livro> GetAll()
        {
            return (List<Livro>)_livroRepository.ListAllAsync().Result;
        }

        public async Task<Livro> Update(Livro livro)
        {
            var entity = _livroRepository.GetByIdAsync(livro.Id);

            if (livro.ISBN > 0)
            {
                var livroExistente = _livroRepository.FilterByCondition(x => x.ISBN == livro.ISBN).FirstOrDefault();

                if (livroExistente != null)
                {
                    throw new Exception("ISBN já existe!");
                }

                (entity.Result as Livro).ISBN = livro.ISBN;
            }

            if (!String.IsNullOrEmpty(livro.Nome))
            {
                (entity.Result as Livro).Nome = livro.Nome;
            }

            if (!String.IsNullOrEmpty(livro.Autor))
            {
                (entity.Result as Livro).Autor = livro.Autor;
            }

            if (livro.Preco > 0)
            {
                (entity.Result as Livro).Preco = livro.Preco;
            }

            if (!String.IsNullOrEmpty(livro.ImagemCapa))
            {
                (entity.Result as Livro).ImagemCapa = livro.ImagemCapa;
            }

            if (entity.IsCompletedSuccessfully)
            {
                await _livroRepository.UpdateAsync(entity.Result);
                
            }
            return entity.Result;
        }

        public async Task<Livro> GetByID(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public List<Livro> GetLivrosPorFiltro(LivroFiltroDTO filtro)
        {
            var query = _livroRepository.FilterByCondition(x => x.Id > 0);
            if (filtro.ISBN > 0)
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

            if (filtro.SortBy != null && filtro.SortBy.ToUpper() == "ISBN")
            {
                return query.OrderBy(o => o.ISBN).ToList();
            }
            else if (filtro.SortBy != null && filtro.SortBy.ToUpper() == "NOME")
            {
                return query.OrderBy(o => o.Nome).ToList();
            }
            else if (filtro.SortBy != null && filtro.SortBy.ToUpper() == "AUTOR")
            {
                return query.OrderBy(o => o.Autor).ToList();
            }
            else if (filtro.SortBy != null && filtro.SortBy.ToUpper() == "PRECO")
            {
                return query.OrderBy(o => o.Preco).ToList();
            }
            else if (filtro.SortBy != null && filtro.SortBy.ToUpper() == "DATAPUBLICACAO")
            {
                return query.OrderBy(o => o.DataPublicacao).ToList();
            }
            

            return query.ToList();
        }
    }
}
