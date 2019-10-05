using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Livro
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, MinimumLength = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Autor é obrigatório")]
        [StringLength(50, MinimumLength = 1)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "O campo Edicao é obrigatório")]
        [Display(Name = "Edição")]
        [Range(1, 100)]
        public int Edicao { get; set; }

        [Required(ErrorMessage = "O campo Isbn é obrigatório")]
        [StringLength(17, MinimumLength = 17)]
        [Display(Name = "Isbn")]
        public string Inbs { get; set; }

        public string Imagem { get; set; }

        public long VezesLocado { get; set; }
    }
}