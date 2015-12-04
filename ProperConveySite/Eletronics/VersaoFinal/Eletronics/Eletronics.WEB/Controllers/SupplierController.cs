using Eletronicos.Model;
using Eletronics.WEB.Business;
using Eletronics.WEB.Models;
using Newtonsoft.Json;
using SupplierService.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Eletronics.WEB.Controllers
{
    public class SupplierController : Controller
    {
        private Mapper mapper = new Mapper();
        private SupplierBO supplierBO = new SupplierBO();

        private IList<SupplierScreenModel> FindAllSupplierScreenModels()
        {
            return this.supplierBO.FindAll().Select(supplier => mapper.SupplierToSupplierScreenModel(supplier)).ToList();
        }


        private IList<SupplierTypeScreenModel> FindAllSupplierTypeScreenModel()
        {
            return this.supplierBO.FindAllSupplierTypes().Select(supplierType => mapper.SupplierTypeToSupplierTypeScreen(supplierType)).ToList();
        }


        public ActionResult Consultar()
        {

            return View("ConsultaFornecedor", new ChangeSupplierModel()
            {
                Suppliers = this.FindAllSupplierScreenModels(),
                SupplierTypes = this.FindAllSupplierTypeScreenModel()
            });
        }

        public ActionResult Cadastrar()
        {

            return View("CadastroFornecedor", this.FindAllSupplierTypeScreenModel());
        }

        public ActionResult Excluir()
        {


            return View("ExclusaoFornecedor", new ChangeSupplierModel()
            {
                Suppliers = this.FindAllSupplierScreenModels(),
                SupplierTypes = this.FindAllSupplierTypeScreenModel()
            });
        }


        [HttpPost]
        public PartialViewResult Pesquisar(Supplier supplier)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();

            DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(IList<Supplier>));

            IList<Supplier> suppliers;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:50821/SupplierService.svc/SupplierServices/FindSuppliersByFilter");

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(supplier);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                suppliers = JsonConvert.DeserializeObject<List<Supplier>>(
               (new JavaScriptSerializer().Serialize(jsonSerializer.ReadObject(streamReader.BaseStream))));
            }

            return PartialView("SupplierQuery", suppliers.Select(s =>
            mapper.SupplierToSupplierScreenModel(s)).ToList());
        }

        [HttpPost]
        public JsonResult EfetivarCadastro(Supplier supplier)
        {
            try
            {
                supplierBO.Add(supplier);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Modificar()
        {
            return View("ModificaFornecedor", new ChangeSupplierModel()
            {
                Suppliers = this.FindAllSupplierScreenModels(),
                SupplierTypes = this.FindAllSupplierTypeScreenModel()
            });
        }

        [HttpPost]
        public JsonResult ConfirmarAlteracao(Supplier supplier)
        {
            try
            {
                supplierBO.Update(supplier);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConfirmarExclusao(Supplier supplier)
        {
            try
            {
                supplierBO.Remove(supplier);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }

}