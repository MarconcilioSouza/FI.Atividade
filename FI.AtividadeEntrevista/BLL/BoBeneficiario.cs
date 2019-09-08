using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Beneficiario> Listar(int idCliente)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.Listar(idCliente);
        }
    }
}
