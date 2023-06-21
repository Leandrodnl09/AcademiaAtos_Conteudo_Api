using AcademiaAtos_Exercicio_ApiWeb.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAtos_Exercicio_ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly Contexto _contexto;

        public MatriculasController(Contexto context)
        {
            _contexto = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var matriculas = _contexto.Matriculas.ToList();
            return Ok(matriculas);
        }

        [HttpGet("matricula/{matricula}")]
        public async Task<ActionResult> GetByYear([FromRoute] int matricula)
        {
            var matriculas = _contexto.Matriculas.Where(c => c.Id == matricula).ToList();
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Matricula matricula)
        {
            _contexto.Matriculas.Add(matricula);
            await _contexto.SaveChangesAsync();
            return Ok(matricula);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Matricula matricula)
        {
            var matriculas = await _contexto.Matriculas.Where(c => c.Id == id).FirstOrDefaultAsync();


            if (matriculas != null)
            {
                matriculas.AlunoId = matricula.AlunoId;
                matriculas.Aluno = matricula.Aluno;
                matriculas.CursoId = matricula.CursoId;
                matriculas.Curso = matricula.Curso;
                await _contexto.SaveChangesAsync();
                return Ok("A matricula foi atualizado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar a matricula!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var matriculas = await _contexto.Matriculas.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (matriculas != null)
            {
                _contexto.Matriculas.Remove(matriculas);
                await _contexto.SaveChangesAsync();
                return Ok("A matricula foi deletado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar a matricula!");
            }
        }
    }
}
