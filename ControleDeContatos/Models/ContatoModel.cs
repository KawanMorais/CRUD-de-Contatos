
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do Contato")]
        [EmailAddress(ErrorMessage = "O E-mail informado é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do Contato")]
        [Phone(ErrorMessage = "O Celular informado é inválido")]
        public string Celular { get; set; }

     
        public int? UsuarioId { get; set; }

     
        public UsuarioModel? Usuario { get; set; }
    }
}
