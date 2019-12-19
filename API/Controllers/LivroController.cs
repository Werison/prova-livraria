using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IAsyncRepository<Livro> _livroRepository;
        private readonly ILivroService _livroService;

        public LivroController(IAsyncRepository<Livro> livroRepository, ILivroService livroServce)
        {
            _livroRepository = livroRepository;
            _livroService = livroServce;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Livro>> Get()
        //{
        //    try
        //    {
        //        return Ok(_livroService.GetAll());
        //    }
        //    catch (Exception)
        //    {
        //        BadRequest();
        //    }
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task Post([FromBody] LivroDTO livro, FormFile imagemLivro)
        //{
        //    try
        //    {
        //        Ok(await _livroService.AddLivro(new Livro(livro.ISBN, livro.Nome, livro.Preco, livro.Autor, livro.DataPublicacao, livro.ImagemCapa)));
        //    }
        //    catch (Exception)
        //    {
        //        BadRequest();
        //    }

        //}

        [HttpPost]
        public  ActionResult<IEnumerable<Livro>> PostByFilter([FromBody] LivroDTO filtro)
        {
            try
            {
                var result = _livroService.GetLivrosPorFiltro(filtro);
                return Ok(result);
            }
            catch (Exception)
            {

                BadRequest();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                await _livroService.GetByID(id);
                return Ok();
            }
            catch (Exception)
            {

                BadRequest();
            }
           
           return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LivroDTO livro)
        {
            try
            {
                await _livroService.Update(new Livro
                {
                    Id = id,
                    Nome = livro.Nome,
                    ImagemCapa = livro.ImagemCapa,
                    Preco = livro.Preco,
                    Autor = livro.Autor,
                    ISBN = livro.ISBN,
                    DataPublicacao = livro.DataPublicacao
                });
            }
            catch (Exception ex)
            {

                BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _livroService.Delete(id);
            }
            catch (Exception)
            {
                BadRequest();
            }
            return Ok();
        }
    }
}