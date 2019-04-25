using System.ComponentModel.DataAnnotations;

namespace LoginScreen.Models.ViewModels
{
    public class ConfirmacaoPorEmailViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage ="digite o código de confirmação")]
        public int codigo { get; set; }
    }
}
