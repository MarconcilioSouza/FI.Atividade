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
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.Listar(idCliente);
        }

        /// <summary>
        /// Excluir beneficiario
        /// </summary>
        public void Excluir(long idBeneficiario)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            beneficiario.Excluir(idBeneficiario);
        }

        /// <summary>
        /// Consultar beneficiario
        /// </summary>
        public DML.Beneficiario Consultar(long idBeneficiario)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
           return beneficiario.Consultar(idBeneficiario);
        }
    }
}
