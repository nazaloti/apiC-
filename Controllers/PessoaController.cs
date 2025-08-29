using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {

        private readonly WFConFinDbContext _context;

        public PessoaController(WFConFinDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var result = await _context.Pessoa.ToListAsync();
            
                return Ok(result);

            } catch (Exception e)

            {
                return BadRequest($"Erro na listagem de pessoas. Exceção: {e.Message}");
            }
        
        }


        [HttpPost]
        public async Task<IActionResult> PostPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                await _context.Pessoa.AddAsync(pessoa);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    
                    return Ok("Sucesso! Pessoa incluída.");

                } else
                {

                    return BadRequest("Erro! Esta pessoa não foi gravada");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Esta pessoa não foi gravada. Exceção: {e.Message}");

            }
        }


        [HttpPut]
        public async Task<IActionResult> PutPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                _context.Pessoa.Update(pessoa);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {

                    return Ok("Sucesso! Os dados da pessoa foram alterados.");

                } else
                {

                    return BadRequest("Erro! Os dados da pessoa não foram alterados.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Os dados da pessoa não foram alterados. Exceção: {e.Message}");

            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePessoa([FromRoute] Guid id)
        {
            try
            {

                var pessoa = await _context.Pessoa.FindAsync(id);

                if (pessoa != null)
                {

                    _context.Pessoa.Remove(pessoa);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {

                        return Ok("Sucesso! Esta pessoa foi deletada com sucesso.");

                    } else
                    {

                        return BadRequest("Erro! Esta pessoa não foi deletada.");

                    }

                } else
                {

                    return NotFound("Erro! Esta pessoa não existe.");

                }

            } catch (Exception e)
            {
                return BadRequest($"Erro! Esta pessoa não foi deletada. Exceção: {e.Message}");
            }

        }

        // Consultar pessoa

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConsultaPessoa([FromRoute] Guid id)

        {

            try
            {
                var pessoa = await _context.Pessoa.FindAsync(id);

                if (pessoa != null)
                {
                    _context.Pessoa.ToList();

                    return Ok(pessoa);

                } else
                {

                    return NotFound("Erro! Esta pessoa não existe");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível consultar esta pessoa. Exceção: {e.Message}");
                
            }

        }

        // Pesquisa pessoa

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetPesquisaPessoa([FromQuery] string valor)
        {
            try
            {
                //  Query Criteria 

                var lista = from o in _context.Pessoa.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.Telefone.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.Email.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);

            } catch (Exception e)
            {
                return BadRequest($"Erro ao pesquisar pessoas. Exceção: {e.Message}");
            }
        }


        // Paginação 

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetPaginacaoPessoa([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {

            try
            {
                // Query criteria

                var lista = from o in _context.Pessoa.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.Telefone.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.Email.ToUpper().Contains(valor.ToUpper())
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

                var paginacaoResponse = new PaginacaoResponse<Pessoa>(lista, quantidade, skip, take);

                return Ok(paginacaoResponse);

            } catch (Exception e)
            {

                return BadRequest($"Erro durante a pesquisa da pessoa. Exceção: {e.Message}");

            }

        }

    }
}
