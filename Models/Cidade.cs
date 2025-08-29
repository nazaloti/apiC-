using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace WFConfin.Models
{
    public class Cidade
    {
        [Key]
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome da cidade deve conter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo Estado deve conter 2 caracteres")]
        public string EstadoSigla { get; set; }

        public Cidade()
        {
            Id = Guid.NewGuid();
        }

        // Relacionamento Entity Framework
        [JsonIgnore]
        public Estado Estado { get; set; }
    }
}
