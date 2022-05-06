using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
   
    public class TinhTrangGiaoHangController : Controller
    {
        TNSTOREEntities objTNStoreEntity = new TNSTOREEntities();
        // GET: TinhTrangGiaoHang
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int id = int.Parse(Session["Id"].ToString());
                var lisOrder = objTNStoreEntity.Orders.Where(n => n.UserId == id).ToList();
                if(lisOrder.Count == 0)
                {
                    ViewBag.Tb = "Bạn chưa đặt hàng";
                }
                return View(lisOrder);
            }
        }
    }
}