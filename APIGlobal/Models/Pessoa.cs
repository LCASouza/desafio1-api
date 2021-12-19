namespace APIGlobal.Models
{
    public class Pessoa
    {
        private int codigo;
        private string nome, cpf, uf, dataDeNascimento;

        public int Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                codigo = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        public string Cpf
        {
            get
            {
                return cpf;
            }
            set
            {
                cpf = value;
            }
        }

        public string Uf
        {
            get
            {
                return uf;
            }
            set
            {
                uf = value;
            }
        }

        public string DataDeNascimento
        {
            get
            {
                return dataDeNascimento;
            }
            set
            {
                dataDeNascimento = value;
            }
        }

        public Pessoa(int codigo, string nome, string cpf, string uf, string dataDeNascimento)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.cpf = cpf;
            this.uf = uf;
            this.dataDeNascimento = dataDeNascimento;
        }
    }
}
