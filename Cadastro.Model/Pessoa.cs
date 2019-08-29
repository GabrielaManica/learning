using System;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório informar email válido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Display(Name = "Data de nascimento")]
        //https://stackoverflow.com/questions/5252979/assign-format-of-datetime-with-data-annotations
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
    }
}
