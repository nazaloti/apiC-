using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WFConfin.Models;

namespace WFConfin.Services
{
    public class TokenService
    {

        private readonly IConfiguration _configuration;

        // Método Construtor
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método para gerar o token
        // Recebe os dados do usuário por parâmetro
        public string GerarToken(Usuario usuario)
        {

            // Cria um objeto para gerar os tokens através da bibliotecas jwt

            var tokenHandler = new JwtSecurityTokenHandler();

            // Converte a string em Bytes e acessa a chave que foi criada no appsettings.json para realizar a criptografia do token

            var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("Chave").Get<string>());

            // Token descriptor == Descrição do nosso token e os dados que nós queremos montar dentro do token

            var tokenDescritor = new SecurityTokenDescriptor
            {
                // Claims = Os dados que queremos do Usuário (nome e função do usuário)

                Subject = new ClaimsIdentity(
                         new Claim[]
                         {
                             new Claim(ClaimTypes.Name, usuario.Login.ToString()),
                             new Claim(ClaimTypes.Role, usuario.Funcao.ToString()),
                         }
                ),

                // Expires: Quando o código ira expirar
                // UtcNow = hora agora

                Expires = DateTime.UtcNow.AddHours(2),

                // SigningCredentials == Informações sobre assinaturas de credenciais

                SigningCredentials = new SigningCredentials 
                
                (
                    // Cria um objeto fazendo a assinatura da nossa chave
                    // SymmetricSecurityKey(chave) == O parâmetro precisa ser passado em Bytes, por isso houve a conversão no início do código
                    // SecurityAlgorithms == Precisamos passasr qual o algoritmo que nós queremos usar para fazer a criptografia

                    new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature

                )

            };

            // Variavel que será usada para criar o token

            var token = tokenHandler.CreateToken(tokenDescritor);

            // Escrevendo os dados do token - Retornando os dados em string do token que foi gerado

            return tokenHandler.WriteToken(token);
            
      
        }
    }
}
