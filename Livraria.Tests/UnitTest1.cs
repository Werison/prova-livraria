using ApplicationCore.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Livraria.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DadoUmLivrSemPreenchimentoDoNomeDeveRetornarUmaExcecao()
        {
            new Livro(1, "", 10, "Paulo Coelho", DateTime.Now, "Imagem");
        }
        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
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

    }

  

}
