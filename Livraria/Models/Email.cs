using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Email
    {
        public string From { get; set; }

        public string To { get; set; }

        [Required(ErrorMessage = "Informe o Título do Email")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Informe a Mensagem do Email")]
        public string Body { get; set; }
    }
}