using MainCarousel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MainCarousel.Controllers
{
    public class HomeController : Controller
    {
        private Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Carousel()
        {
            using (Context context = new Context())
            {
                var data = context.carousel.SqlQuery("SELECT * FROM Carousel ORDER BY Sira").ToList();
                return View(data);
            }           
        }
        public ActionResult List()
        {
            return View(context.carousel.SqlQuery("SELECT * FROM Carousel ORDER BY Id DESC").ToList());
        }
        public ActionResult CarouselCreate()
        {
            var data = context.carousel.SqlQuery("SELECT * FROM Carousel ORDER BY Sira").ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult CarouselCreate(Carousel c, HttpPostedFileBase uploadfile)
        {
            using (Context context = new Context())
            {
                if (uploadfile.ContentLength > 0)
                {
                    var dt = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Carousel/"), dt);
                    uploadfile.SaveAs(filePath);
                    c.Path = dt;
                }               
                context.carousel.Add(c);
                context.SaveChanges();
                return RedirectToAction("Carousel");
            }            
        }
        public ActionResult CarouselEdit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = context.carousel.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarouselEdit(Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                context.Entry(carousel).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Carousel");
            }
            return View(carousel);
        }
        public ActionResult CarouselDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = context.carousel.Find(id);            
            if (data== null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                context.Entry(data).State = EntityState.Deleted;
                context.SaveChanges();
            }
            if (System.IO.File.Exists(Server.MapPath("~/Images/Carousel/" + data.Path)))
            {
                System.IO.File.Delete(Server.MapPath("~/Images/Carousel/" + data.Path));
                return RedirectToAction("List");
            }
            return View();
        }
    }
}