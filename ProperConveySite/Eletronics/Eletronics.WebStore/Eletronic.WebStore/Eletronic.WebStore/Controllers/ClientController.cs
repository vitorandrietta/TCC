using Eletronicos.Model;
using EletronicStore.WEB.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EletronicStore.WEB.Controllers
{
    public class ClientController : Controller
    {
        private ClientBO clientBO = new ClientBO();
        public ActionResult Index()
        {
            return View();
        }



        //[Authorize]
        public ActionResult LoginPage(){
            return View("Login");
        }
        
        [HttpPost]
        public JsonResult Login(ClientLogin clientLogin)
        {

            try
            {
                if (clientBO.login(clientLogin))
                {   
                    FormsAuthentication.SetAuthCookie(clientBO.FindClient(clientLogin).ClientID, false);
                    return Json(new { success = true, sucessText = "Bem vindo " + clientLogin.Username },
                    JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, exceptionText = "Usuário ou senha incorreto(s)" },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, exceptionText = "Erro no processamento interno do servidor" },
                JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPage", "Client");
        }

    }
}
