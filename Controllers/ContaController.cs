using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ContaController : Controller
    {

        private readonly WFConFinDbContext _context;

        public ContaController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetConta()
        {

            try
            {
                var result = await _context.Conta.ToListAsync();

                return Ok(result);

            } catch (Exception e)
            {

                return BadRequest($"Erro na consulta da conta. Exceção: {e.Message}");

            }

        }

        [HttpPost]
        public async Task<IActionResult> PostConta([FromBody] Conta conta)
        {

            try
            {

                await _context.Conta.AddAsync(conta);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {

                    return Ok("Sucesso! Os dados da conta foram inseridos com sucesso");

                } else
                {

                    return BadRequest("Erro! Não foi possível cadastrar esta conta.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível cadastrar esta conta. Exceção: {e.Message}");

            }

        }

        [HttpPut]
        public async Task<IActionResult> PutConta([FromBody] Conta conta)
        {

            try
            {

                _context.Conta.Update(conta);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {

                    return Ok("Sucesso! Os dados da conta foram atualizados com sucesso!");

                } else
                {

                    return BadRequest("Erro! Os dados da conta não foram atualizados com sucesso.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível concluir a atualização dos dados da conta. Exceção: {e.Message}");

            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteConta([FromRoute] Guid id)
        {

            try
            {

                Conta conta = await _context.Conta.FindAsync(id);

                if (conta != null)
                {

                    _context.Conta.Remove(conta);

                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {

                        return Ok("Suceso! Conta deletada com sucesso!");

                    }
                    else
                    {

                        return BadRequest("Erro! Não foi possível concluir a exclusão da conta.");

                    }

                } else
                {

                    return NotFound("Erro! Está conta não existe.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível deletar a conta. Exceção: {e.Message}");

            }

        }

        // Consulta conta

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConsultaConta([FromRoute] Guid id)
        {

            try
            {

               var conta = await _context.Conta.FindAsync(id);

                if (conta != null)
                {

                    await _context.Conta.ToListAsync();
                    return Ok(conta);

                } else
                {

                    return NotFound("Erro! Não foi possível consultar esta conta");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível consultar esta conta. Exceção: {e.Message}");

            }
          
        }


        // Pesquisa conta

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetPesquisaConta([FromQuery] string valor)
        {

            try
            {
                // Query Criteria

                var lista = from o in _context.Conta.Include(o => o.pessoa).ToList()
                            where o.Descricao.ToUpper().Contains(valor.ToUpper())
                            || 
                            o.pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível pesquisar esta conta. Exceção: {e.Message}");

            }

        }



        // Paginação Conta

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetPaginacaoConta([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {

            try
            {

                var lista = from o in _context.Conta.Include(o => o.pessoa).ToList()
                            where o.Descricao.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                if (ordemDesc)
                {

                    lista = from o in lista
                            orderby o.Descricao descending
                            select o;

                }
                else
                {

                    lista = from o in lista
                            orderby o.Descricao ascending
                            select o;

                }

                var quantidade = lista.Count();

                lista = lista
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Conta>(lista, quantidade, skip, take);

                return Ok(paginacaoResponse);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível realizar a paginação. Exceção: {e.Message}");

            }
        }


        // Pesquisa pelo IdPessoa

        [HttpGet("Pessoa/{pessoaId}")]
        public async Task<IActionResult> GetContasPessoa([FromRoute] Guid pessoaId)
        {

            try
            {
                // Query Criteria

                var lista = from o in _context.Conta.Include(o => o.pessoa).ToList()
                            where o.PessoaId == pessoaId
                            select o;

                return Ok(lista);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível pesquisar esta conta. Exceção: {e.Message}");

            }

        }
    }
}
