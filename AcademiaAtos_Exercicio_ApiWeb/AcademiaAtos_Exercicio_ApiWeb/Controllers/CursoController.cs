using AcademiaAtos_Exercicio_ApiWeb.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAtos_Exercicio_ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : Controller
    {
        private readonly Contexto _contexto;

        public CursoController(Contexto contexto)
        {
            _contexto = contexto;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var curso = _contexto.Cursos.ToList();
            return Ok(curso);
        }

        [HttpGet("curso/{curso}")]
        public async Task<ActionResult> GetByYear([FromRoute] string curso)
        {
            var cursos = _contexto.Cursos.Where(c => c.Nome == curso).ToList();
            return Ok(cursos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Curso curso)
        {
            _contexto.Cursos.Add(curso);
            await _contexto.SaveChangesAsync();
            return Ok(curso);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Curso curso)
        {
            var cursos = await _contexto.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();


            if (cursos != null)
            {
                cursos.Nome = curso.Nome;
                await _contexto.SaveChangesAsync();
                return Ok("O curso foi atualizado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar o curso!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var alunos = await _contexto.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (alunos != null)
            {
                _contexto.Cursos.Remove(alunos);
                await _contexto.SaveChangesAsync();
                return Ok("O curso foi deletado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar o curso!");
            }
        }
    }
}
