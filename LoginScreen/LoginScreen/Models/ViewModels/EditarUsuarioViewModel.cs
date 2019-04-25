using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginScreen.Models.ViewModels
{
    public class EditarUsuarioViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "É necessário informar seu nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "É necessário digitar a nova senha")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caracteres")]
        [DataType(DataType.Password)]
        public string novaSenha { get; set; }
        [Required(ErrorMessage = "É necessário confirmar a nova senha")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caracteres")]
        [DataType(DataType.Password)]
        [Compare("novaSenha", ErrorMessage = "As senhas não batem")]
        public string confirmarNovaSenha { get; set; }
    }
}
