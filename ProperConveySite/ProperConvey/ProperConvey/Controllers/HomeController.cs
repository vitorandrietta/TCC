using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProperConvey.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(Models.MedicoLogin medicoLogin)
        {
            ADO.ADOMedicoMock ValidacaoMedico = new ADO.ADOMedicoMock();
            if (ValidacaoMedico.validarLogin(medicoLogin.Username, medicoLogin.Password))
            {
                return Json(new { success = true, sucessText = "Bem vindo " + medicoLogin.Username },
                JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(new { success = false, exceptionText = "Senha ou usuário incorreto(s)" },
                JsonRequestBehavior.AllowGet);

            }
        }
    }
}