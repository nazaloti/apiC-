using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {
        private readonly WFConFinDbContext _context;

        public EstadoController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            try
            {
                var result = _context.Estado.ToList();

                return Ok(result);

            }catch(Exception e)
            {
                return BadRequest($"Erro na listagem de estados. Exceção: {e.Message}");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> PostEstado([FromBody] Estado estado)
        {
            try
            {
                await _context.Estado.AddAsync(estado);

                var valor = await _context.SaveChangesAsync();
                if(valor == 1)
                {
                    return Ok("Sucesso! Estado incluído.");
                }
                else
                {
                    return BadRequest("Erro, estado não incluído.");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, estado não incluído. Exceção: {e.Message}");
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Update(estado);

                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso! Estado alterado.");
                }
                else
                {
                    return BadRequest("Erro, estado não alterado.");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, estado não alterado. Exceção: {e.Message}");
            }

        }

        [HttpDelete("{sigla}")]
        public async Task<IActionResult> DeleteEstado([FromRoute] string sigla)
        {
            try
            {

                var estado = await _context.Estado.FindAsync(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    _context.Estado.Remove(estado);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Sucesso! Estado excluído.");
                    }
                    else
                    {
                        return BadRequest("Erro! Estado não excluído");
                    }

                }
                else
                {
                    return NotFound("Erro! Estado não existe.");
                }

            }

            catch (Exception e)
            {
                return BadRequest($"Erro, estado não deletado. Exceção: {e.Message}");
            }

        }

        [HttpGet("{sigla}")]
        public async Task<IActionResult> GetEstado([FromRoute] string sigla)
        {
            try
            {

                var estado = await _context.Estado.FindAsync(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {

                    return Ok(estado);

                }
                else
                {
                    return NotFound("Erro! Estado não existe.");
                }

            }

            catch (Exception e)
            {
                return BadRequest($"Erro, consulta de estado. Exceção: {e.Message}");
            }

        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetEstadoPesquisa([FromQuery] string valor)
        {
            try
            {
                // Qurey Criteria
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                            || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                // Entity

                /*
                 * _context.Estado
                        .Where(o => o.Sigla.ToUpper().Contains(valor.ToUpper())
                                      || o.Nome.ToUpper().Contains(valor.ToUpper())
                              )
                        .ToList(); 

                */

                // Expression

                /*
                 * 
                Expression<Func<Estado, bool>> expressao = o => true;
                expressao = o => o.Sigla.ToUpper().Contains(valor.ToUpper())
                                  || o.Nome.ToUpper().Contains(valor.ToUpper());

                lista = _context.Estado.Where(expressao).ToList();

                */

                return Ok(lista);   

                /* 
                 
                    Select * from estado where upper(sigla) like upper('%valor%') or upper(nome) like ('%valor%')

                 */

            }

            catch (Exception e)
            {
                return BadRequest($"Erro, pesquisa de estado. Exceção: {e.Message}");
            }

        }

        [HttpGet("Paginacao")]

        /* 
         * 
           Skip = ignorar informações de registros dentro do SQL;
           Take = Quantidade de informações que você quer retornar a partir do skip;
           valor = informação que será pesquisada;
           ordem = como os dados serão ordenados;
           
         */

        public async Task<IActionResult> GetEstadoPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                // Qurey Criteria
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                            || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                if(ordemDesc)
                {
                    lista = from o in lista
                            orderby o.Nome descending
                            select o;
                }
                else
                {
                    lista = from o in lista
                            orderby o.Nome ascending
                            select o;
                }

                var quantidade = lista.Count();

                lista = lista
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Estado>(lista, quantidade, skip, take);

                return Ok(paginacaoResponse);

            }

            catch (Exception e)
            {
                return BadRequest($"Erro, pesquisa de estado. Exceção: {e.Message}");
            }

        }

    }
}
