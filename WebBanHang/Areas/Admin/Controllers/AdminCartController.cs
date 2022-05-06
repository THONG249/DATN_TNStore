using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;
using PagedList.Mvc;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{

    public class AdminCartController : BaseController
    {
        TNSTOREEntities objTNStoreEntity = new TNSTOREEntities();
        // GET: Admin/AdminCart
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
          
            var lstOrder = new List<Order>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                lstOrder = objTNStoreEntity.Orders.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                lstOrder = objTNStoreEntity.Orders.ToList();
            }
            ViewBag.currentFilter = SearchString;
            int pagesize = 4;
            int PageNumber = (page ?? 1);

            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();

            return View(lstOrder.ToPagedList(PageNumber, pagesize));
        
    }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            var objProduct = objTNStoreEntity.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Order obj)
        {
            
            objTNStoreEntity.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            objTNStoreEntity.SaveChanges();
            return RedirectToAction("Index");
        }
        // Xóa đơn hàng
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objTNStoreEntity.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Delete(Order objPro)
        {
            var objOrder = objTNStoreEntity.Orders.Where(n => n.Id == objPro.Id).FirstOrDefault();
            var objOrderIdCount = objTNStoreEntity.OrderDetails.Where(s => s.OrderId == objPro.Id).Count();

            for(int i=0; i< objOrderIdCount; i++)
            {
               objTNStoreEntity.OrderDetails.Where(s => s.OrderId == objPro.Id).FirstOrDefault();
                objTNStoreEntity.OrderDetails.Remove(objTNStoreEntity.OrderDetails.Where(s => s.OrderId == objPro.Id).FirstOrDefault());
                objTNStoreEntity.SaveChanges();
            }
            objTNStoreEntity.Orders.Remove(objOrder);
            objTNStoreEntity.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            CTDONHANG obj = new CTDONHANG();
            //Lấy đơn hàng theo id
            obj.Order = objTNStoreEntity.Orders.Where(n => n.Id == Id).FirstOrDefault();
            int idUser = (int)obj.Order.UserId;//gán id user == idUsser của bảng order
            obj.User = objTNStoreEntity.Users.Where(n => n.Id == idUser).ToList();//Lấy danh dách người dùng
            obj.ListOderDetail = objTNStoreEntity.OrderDetails.Where(p => p.OrderId == Id).ToList();//danh dách chi tiết đơn hàng theo id
            var lstPro = objTNStoreEntity.Products.ToList();         
            var ListName =(from a in lstPro join b in obj.ListOderDetail
                               on a.Id equals b.ProductId
                         select new
                          {
                           a.Name,
                           Gia =  a.PriceDiscount * b.Quantity
                          }
           ).ToList();
            ViewBag.Name = ListName;
            return View(obj);
        }
    }
}