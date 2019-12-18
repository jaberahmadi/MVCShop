using DomainClass;
using System.Linq;
using System.Web.Mvc;
using WiewModels;

namespace MvcInternetShop.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Search()
        {
            return Content("Search");
        }

        [HttpGet]
        public ActionResult AddReseller()
        {
            var blProvience = new ProvienceRepository();
            var model = new AddResellerViewModel();
            model.Proviences = blProvience.Select().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddReseller(Reseller reseller)
        {
            if (ModelState.IsValid)
            {
                var blReseller = new ResellerRepository();
                if (blReseller.Add(reseller))
                {
                    //ViewBag.Message = "نمایندگی ثبت شد";
                    return MessageBox.Show("نمایندگی ثبت شد", MessageType.Success);
                }
                else
                {
                    //ViewBag.Message = "نمایندگی ثبت نشد";
                    return MessageBox.Show("نمایندگی ثبت نشد", MessageType.Error);
                }
            }
            else
            {
                //ViewBag.Message = "مقادر ورودی نا معتبر است";
                return MessageBox.Show("مقادر ورودی نا معتبر است", MessageType.Error);
            }


            //var blOstan = new ProvienceRepository();
            //var model = new AddResellerViewModel();
            //model.Province = blOstan.Select().ToList();
            //model.Reseller = reseller;
            //return View(model);
        }

        [HttpPost]
        public ActionResult GetCities(int ostanId)
        {
            var blCity = new CityRepository();
            var model = new CityDrpViewModel();
            model.Cities = blCity.Where(p => p.OstanId == ostanId).ToList();
            return PartialView("_CityDropDownList", model);
        }

    }
}
