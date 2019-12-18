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

            var result = await _livroRepository.ListAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task Post([FromBody] LivroDTO livro)
        {
            Livro oLivro = new Livro(livro.ISBN, livro.Nome, livro.Preco, livro.Autor, livro.DataPublicacao);
            var retorno = await _livroRepository.AddAsync(oLivro);
            Ok(retorno);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _livroRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LivroDTO livro)
        {
            try
            {
                var entity = _livroRepository.GetByIdAsync(id);
                Livro oLivro = new Livro(livro.ISBN, livro.Nome, livro.Preco, livro.Autor, livro.DataPublicacao);
                await _livroRepository.UpdateAsync(oLivro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}