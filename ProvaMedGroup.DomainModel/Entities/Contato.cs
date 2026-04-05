using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaMedGroup.DomainModel.Entities
{
    public class Contato : EntityBase
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public char Sexo { get; set; }

    }
}