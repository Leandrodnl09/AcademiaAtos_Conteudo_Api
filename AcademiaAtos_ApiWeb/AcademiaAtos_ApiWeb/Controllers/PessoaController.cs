using AcademiaAtos_ApiWeb.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAtos_ApiWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        [Route("pessoas")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var pessoas = await contexto
                .Pessoas
                .AsNoTracking()
                .ToListAsync();

            return pessoas == null ? NotFound() : Ok(pessoas);
        }

        [HttpGet]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var pessoas = await contexto
                .Pessoas
                //.AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);
            return pessoas == null ? NotFound() : Ok(pessoas);
        }

        [HttpPost]
        [Route("pessoas")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await contexto.Pessoas.AddAsync(pessoa);
                await contexto.SaveChangesAsync();
                return Created($"pessoas/pessoas/{pessoa.id}", pessoa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] Contexto contexto,[FromBody] Pessoa pessoa, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model invalida!");
            }
            
            var p = await contexto.Pessoas
                .FirstOrDefaultAsync(x => x.id == id);  

            if (p == null)
            {
                return NotFound("Pessoa não encontrada!");
            }

            try
            {
                p.nome = pessoa.nome;
                await contexto.SaveChangesAsync();

                return Ok(p);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var p = await contexto.Pessoas.FirstOrDefaultAsync(p => p.id == id);

            if(p == null)
            {
                return BadRequest("Pessoa não encontrada!");
            }
            try
            {
                contexto.Pessoas.Remove(p);
                await contexto.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> cadEmailemPessoa(
          [FromServices] Contexto contexto,
          [FromBody] Email email,
          [FromRoute] int id
          )
        {
            var pessoa = await contexto
                .Pessoas.AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            email.pessoa = pessoa;

            try
            {
                contexto.Set<Email>().Add(email);
                contexto.Entry(email.pessoa).State = EntityState.Unchanged;
                await contexto.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
            }
            return Created($"api/pessoas/{pessoa.id}", email);


        }
    }
}
