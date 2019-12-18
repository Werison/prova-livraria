using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Infrastructure.Data;

namespace Livraria.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {

        }
        [TestMethod]
        public void DadoUmLivroValidoDeveRetornarTrue()
        {
            Livro livro = new Livro();
            livro.ISBN = 1;
          //  livro.Autores = new List<Autor> { new Autor { AutorID = 1, Nome = "Katie Daynes" }, new Autor { AutorID = 2, Nome = "Shelly Laslo" } };
            livro.Nome = "O CRESCIMENTO E AS MUDANÇAS: O LIVRO DOS PORQUÊS";
            livro.Preco = 54.00;
            livro.DataPublicacao = new DateTime(1982, 01, 05);
            livro.ImagemCapa = "https://livrariacultura.vteximg.com.br/arquivos/ids/15513906-475-475/2112174919.jpg?v=637105459930200000";

           //  new ApplicationCore.Services.LivroService().AddLivro(1, livro.ISBN, livro.Nome, livro.Preco, livro.DataPublicacao, livro.ImagemCapa);
         //  var a = new ApplicationCore.Services.LivroService(new EFRepository).GetAll();
        }

    }

  

}
