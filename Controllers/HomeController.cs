using Microsoft.AspNetCore.Mvc;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private static List<Estado> listaEstados = new List<Estado>();


        [HttpGet("estado")]
        public IActionResult GETEstados()
        {
            return Ok(listaEstados);
        }

        [HttpPost("estado")]
        public IActionResult POSTEstado([FromBody] Estado estado)
        {
            listaEstados.Add(estado);
            return Ok("Estado cadastrado.");
        }

        [HttpGet]
        public IActionResult GetInformacao()
        {
            var result = "Retorno em texto";
            return Ok(result); 
        }

        [HttpGet("info2")]
        public IActionResult GetInformacao2()
        {
            var result = "Retorno em texto 2";
            return Ok(result);
        }

        [HttpGet("info3/{valor}")]
        public IActionResult GetInformacao3(string  valor)
        {
            var result = "Retorno em texto 3 - Valor: " + valor;
            return Ok(result);
        }

        [HttpGet("info4/{nome}")]
        public IActionResult GetInformacao4([FromRouteAttribute] string nome)
        {
            var result = "Retorno em texto 4 - Nome " + nome;
            return Ok(result);
        }

        [HttpPost("info5")]
        public IActionResult GetInformacao5([FromQuery] string pais)
        {
            var result = "Retorno em texto 5 - pais " + pais;
            return Ok(result);
        }

        [HttpGet("info6")]
        public IActionResult GetInformacao6([FromHeader] string cidade)
        {
            var result = "Retorno em texto 5 - pais " + cidade;
            return Ok(result);
        }

        [HttpPost("info7")]
        public IActionResult GetInformacao7([FromBody] Corpo corpo)
        {
            var result = "Retorno em texto 7 - Valor " + corpo.valor;
            return Ok(result);
        }

    }

    public class Corpo
    {
        public string valor { get; set; }
    }
}
