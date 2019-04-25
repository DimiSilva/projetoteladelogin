using System.ComponentModel.DataAnnotations;

namespace LoginScreen.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome de usuário")]
        [StringLength(16, MinimumLength = 4, ErrorMessage ="O nome de usuário precisa ter entre {2} e {1} caracteres")]
        [Required(ErrorMessage = "É necessário informar seu usuário")]
        public string nomeDeUsuario { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "É necessário informar seu nome")]
        public string nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "É necessário informar seu email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="É necessário informar um email válido")]
        public string email { get; set; }

        [StringLength(20,MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caracteres")]
        [Required(ErrorMessage = "É necessário informar uma senha")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        public bool confirmado { get; set; }
        public Usuario()
        {

        }

        public Usuario(string nomeDeUsuario, string nome, string email, string senha)
        {
            this.id = id;
            this.nomeDeUsuario = nomeDeUsuario;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.confirmado = true;
        }
        public Usuario(string nomeDeUsuario, string nome, string email, string senha, bool confirmado)
        {
            this.nomeDeUsuario = nomeDeUsuario;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.confirmado = confirmado;
        }
    }
}
