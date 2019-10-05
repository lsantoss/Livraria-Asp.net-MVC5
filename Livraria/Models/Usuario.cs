using System;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Usuario
    {
        public long Id { get; set; }


        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [StringLength(50, MinimumLength = 1)]
        public string Login { get; set; }


        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 1)]
        public string Senha { get; set; }

  
        public DateTime Validade { get; set; }


        [Required(ErrorMessage = "O campo Privilegio é obrigatório")]
        public int Privilegio { get; set; }
    }
}