using System.ComponentModel.DataAnnotations;

namespace WFConfin.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo nome deve conter até 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Login deve conter até 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório")]
        [StringLength(20, MinimumLength = 3,  ErrorMessage = "O campo Passaword deve conter até 20 caracteres")]
        public string Passaword { get; set; }

        [Required(ErrorMessage = "O campo Funcao é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Funcao deve conter até 20 caracteres")]
        public string Funcao { get; set; }

    }
}
