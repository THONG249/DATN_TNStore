using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        TNSTOREEntities objTNStoreEntity = new TNSTOREEntities();
        public ActionResult Details(int Id)
        {
            var objProduct = objTNStoreEntity.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}