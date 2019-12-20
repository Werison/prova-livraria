using ApplicationCore.Entities;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;
using ApplicationCore.Interfaces;
using System.Linq;

namespace Livraria.Tests
{
    [TestClass]
    public class UnitTest
    {
        private ServiceCollection serviceCollection;
        private ILivroService service;
        public UnitTest()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFrameworkSqlite().AddDbContext<LivrariaContext>();
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepository<>));
            serviceCollection.AddScoped<ILivroService, LivroService>();
            var provider = serviceCollection.BuildServiceProvider();
            service = provider.GetService<ILivroService>();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DadoUmLivrSemPreenchimentoDoNomeDeveRetornarUmaExcecao()
        {
            new Livro(1, "", 10, "Paulo Coelho", DateTime.Now, "Imagem");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DadoUmLivroSemPreencherOAutorDeveRetornarUmaExecao()
        {
            new Livro(1, "Livro 1", 10, "", DateTime.Now, "Imagem");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DadoUmLivroSemPreencherOISBMDeveRetornarUmaExecao()
        {
            new Livro(0, "Livro 1", 10, "teste", DateTime.Now, "Imagem");
        }

        [TestMethod]
        public void DadaUmaInclusaoDeLivroComISBNNaoExistenteDeveRetornarTrue()
        {
            var livros = service.GetAll();
            var isbnFake = livros.Max(x => x.ISBN) + 1;
            var novoLivro = new Livro(isbnFake, "Livro 1", 10, $"Autor { isbnFake.ToString() }", DateTime.Now, $"Imagem {isbnFake}");
            Assert.IsTrue(service.AddLivro(novoLivro).Result.ISBN == isbnFake);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DadaUmaInclusaoDeLivroParaISBNExistenteDeveRetornarUmaExecao()
        {

            var isbnFake = service.GetAll().First().ISBN;
            var novoLivro = new Livro(isbnFake, "Livro 1", 10, $"Autor { isbnFake.ToString() }", DateTime.Now, $"Imagem {isbnFake}");
            service.AddLivro(novoLivro);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DadaUmaAlteracaoDeLivroParaISBNExistenteDeveRetornarUmaExecao()
        {
            var livros = service.GetAll();
            var livroAlterado = livros.LastOrDefault();
            var isbnExistente = livros.FirstOrDefault().ISBN;
            livroAlterado.ISBN = isbnExistente;
            var resultado = service.Update(livroAlterado).Result;
        }

        [TestMethod]
        public void DadaUmaAlteracaoDeLivroParaISBNNaoExistenteDeveRetornarTrue()
        {
            var livros = service.GetAll();
            var livroAlterado = livros.LastOrDefault();
            var isbnNaoExistente = livros.Max(x => x.ISBN) + 1;
            livroAlterado.ISBN = isbnNaoExistente;
            var resultado = service.Update(livroAlterado).Result;
            Assert.IsTrue(resultado.ISBN == livroAlterado.ISBN);
        }
        [TestMethod]
        public void DadaUmaExclusaoDeLivroDeveRetornarTrue()
        {
            var livros = service.GetAll();
            var livroAlterado = livros.LastOrDefault();
            service.Delete(livroAlterado.Id);
            var livros2 = service.GetAll();
            Assert.IsNull(service.GetByID(livroAlterado.Id).Result);
        }


    }



}
