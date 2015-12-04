using Eletronicos.Data;
using Eletronicos.Model;
using Eletronicos.Model.Product;
using Eletronics.WEB.Business;
using Eletronics.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Eletronics.WEB.Controllers
{
    public class ProductController : Controller
    {
        private ProductBO productBO = new ProductBO();
        private Mapper mapper = new Mapper();
        private SupplierDAO supplierDAO =  new SupplierDAO();
        public ActionResult Consultar()
        {
            IList<Supplier> suppliersCore;


            suppliersCore = this.supplierDAO.FindAllSuppliers();

            IList<Product> productsCore = productBO.FindAllProducts();
            return View("ConsultaProduto", new ChangeProductModel()
            {
                products = productsCore.Select(p => mapper.ProductToProductScreenModel(p)).ToList(),
                suppliers = suppliersCore.Select(s => mapper.SupplierToProductSupplier(s)).ToList()
            });
        }

        public ActionResult Cadastrar()
        {
            IList<Supplier> suppliersCore;


            suppliersCore = this.supplierDAO.FindAllSuppliers();

            return View("CadastroProduto", suppliersCore.Select(s => mapper.SupplierToProductSupplier(s)).ToList());

        }

        public ActionResult Excluir()
        {
            IList<Supplier> suppliersCore;

            
            suppliersCore = this.supplierDAO.FindAllSuppliers();
            

            IList<Product> productsCore = productBO.FindAllProducts();
            return View("ExclusaoProduto", new ChangeProductModel()
            {
                products = productsCore.Select(p => mapper.ProductToProductScreenModel(p)).ToList(),
                suppliers = suppliersCore.Select(s => mapper.SupplierToProductSupplier(s)).ToList()
            });
        }


        [HttpPost]
        public PartialViewResult Pesquisar(Product product)
        {
            IList<Product> filteredProducts = productBO.FindProductsByFilter(product);
            return PartialView("ProductQuery", filteredProducts.Select(p => mapper.ProductToProductScreenModel(p)).ToList());

        }

        [HttpPost]
        public JsonResult EfetivarCadastro(Product product)
        {
            try
            {
                productBO.AddProduct(product);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Modificar()
        {
            IList<Supplier> suppliersCore;


            suppliersCore = this.supplierDAO.FindAllSuppliers();
            

            IList<Product> productsCore = productBO.FindAllProducts();
            return View("ModificarProduto", new ChangeProductModel()
            {
                products = productsCore.Select(p => mapper.ProductToProductScreenModel(p)).ToList(),
                suppliers = suppliersCore.Select(s => mapper.SupplierToProductSupplier(s)).ToList()
            });
        }

        [HttpPost]
        public JsonResult ConfirmarAlteracao(Product product)
        {
            try
            {
                productBO.UpdateProduct(product);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConfirmarExclusao(Product product)
        {
            try
            {
                productBO.DeleteProduct(product);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
