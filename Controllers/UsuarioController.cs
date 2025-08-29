using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Linq.Expressions;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    //anotations

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {

        // contexto do banco de dados

        private readonly WFConFinDbContext _context;

        //Construtor com o parâmetro para o contexto do Banco de Dados

        public UsuarioController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                var result = await _context.Usuario.ToListAsync();

                return Ok(result);

            } catch (Exception e)
            {

                return BadRequest($"Erro ao consultar usuário. Exceção: {e.Message}. ");

            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {

            try
            {
                var listUsuario = _context.Usuario.Where(x => x.Login == usuario.Login).ToList();

                if (listUsuario.Count > 0)
                {

                    return BadRequest("Erro! Este nome de usuário já existe. Inclua outro nome de usuário e tente novamente.");

                }

                await _context.Usuario.AddAsync(usuario);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {

                    return Ok("Sucesso! O usuário foi adicionado com sucesso!");

                } else
                {

                    return BadRequest("Erro! Não foi possível salvar o usuário.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível salvar o usuário. Exceção: {e.Message}.");

            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {

            try
            {

                _context.Usuario.Update(usuario);

                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {

                    return Ok("Sucesso! Usuário alterado com sucesso.");

                } else
                {

                    return BadRequest("Não foi possível atualizar as informações deste usuário.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível alterar as informações deste usuário. Exceção: {e.Message}.");

            }

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
        {
            try
            {

                Usuario usuario = await _context.Usuario.FindAsync(id);

                if (usuario != null)
                {

                    _context.Usuario.Remove(usuario);

                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {

                        return Ok("Sucesso! Usuário excluído com sucesso.");

                    }
                    else
                    {

                        return BadRequest("Erro! Não foi possívele excluir este usuário");

                    }

                }
                else
                {

                    return NotFound("Este usuário não existe. Por favor, confira os dados e tente novamente.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possívele excluir este usuário. Exceção: {e.Message}.");

            }

        }

        // Consulta usuário

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultaUsuario([FromRoute] Guid id) 
        {

            try
            {

                Usuario usuario = await _context.Usuario.FindAsync(id);

                if (usuario != null)
                {
                    await _context.Usuario.ToListAsync();
                    return Ok(usuario);

                } else
                {

                    return NotFound("Este usuário não existe. Confira os dados e tente novamente.");

                }

            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível consultar este usuário.Exceção: {e.Message}.");

            }

        }

        // Pesquisar usuario

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetPesquisaUsuario([FromQuery] string valor)
        {

            try
            {

                var lista = from o in _context.Usuario.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            ||
                            o.Login.ToUpper().Contains(valor.ToUpper())

                            select o;

                return Ok(lista);


            } catch (Exception e)
            {

                return BadRequest($"Erro! Não foi possível pesquisar os usuários. Exceção: {e.Message}.");

            }

        }

    }
}
