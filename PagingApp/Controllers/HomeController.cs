using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagingApp.Models;


namespace PagingApp.Controllers
{
   
    public class HomeController : Controller
    {
        
        MobileContext db = new MobileContext();
        public List<Phone> phones;

        public HomeController()
        {
            phones = new List<Phone>();
            phones.Add(new Phone { Id = 1, Model = "Samsung Galaxy III", Producer = "Samsung" });
            phones.Add(new Phone { Id = 2, Model = "Samsung Ace II", Producer = "Samsung" });
            phones.Add(new Phone { Id = 3, Model = "HTC Hero", Producer = "HTC" });
            phones.Add(new Phone { Id = 4, Model = "HTC One S", Producer = "HTC" });
            phones.Add(new Phone { Id = 5, Model = "HTC One X", Producer = "HTC" });
            phones.Add(new Phone { Id = 6, Model = "LG Optimus 3D", Producer = "LG" });
            phones.Add(new Phone { Id = 7, Model = "Nokia N9", Producer = "Nokia" });
            phones.Add(new Phone { Id = 8, Model = "Samsung Galaxy Nexus", Producer = "Samsung" });
            phones.Add(new Phone { Id = 9, Model = "Sony Xperia X10", Producer = "SONY" });
            phones.Add(new Phone { Id = 10, Model = "Samsung Galaxy II", Producer = "Samsung" });
        }

       

        public ActionResult Index(int page = 1, int? count = 3  )
        {
            

            int pageSize = 3;
            if(count == null)
            {
                pageSize = 3; 
            }
            else
            {
                pageSize =(int)count;
            }
            IEnumerable<Phone> phonesPerPages = db.Phones.OrderBy
                (x => x.Id).Skip((page - 1) * pageSize).
                Take(pageSize).ToList();

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = db.Phones.Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Phones = phonesPerPages
            };
            return View(ivm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}