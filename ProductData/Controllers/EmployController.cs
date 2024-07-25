using ProductData.DBconnect;
using ProductData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ProductData.Controllers
{
    public class EmployController : Controller
    {
        // GET: Employ

        StudentDbEntities db=new StudentDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveData(product model,HttpPostedFileBase postfile)
        {
                string extension = Path.GetExtension(postfile.FileName);
                string fname = Path.GetFileName(postfile.FileName);
                string filename = fname.Split('.')[0];
                if (extension == ".PNG" || extension == ".jpg")
                {
                    string Filename = filename + System.DateTime.Now.ToString("yyyymmddhhmmss") + extension;
                    string savepath = Server.MapPath("~/Content/Images/");
                    postfile.SaveAs(savepath + Filename);
                    model.picname = Filename;
                    db.products.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    return Content("Please upload .png or .jpg image");
                }
            return View("Index");
        }
        public ActionResult GetAllRecord()
        {
            var data=db.products.ToList();
            return PartialView("GetAllRecord",data);
        }
        public ActionResult  Edit(int id) {
            var data=db.products.Where(m=>m.id==id).FirstOrDefault();
            return View(data);
        }
        public ActionResult EditData(product model, HttpPostedFileBase postfile)
        {
            if (postfile == null)
            {
                var data = db.products.Where(m => m.id == model.id).FirstOrDefault();
                if(data!=null) 
                {
                    data.name = model.name;
                    data.city= model.city;
                    data.Qty = model.Qty;
                    data.rate = model.rate;
                    data.Total= model.Total;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            else
            {
                string extension=Path.GetExtension(postfile.FileName);
                string fname=Path.GetFileName(postfile.FileName);
                string Filename = fname.Split('.')[0];
                if(extension==".png" || extension == ".jpg")
                {
                    string filename= Filename + extension;
                    string Savepath =Server.MapPath("~/Content/Images/");
                    postfile.SaveAs(Savepath+ filename);

                    var data = db.products.Where(m => m.id == model.id).FirstOrDefault();
                    if(data!=null)
                    {
                        data.name = model.name; 
                        data.city= model.city;
                        data.Qty = model.Qty;
                        data.rate = model.rate;
                        data.Total= model.Total;
                        data.picname= filename;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");

            }
        }
        public ActionResult Delete(int id)
        {
            var data = db.products.Where(m => m.id == id).FirstOrDefault();
            db.products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Jay_id)
        {
            var data = db.products.Find(Jay_id);
            return View(data);
        }
        public ActionResult GetList(string Search,int PageNumber = 1)
        {
            var data=db.products.ToList();
            var prod=from std in db.products select std;
            prod=prod.OrderBy(std => std.name);
            ViewBag.TotalPage = Math.Ceiling(prod.Count() / 4.0);
            ViewBag.pageno = PageNumber;
            data =prod.Skip((PageNumber -1)*4).Take(4).ToList(); 
            if (Search != null)
            {
                return View(db.products.Where(m=>m.name.Equals(Search)|| Search==null).ToList());
            }
            return View(data);
        }
    }
}

