using crud_cassandra.Model;
using crud_cassandra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace crud_cassandra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClasseController : ControllerBase
    {
        

        private readonly ILogger<Classe> _logger;
        private readonly ClasseRepository _repo;

        public ClasseController(ILogger<ClasseController> logger,ClasseRepository repo)
        {
            _repo = repo;
        }

        [HttpGet(Name = "GetClasse")]
        public IEnumerable<Classe> Get()
        {
            var classes = _repo.GetClasseList();
            return classes; 
        }

        [HttpPost(Name = "PostClasse")]
        public IActionResult Post([FromBody] Classe aluno)
        {
            try
            {
                var criar_classe = _repo.PostClasseAluno(aluno.id,aluno.nome,aluno.sobrenome);
                return Ok(new { StatusCode = 200, Message = "Aluno criado com sucesso", Data = criar_classe });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.Message);
                return NotFound();
            }
        }

        [HttpPut(Name = "PutClasse")]
        public IActionResult Put([FromBody] Classe aluno)
        {
            try
            {
                var criar_classe = _repo.PutClasseAluno(aluno.id, aluno.nome, aluno.sobrenome);
                return Ok(new { StatusCode = 200, Message = "Aluno atualizado com sucesso", Data = criar_classe });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.Message);
                return NotFound();
            }
        }


        [HttpDelete(Name = "PutClasse")]
        public IActionResult Delete([FromBody] Classe aluno)
        {
            try
            {
                var criar_classe = _repo.DeleteClasseAluno(aluno.id);
                return Ok(new { StatusCode = 200, Message = "Aluno deletado com sucesso", Data = criar_classe });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.Message);
                return NotFound();
            }
        }


    }
}
