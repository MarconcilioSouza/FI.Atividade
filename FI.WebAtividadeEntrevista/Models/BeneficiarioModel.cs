using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}