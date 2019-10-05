using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Cliente
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, MinimumLength = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Idade é obrigatório")]
        [Range(0, 130)]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório")]
        [StringLength(9, MinimumLength = 8)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [StringLength(10, MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Cpf é obrigatório")]
        [StringLength(14, MinimumLength = 14)]
        public string Cpf { get; set; }
    }
}