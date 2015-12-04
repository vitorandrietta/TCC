using Eletronicos.Model;
using Eletronicos.Model.Product;
using Eletronicos.Model.Purchase;
using Eletronics.WEB.Business;
using EletronicStore.WEB.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EletronicStore.WEB.Models;
using Eletronic.WebStore.Mapper;
namespace EletronicStore.WEB.Controllers
{
    public class PurchaseController : Controller
    {
        private Mapper mapper = new Mapper();
        private ProductBO productBO = new ProductBO();
        private PurchaseBO purchaseBO = new PurchaseBO();
        private IList<ProductVM> pagedProducts = null;
        private int numberOfPages;

        public PurchaseController()
        {
            this.UpdatePages();
        }

        private string CurrentUserId()
        {
            return FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
        }
        
        
        private void UpdatePages()
        {
            IList<Product> avaiableProducts = this.productBO.FindProductsByFilter(new Product() { AvaiableQuantity = 1 });
            this.pagedProducts = avaiableProducts.Select(p => mapper.ProductToProductVM(p)).ToList();
            this.numberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.pagedProducts.Count / 4)));
            if (pagedProducts.Count % 4 > 0)
            {
                this.numberOfPages++;
            }
        }

        [Authorize]
        public ActionResult Selector()
        {

            if (this.CurrentUserId() == null || this.CurrentUserId()=="") {

                return RedirectToAction("LoginPage", "Client");
            }
            
            
            return View("Purchase");
        }


        [Authorize]
        [HttpPost]
        public JsonResult CancelPurchase(Purchase purchase)
        {
            try
            {
                if (this.CurrentUserId() == null || this.CurrentUserId() == "")
                {

                    throw new ApplicationException("u aren't logged");
                }
                
                purchaseBO.RemovePurchase(purchase);
                this.UpdatePages();
                return Json(new { success = true, sucessText = "Compra cancelada com êxito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]

        public JsonResult MakePurchase(Purchase purchase)
        {
            try
            {
                if (this.CurrentUserId() == null || this.CurrentUserId() == "")
                {

                    throw new ApplicationException("u aren't logged");
                }

                string userId = this.CurrentUserId(); ;
                purchase.ClientID = userId;
                purchaseBO.RegisterPurchase(purchase);
                this.UpdatePages();
                return Json(new { success = true, sucessText = "Compra Efetuada com êxito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, exceptionText = "occoreu um erro ao efetuar a compra" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult PageChange(int pageNumber)
        {

            if (this.CurrentUserId() == null || this.CurrentUserId() == "")
            {
                return RedirectToAction("LoginPage", "Client");
            
            }

            
            int backwardItens = (pageNumber - 1) * 4;

            if (pageNumber == 1)
            {
                return View("ProductList", new PageVM()
                {
                    products = this.pagedProducts.Take(this.pagedProducts.Count >= 4 ? 4 : this.pagedProducts.Count),
                    currentPage = pageNumber,
                    TotalOfPages = this.numberOfPages
                });
            }


            return View("ProductList", new PageVM()
            {
                products = this.pagedProducts.Skip(backwardItens).
                Take(4),
                currentPage = pageNumber,
                TotalOfPages = this.numberOfPages
            });

        }

        [Authorize]
        public ActionResult ProductList()
        {
            if (this.CurrentUserId() == null || this.CurrentUserId() == "")
            {
                return RedirectToAction("LoginPage", "Client");

            }
            
            return RedirectToAction("PageChange", "Purchase", new { pageNumber = 1 });
        }

        [Authorize]
        public ActionResult MyPurchases()
        {
            if (this.CurrentUserId() == null || this.CurrentUserId() == "")
            {
                return RedirectToAction("LoginPage", "Client");

            }

            string userId = this.CurrentUserId();
            IList<Purchase> clientPurchases = purchaseBO.ClientPurchases(new Client() { CPF = userId });
            return View("MyPurchases", clientPurchases.Select(cp => mapper.PurchaseToProductPurchaseVM(cp)).ToList());
            //Mostrar as compras em um grid view paginado, todos com o botao de exclusao
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPage", "Client");
        }

        

    }


}


