namespace APIGlobal.Models
{
    public class Usuario
    {
        private int id;
        private string username, senha, cargo;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Senha
        {
            get
            {
                return senha;
            }
            set
            {
                senha = value;
            }
        }

        public string Cargo
        {
            get
            {
                return cargo;
            }
            set
            {
                cargo = value;
            }
        }

        public Usuario(int id, string username, string senha, string cargo)
        {
            this.id = id;
            this.username = username;
            this.senha = senha;
            this.cargo = cargo;
        }
    }
}
