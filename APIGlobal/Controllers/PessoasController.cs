using APIGlobal.Models;
using APIGlobal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGlobal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : Controller
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        Usuario root = new Usuario(1, "root", "master123", "admin");

        private bool tratamentoDeDadosSimples(string nome, string cpf, string uf, string data)
        {
            if (nome.Length >= 1 && cpf.Length == 11 && uf.Length == 2 && data.Length == 8)
            {
                if (pessoas.FindAll(x => x.Cpf == cpf).Count == 0)
                { 
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("login/{username}/{senha}")]
        [AllowAnonymous]
        public string autenticarUsuario([FromRoute] string username, [FromRoute] string senha)
        {
            if (username == root.Username && senha == root.Senha)
            {
                var token = TokenService.GerarToken(root);

                return token.ToString();
            }
            return "Usuario ou Senha Invalidos";
        }

        [HttpGet("listar-pessoas")]
        [Authorize]
        public List<Pessoa> listarPessoas()
        {
            return pessoas;
        }

        [HttpGet("consultar-codigo/{id}")]
        [Authorize]
        public Pessoa consultarCodigoPessoa([FromRoute] int id)
        {
            return pessoas.Find(x => x.Codigo == id);
        }

        [HttpGet("consultar-uf/{uf}")]
        [Authorize]
        public List<Pessoa> consultarUfPessoa([FromRoute] string uf)
        {
            return pessoas.FindAll(x => x.Uf == uf);
        }

        [HttpPost("inserir-pessoa/{nome}/{cpf}/{uf}/{data}")]
        [Authorize]
        public Pessoa inserirPessoa([FromRoute] string nome, [FromRoute] string cpf, [FromRoute] string uf, [FromRoute] string data)
        {
            if (tratamentoDeDadosSimples(nome, cpf, uf, data))
            {
                int codigoNovo = 1;
                if (pessoas.Count > 0)
                {
                    Pessoa ultimaPessoa = pessoas.Last();
                    codigoNovo = ultimaPessoa.Codigo+1;
                }

                Pessoa pessoa = new Pessoa(codigoNovo, nome, cpf, uf, data);
                pessoas.Add(pessoa);
                return pessoa;
            }
            return null;
        }

        [HttpPost("atualizar-pessoa/{codigo}/{nome}/{cpf}/{uf}/{data}")]
        [Authorize]
        public Pessoa atualizarPessoa([FromRoute] int codigo, [FromRoute] string nome, [FromRoute] string cpf, [FromRoute] string uf, [FromRoute] string data)
        {
            if (tratamentoDeDadosSimples(nome, cpf, uf, data))
            {
                if (pessoas.Find(x => x.Codigo == codigo) != null)
                {
                    pessoas[codigo-1] = new Pessoa(codigo, nome, cpf, uf, data);
                    return pessoas[codigo-1];
                }
            }
            return null;
        }

        [HttpDelete("excluir-pessoa-id/{id}")]
        [Authorize]
        public string excluirPessoa([FromRoute] int id)
        {
            if (pessoas.Exists(x => x.Codigo == id))
            {
                int posicaoNaLista = id - 1;
                pessoas.RemoveAll(x => x.Codigo == id);
                return "Pessoa Removida com Sucesso!";
            }
            return "Pessoa Nao Encontrada";
        }

    }
}
