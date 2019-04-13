using System.ComponentModel.DataAnnotations;

namespace LoginScreen.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [StringLength(16, MinimumLength = 4, ErrorMessage ="O nome de usuário precisa ter entre {2} e {1} caracteres")]
        [Required(ErrorMessage = "É necessário informar seu usuário")]
        public string nomeDeUsuario { get; set; }

        [Required(ErrorMessage = "É necessário informar seu nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "É necessário informar seu email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="É necessário informar um email válido")]
        public string email { get; set; }

        [StringLength(20,MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caracteres")]
        [Required(ErrorMessage = "É necessário informar uma senha")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha precisa ter entre {2} e {1} caracteres")]
        [Required(ErrorMessage = "É necessário confirmar sua senha")]
        [DataType(DataType.Password)]
        public string confirmarSenha { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string nomeDeUsuario, string nome, string email, string senha, string confirmarSenha)
        {
            this.id = id;
            this.nomeDeUsuario = nomeDeUsuario;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.confirmarSenha = confirmarSenha;
        }
        public void AlterarNome(string novoNome)
        {
            nome = novoNome;
        }
        public void AlterarSenha(string novaSenha)
        {
            senha = novaSenha;
        }
    }
}
