using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using System.Net;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                string cpf = RemoverNaoDigitos(model.Cpf);

                bool cpfExiste = bo.VerificarExistencia(cpf);

                if (cpfExiste)
                    return Json("Cpf já cadastrado!");

                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    Cpf = cpf
                });

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                string cpf = RemoverNaoDigitos(model.Cpf);

                Cliente clienteExiste = bo.Consultar(model.Id);

                if (clienteExiste == null)
                    return Json("Cliente não encontrado!");
                else if (clienteExiste.Cpf != cpf)
                {
                    bool cpfExiste = bo.VerificarExistencia(cpf);

                    if (cpfExiste)
                        return Json("Cpf já cadastrado!");
                }

                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    Cpf = cpf
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Cpf = cliente.Cpf
                };

                ViewBag.IdCliente = cliente.Id;
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Beneficiario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BoBeneficiario bo = new BoBeneficiario();
            var beneficiarios = bo.Listar(id.Value);

            if (beneficiarios == null)
            {
                return HttpNotFound();
            }

            List<BeneficiarioModel> model = null;

            if (beneficiarios != null)
            {
                model = new List<BeneficiarioModel>();
                foreach (var beneficiario in beneficiarios)
                {
                    model.Add(new BeneficiarioModel()
                    {
                        Id = beneficiario.Id,
                        Cpf = beneficiario.Cpf,
                        IdCliente = beneficiario.IdCliente,
                        Nome = beneficiario.Nome,
                    });
                }
            }

            return PartialView("Beneficiario", model);
        }

        [HttpGet]
        public ActionResult EditBeneficiario(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BoBeneficiario bo = new BoBeneficiario();
            var beneficiarios = bo.Listar(id.Value);

            if (beneficiarios == null)
            {
                return HttpNotFound();
            }

            List<BeneficiarioModel> model = null;

            if (beneficiarios != null)
            {
                model = new List<BeneficiarioModel>();
                foreach (var beneficiario in beneficiarios)
                {
                    model.Add(new BeneficiarioModel()
                    {
                        Id = beneficiario.Id,
                        Cpf = beneficiario.Cpf,
                        IdCliente = beneficiario.IdCliente,
                        Nome = beneficiario.Nome,
                    });
                }
            }

            return PartialView("Beneficiario", model);
        }

        [HttpGet]
        public ActionResult DeleteBeneficiario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BoBeneficiario bo = new BoBeneficiario();
            var beneficiarioExiste = bo.Consultar(id.Value);

            if (beneficiarioExiste == null)
            {
                return HttpNotFound();
            }

            bo.Excluir(id.Value);

            return RedirectToAction("Alterar", new { id = beneficiarioExiste.IdCliente });
        }

        private string RemoverNaoDigitos(string texto)
        {
            return new string(texto.ToCharArray()
                             .Where(c => char.IsDigit(c)).ToArray());
        }
    }
}