using System;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Locacao
    {
        public long Id { get; set; }
        
        [Display(Name = "Id do Cliente")]
        public long Idcliente { get; set; }

        
        [Display(Name = "Id do Livro")]
        public long Idlivro { get; set; }


        [Required(ErrorMessage = "O campo Data da Locação é obrigatório")]
        [Display(Name = "Data da Locação")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }


        [Required(ErrorMessage = "O campo Data de Entrega é obrigatório")]
        [Display(Name = "Data de Entrega")]
        [DataType(DataType.Date)]
        public DateTime Entrega { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório")]
        [Display(Name = "Preço")]
        public double Preco { get; set; }


        [Display(Name = "Cliente")]
        public string NomeCliente { get; set; }


        [Display(Name = "Livro")]
        public string NomeLivro { get; set; }

    }
}