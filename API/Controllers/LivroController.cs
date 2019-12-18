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
        public LivroController(IAsyncRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _livroRepository.ListAllAsync());

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task Post([FromBody] LivroDTO livro)
        {
            try
            {
                livro.ISBN = 1;
                livro.Nome = "O CRESCIMENTO E AS MUDANÇAS: O LIVRO DOS PORQUÊS";
                livro.Preco = 54.00;
                livro.DataPublicacao = new DateTime(1982, 01, 05);
                //livro.ImagemCapa = "https://livrariacultura.vteximg.com.br/arquivos/ids/15513906-475-475/2112174919.jpg?v=637105459930200000"; await _livroRepository.AddAsync(livro);
            }
            catch (Exception)
            {
                 BadRequest();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _livroRepository.GetByIdAsync(id));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}