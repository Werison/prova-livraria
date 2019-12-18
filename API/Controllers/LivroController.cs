using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IAsyncRepository<Livro> _livroRepository;
        private readonly ILivroService _livroServce;

        public LivroController(IAsyncRepository<Livro> livroRepository, ILivroService livroServce)
        {
            _livroRepository = livroRepository;
            _livroServce = livroServce;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            return Ok(_livroServce.GetAll());
        }

        [HttpPost]
        public async Task Post([FromBody] LivroDTO livro)
        {
            Ok(await _livroServce.AddLivro(new Livro(livro.ISBN, livro.Nome, livro.Preco, livro.Autor, livro.DataPublicacao)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _livroServce.GetByID(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LivroDTO livro)
        {
            await _livroServce.Update(new Livro
            {
                Id = id,
                Nome = livro.Nome,
                ImagemCapa = livro.ImagemCapa,
                Preco = livro.Preco,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                DataPublicacao = livro.DataPublicacao
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroServce.Delete(id);
            return Ok();
        }
    }
}