using Cadastro.Model;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.API.Data
{
    public class CadastroContext : DbContext
    {
        public CadastroContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Cadastro.db");
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
