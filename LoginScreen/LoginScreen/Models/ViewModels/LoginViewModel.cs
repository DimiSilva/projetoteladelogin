using System.ComponentModel.DataAnnotations;

namespace LoginScreen.Models.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(16, MinimumLength = 4, ErrorMessage = "O nome de usuário precisa ter entre {2} e {1} caractéres")]
        [Required(ErrorMessage = "É necessário informar seu usuário")]
        public string nomeDeUsuario { get; set;}
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caractéres")]
        [Required(ErrorMessage = "É necessário informar a senha")]
        public string senha { get; set; }
    }
}
