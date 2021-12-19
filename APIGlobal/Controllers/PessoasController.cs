using APIGlobal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGlobal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : Controller
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

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

        [HttpGet("listar-pessoas")]
        public List<Pessoa> listarPessoas()
        {
            return pessoas;
        }

        [HttpGet("consultar-codigo/{id}")]
        public Pessoa consultarCodigoPessoa([FromRoute] int id)
        {
            return pessoas.Find(x => x.Codigo == id);
        }

        [HttpGet("consultar-uf/{uf}")]
        public List<Pessoa> consultarUfPessoa([FromRoute] string uf)
        {
            return pessoas.FindAll(x => x.Uf == uf);
        }

        [HttpPost("inserir-pessoa/{nome}/{cpf}/{uf}/{data}")]
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

        [HttpPost("atualizar-pessoa/{nome}/{cpf}/{uf}/{data}")]
        public Pessoa atualizarPessoa([FromRoute] Pessoa request)
        {
            if (tratamentoDeDadosSimples(request.Nome, request.Cpf, request.Uf, request.DataDeNascimento))
            {
                Pessoa pessoaAtualizada = pessoas.Find(x => x.Codigo == request.Codigo);
                if (pessoaAtualizada != null)
                {
                    pessoas[request.Codigo] = pessoaAtualizada;
                    return pessoaAtualizada;
                }
            }
            return null;
        }

        [HttpDelete("excluir-pessoa-id/{id}")]
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
