using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthosWPF.Model
{
    class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public Condominio Condominio { get; set; }

    }

    class Condominio
    {
        public string NomeCondominio{ get; set; }
        public Administradora Administradora { get; set; }

    }

    class Administradora
    {
        public string NomeAdministradora { get; set; }
    }

    class Email
    {
        public string De { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public string Para { get; set; }
    }
}

