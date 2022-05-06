using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class CommentController : Controller
    {
        TNSTOREEntities objTNStoreEntity = new TNSTOREEntities();
        // GET: Comment
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(Comment comment)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            { 
                comment.IdUser = int.Parse(Session["Id"].ToString());
                comment.Day = DateTime.Now;
                objTNStoreEntity.Comments.Add(comment);
                objTNStoreEntity.SaveChanges();
                ViewBag.Cmt = "Phản hồi đã được gửi";
                return RedirectToAction("Index");
              
            }
              
        }
    }
}