using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {
        private readonly WFConFinDbContext _context;

        public CidadeController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidades()
        {
            try
            {
                var result = _context.Cidade.Include(x => x.Estado).ToList();
                return Ok(result);

            } catch (Exception e)
            {
                return BadRequest($"Erro na listagem de cidades. Exceção: {e.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostCidades([FromBody] Cidade cidade)
        {
            try
            {
                await _context.Cidade.AddAsync(cidade);

                var valor = await _context.SaveChangesAsync();

                if(valor == 1)
                {
                    return Ok("Sucesso! Cidade incluída.");
                }
                else
                {
                    return BadRequest("Erro! Cidade não incluída");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro! Cidade não incluída. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Sucesso! Cidade atualizada com sucesso.");

                } else
                {
                    return BadRequest("Erro! A cidade não foi atualizada.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro! A cidade não foi atualizada. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade([FromRoute] Guid id)
        {
            try
            {
                Cidade cidade = await _context.Cidade.FindAsync(id);

                if (cidade != null)
                {
                    _context.Cidade.Remove(cidade);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Sucesso! Cidade excluída.");

                    } else
                    {
                        return BadRequest("Erro! A cidade não foi excluída.");

                    }

                } else
                {
                    return NotFound("Erro! A cidade não existe.");
                }

            } catch (Exception e)
            {
                return BadRequest($"Erro! A cidade não foi excluída. Exceção: {e.Message}");
            }
        }

        
        /* Consulta cidade */

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaCidade([FromRoute] Guid id)
        {
            try
            {
                var cidade = await _context.Cidade.FindAsync(id);

                if (cidade != null)
                {

                    _context.Cidade.ToList();
                    return Ok(cidade);

                } else
                {

                    return NotFound("Erro! Cidade não existe.");

                }

            } catch (Exception e)
            {
                return BadRequest($"Erro! Não foi possível consultar a cidade. Exceção: {e.Message}");
            }
        }

        // Pesquisa cidade

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> PesquisaCidade([FromQuery] string valor)
        {
            try
            {
                // Query Criteria

                var lista = from o in _context.Cidade.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper()) 
                            ||
                            o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);

            } catch (Exception e)
            {
                return BadRequest($"Erro ao pesquisar cidade(s)! Exceção: {e.Message}");
            }
        }

        
        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetPaginacaoCidade([FromQuery] string valor, int skip, int take, bool ordemDesc)
        
        {
            
            try
            {

                // Query Criteria

                var lista = from o in _context.Cidade.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
                            select o;

                if (ordemDesc)
                {

                    lista = from o in lista
                            orderby o.Nome descending
                            select o;

                } else
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

                var paginacaoResponse = new PaginacaoResponse<Cidade>(lista, quantidade, skip, take);

                return Ok(paginacaoResponse);

            } catch (Exception e)
            {
                return BadRequest($"Erro durante a pesquisa da cidade. Exceção: {e.Message}");
            }

        }

    }
}
