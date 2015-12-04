using ClientService.Business;
using Eletronicos.Data;
using Eletronicos.Model;
using Eletronics.WEB.Business;
using Eletronics.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eletronics.WEB.Controllers
{
    public class ClientController : Controller
    {

        private ClientBO clientBO = new ClientBO();
        private Mapper mapper = new Mapper();

        private IList<ClientScreenModel> FindAllClientScreenModel()
        {

            return this.clientBO.FindAll().Select(client => mapper.ClientToClientScreen(client)).ToList();
        }
        private IList<ClientTypeScreenModel> FindAllClientTypeScreenModel()
        {
            return this.clientBO.FindAllClientTypes().Select(clientType => mapper.ClientTypeToClientTypeScreen(clientType)).ToList();
        }

        public ActionResult Consultar()
        {

            return View("ConsultaCliente", new ChangeClientModel()
            {
                Clients = this.FindAllClientScreenModel(),
                ClientTypes = this.FindAllClientTypeScreenModel()
            });
        }

        public ActionResult Cadastrar()
        {
            return View("CadastroCliente", this.FindAllClientTypeScreenModel());
        }

        public ActionResult Excluir()
        {

            return View("ExclusaoCliente", new ChangeClientModel()
            {
                Clients = this.FindAllClientScreenModel(),
                ClientTypes = this.FindAllClientTypeScreenModel()
            });
        }


        [HttpPost]
        public PartialViewResult Pesquisar(Client client)
        {
            IList<Client> clientes = clientBO.FindClientsByFilter(client);
            return PartialView("ClientQuery", clientes.Select(c =>
            mapper.ClientToClientScreen(c)).ToList());
        }

        [HttpPost]
        public JsonResult EfetivarCadastro(Client client)
        {
            try
            {
                clientBO.Add(client);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Modificar()
        {
            return View("ModificarCliente", new ChangeClientModel()
            {
                Clients = this.FindAllClientScreenModel(),
                ClientTypes = this.FindAllClientTypeScreenModel()
            });
        }

        [HttpPost]
        public JsonResult ConfirmarAlteracao(Client client)
        {
            try
            {
                clientBO.Update(client);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConfirmarExclusao(Client client)
        {
            try
            {
                clientBO.Remove(client);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }



    }

}
