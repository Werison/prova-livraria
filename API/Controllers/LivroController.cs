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
using System.IO;
using System.Net.Http.Headers;

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

        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            try
            {
                return Ok(_livroService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LivroDTO livro)
        {
            try
            {
                return Ok(await _livroService.AddLivro(new Livro(livro.ISBN, livro.Nome, livro.Preco, livro.Autor, livro.DataPublicacao, livro.ImagemCapa)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("LivrosComFiltro")]
        public ActionResult<IEnumerable<Livro>> PostByFilter([FromBody] LivroFiltroDTO filtro)
        {
            try
            {
                var result = _livroService.GetLivrosPorFiltro(filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _livroService.GetByID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
                return BadRequest(ex);
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
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }

         //   return BadRequest("Erro ao tentar realizar upload");
        }
    }
}