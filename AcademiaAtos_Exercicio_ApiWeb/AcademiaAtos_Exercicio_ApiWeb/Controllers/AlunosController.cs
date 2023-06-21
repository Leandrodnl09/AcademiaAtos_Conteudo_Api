using AcademiaAtos_Exercicio_ApiWeb.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace AcademiaAtos_Exercicio_ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : Controller
    {
        private readonly Contexto _contexto;

        public AlunosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var alunos = _contexto.Alunos.ToList();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var aluno = await _contexto.Alunos.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Aluno aluno)
        {
            _contexto.Alunos.Add(aluno);
            await _contexto.SaveChangesAsync();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Aluno aluno)
        {
            var alunos = await _contexto.Alunos.Where(c => c.Id == id).FirstOrDefaultAsync();


            if (alunos != null)
            {
                alunos.Nome = aluno.Nome;
                alunos.Email = aluno.Email;
                await _contexto.SaveChangesAsync();
                return Ok("O Aluno foi atualizado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar o modelo!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var aluno = await _contexto.Alunos.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (aluno != null)
            {
                _contexto.Alunos.Remove(aluno);
                await _contexto.SaveChangesAsync();
                return Ok("O aluno foi deletado!");
            }
            else
            {
                return NotFound("Não foi possivel encontrar o aluno!");
            }
        }
    }
}
