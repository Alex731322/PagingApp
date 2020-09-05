using PagingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagingApp.Controllers;
using System.EnterpriseServices.Internal;
using System.Collections.Concurrent;
using System.Net;

namespace PagingApp.Controllers
{
    public class BerkutController : Controller
    {
        MobileContext db = new MobileContext();
        //   List<Phone> phones;
        
        public ActionResult EditDbData(int? Id, string Model, string Producer, int? counttr = 3)
        {
            Phone ph = new Phone();
            ph.Id = (int)Id;
            ph.Producer = Producer;
            ph.Model = Model;

            if (Id != null)
            {
                db.Entry(ph).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

          //  int current = db.C.Phones.Count();


            IEnumerable<Phone> phonesPerPages_durak = db.Phones.OrderBy
                (x => x.Id).Skip(0).
                Take(db.Phones.Count()).ToList();




            int kol_vo = 1;
            foreach  ( var durak in phonesPerPages_durak)
            {
                if (durak.Id == Id) break;
                kol_vo++;
            }



            int pageSize = (int)counttr;

            int pagetr = (int)Math.Ceiling((decimal)db.Phones.Count() / pageSize);
            int p_tek = (int)Math.Ceiling((decimal) kol_vo / pageSize);

            IEnumerable<Phone> phonesPerPages = db.Phones.OrderBy
                (x => x.Id).Skip((p_tek - 1) * pageSize).
                Take(pageSize).ToList();


            PageInfo pageInfo = new PageInfo
            {
                PageNumber = p_tek,
                PageSize = pageSize,
                TotalItems = db.Phones.Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Phones = phonesPerPages
            };

            return View("~/Views/Home/Index.cshtml", ivm);
        }

        public ActionResult DeleteDbData(int? Id, int? counttr= 3 )
        {
            if(Id == null)
            {
                return HttpNotFound();
            }

            Phone ph = db.Phones.Find(Id);
            if (ph != null)
            {
                db.Phones.Remove(ph);
                db.SaveChanges();

            }

            int pageSize = (int)counttr; 

            int pagetr = (int)Math.Ceiling((decimal)db.Phones.Count() / pageSize);
           
            IEnumerable<Phone> phonesPerPages = db.Phones.OrderBy
                (x => x.Id).Skip((pagetr - 1) * pageSize).
                Take(pageSize).ToList();


            PageInfo pageInfo = new PageInfo
            {
                PageNumber = pagetr,
                PageSize = pageSize,
                TotalItems = db.Phones.Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Phones = phonesPerPages
            };

            return View("~/Views/Home/Index.cshtml", ivm);
        }
        public string Cow(string Lena)
        {
            return Lena;
        }
        public ActionResult Index(string Model, string Producer, int? counttr=3 )
        {
            Phone ph = new Phone();
            ph.Model = Model;
            ph.Producer = Producer;
            db.Phones.Add(ph);
            db.SaveChanges();

           

            int pageSize = 3;
            if (counttr == null)
            {
                pageSize = 3;
            }
            else
            {
                pageSize = (int)counttr;
            }

            int pagetr = (int)Math.Ceiling((decimal)db.Phones.Count() / pageSize) ;
            


            IEnumerable<Phone> phonesPerPages = db.Phones.OrderBy
                (x => x.Id).Skip((pagetr - 1) * pageSize).
                Take(pageSize).ToList();
            


            PageInfo pageInfo = new PageInfo
            {
                PageNumber = pagetr,
                PageSize = pageSize,
                TotalItems = db.Phones.Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Phones = phonesPerPages
            };
            ViewData["Model"] = Model;
            ViewData["Producer"] = Producer;
            
            return View("~/Views/Home/Index.cshtml",ivm);
        }
     
     
    }
}