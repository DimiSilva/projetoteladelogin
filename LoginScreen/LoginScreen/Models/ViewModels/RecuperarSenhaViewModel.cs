using System.ComponentModel.DataAnnotations;

namespace LoginScreen.Models.ViewModels
{
    public class RecuperarSenhaViewModel
    {
        [Required(ErrorMessage = "É necessário informar seu email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O email digitado é inválido")]
        public string email { get; set; }
    }
}
