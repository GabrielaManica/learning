using System;
using System.Collections.Generic;
using System.Linq;
using Cadastro.API.Data;
using Cadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly CadastroContext _context;

        public PessoasController(CadastroContext context)
        {
            _context = context;
        }

        // GET: api/Pessoas
        [HttpGet]
        public IEnumerable<Pessoa> Get([FromQuery] string nome, string email)
        {

            var list = _context.Pessoas.AsNoTracking();

            if (!string.IsNullOrEmpty(nome))
            {
                list = list.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));
            }
            if (!string.IsNullOrEmpty(email))
            {
                list = list.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }

            return list.ToList();
        }

        // GET: api/Pessoas/5
        [HttpGet("{id}", Name = "Get")]
        public Pessoa Get(int id)
        {
            return _context.Pessoas.FirstOrDefault(x => x.Id == id);
        }

        // POST: api/Pessoas
        [HttpPost]
        public void Post([FromBody] Pessoa value)
        {
            _context.Pessoas.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/Pessoas/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Pessoa value)
        {
            var pessoaDb = _context.Pessoas.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if(pessoaDb == null)
            {
                return BadRequest();
            }

            _context.Pessoas.Attach(value);
            _context.Entry(value).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
