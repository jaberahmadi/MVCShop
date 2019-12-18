using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiewModels;

namespace MvcInternetShop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [HttpGet]
        public ActionResult Index()
        {
            GroupRepository blGroup = new GroupRepository();
            ProductRepository blProduct = new ProductRepository();
            var model = new HomeIndexViewModel();
            model.Groups = blGroup.Select();
            model.Products = blProduct.Select();
            model.BestSellersProducts = blProduct.Select().OrderBy(p => p.FactorItems.Count).Take(6);
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            UserRepository blUser = new UserRepository();
            if (ModelState.IsValid)
            {
                if (user.BirthDate != null)
                {
                    user.BirthDate = user.BirthDate.Value.ToMiladiDate();
                }

                if (blUser.Add(user))
                {
                    //موفق
                    return MessageBox.Show("با موفقیت ثبت شد", MessageType.Success);
                }
                else
                {
                    //نا موفق
                    return MessageBox.Show("ثبت نشد", MessageType.Error);
                }
            }
            else
            {
                //خطا مقداری
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }

        [HttpPost]
        public ActionResult AddToCart(int Id, byte Count)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Cart_" + Id.ToString()))
                {
                    //ویرایش کوکی
                    var cookie = new HttpCookie("Cart_" + Id.ToString(), (Convert.ToByte(Request.Cookies["Cart_" + Id.ToString()].Value) + 1).ToString());
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.HttpOnly = true;
                    Response.Cookies.Set(cookie);
                }
                else
                {
                    //افزودن کوکی جدید
                    var cookie = new HttpCookie("Cart_" + Id.ToString(), Count.ToString());
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                }
                return Json(new JsonData()
                {
                    Success = true,
                    Script = MessageBox.Show("کالا به سبد خرید شما اضافه شد.", MessageType.Success).Script,
                    Html = "<span>لیست خرید (" + CartCount() + ")</span>"
                });
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا به سبد خرید شما اضافه نشد.", MessageType.Error).Script,
                    Html = ""
                });
            }
        }

        [HttpPost]
        public ActionResult RemoveCart(int Id)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Cart_" + Id.ToString()))
                {
                    Response.Cookies["Cart_" + Id.ToString()].Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies.Remove("Cart_" + Id.ToString());
                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("کالا از سبد خرید شما حذف شد.", MessageType.Success).Script,
                        Html = "لیست خرید (" + CartCount() + ")"
                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Success = false,
                        Script = MessageBox.Show("این کالا در سبد خرید شما وجود ندارد", MessageType.Warning).Script,
                        Html = "لیست خرید (" + CartCount() + ")"
                    });
                }
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا از سبد خرید شما حذف نشد.", MessageType.Error).Script,
                    Html = ""
                });
            }
        }

        public string CartCount()
        {
            List<HttpCookie> lst = new List<HttpCookie>();
            for (int i = Request.Cookies.Count - 1; i >= 0; i--)
            {
                if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                    lst.Add(Request.Cookies[i]);
            }
            int CartCount = lst.Where(p => p.Name.StartsWith("Cart_")).Count();
            return CartCount.ToString();
        }

        [HttpGet]
        public ActionResult Basket()
        {
            ProductRepository blProduct = new ProductRepository();
            List<BasketViewModel> listBasket = new List<BasketViewModel>();
            List<HttpCookie> lst = new List<HttpCookie>();
            for (int i = Request.Cookies.Count - 1; i >= 0; i--)
            {
                if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                    lst.Add(Request.Cookies[i]);
            }
            foreach (var item in lst.Where(p => p.Name.StartsWith("Cart_")))
            {
                listBasket.Add(new BasketViewModel { Product = blProduct.Find(Convert.ToInt32(item.Name.Substring(5))), Count = Convert.ToInt32(item.Value) });
            }
            return View(listBasket);
        }

        [HttpGet]
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult Buy(Factor factor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        FactorRepository blFactor = new FactorRepository();
        //        FactorItemRepository blFactorItem = new FactorItemRepository();
        //        ProductRepository blProduct = new ProductRepository();
        //        List<BasketViewModel> listBasket = new List<BasketViewModel>();
        //        List<HttpCookie> lst = new List<HttpCookie>();
        //        for (int i = Request.Cookies.Count - 1; i >= 0; i--)
        //        {
        //            if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
        //                lst.Add(Request.Cookies[i]);
        //        }
        //        foreach (var item in lst.Where(p => p.Name.StartsWith("Cart_")))
        //        {
        //            listBasket.Add(new BasketViewModel { Product = blProduct.Find(Convert.ToInt32(item.Name.Substring(5))), Count = Convert.ToInt32(item.Value) });
        //        }

        //        decimal price = 0;
        //        foreach (var item in listBasket)
        //        {
        //            price += (item.Count * item.Product.Price);
        //        }

        //        factor.BuyDate = DateTime.Now;
        //        factor.Price = price;
        //        factor.Description = "خرید توسط کاربر " + factor.Name + " در تاریخ " + DateTime.Now.ToPersianDate().ToString() + " به مبلغ" + factor.Price.ToPrice() + " تومان انجام شد";
        //        if (Session["UserId"] != null)
        //        {
        //            factor.UserId = Convert.ToInt32(Session["UserId"]);
        //        }
        //        factor.IsFinish = false;

        //        if (blFactor.Add(factor))
        //        {
        //            int factorId = blFactor.GetLastIdentity();
        //            foreach (var item in listBasket)
        //            {
        //                blFactorItem.Add(new FactorItem() { FactorId = factorId, ProductId = item.Product.Id, Qty = Convert.ToByte(item.Count) });
        //            }
        //            // redirect to ...
        //            var zarinpal = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

        //            string result = "";
        //            int code = zarinpal.PaymentRequest("529ade88-fb48-425b-a813-2c485ee8a9ca", Convert.ToInt32(factor.Price), factor.Description, factor.Email, factor.Mobile, "http://" + Request.Url.Authority + "/Home/Verify?Factor=" + factorId.ToString().Encrypt().UrlEncode(), out result);
        //            if (code == 100)
        //            {
        //                return Redirect("https://www.zarinpal.com/pg/StartPay/" + result);
        //            }
        //            else
        //            {
        //                ViewBag.Message = "خطا هنگام اتصال به درگاه بانکی";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "اطلاعات شما ثبت نشد";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "اطلاعات خود را بدرستی وارد کنید";
        //    }
        //    return View();
        //}


//        public ActionResult Verify(string Factor, string Status, string Authority)
//        {
//            if (string.IsNullOrEmpty(Status) == false && string.IsNullOrEmpty(Authority) == false && string.IsNullOrEmpty(Factor) == false && Status.ToLower() == "ok")
//            {
//                FactorRepository blFactor = new FactorRepository();
//                var fact = blFactor.Find(Convert.ToInt32(Factor.Decrypt()));
//                long refId = 0;
//                System.Net.ServicePointManager.Expect100Continue = false;
//                ZarinPal.PaymentGatewayImplementationServicePortTypeClient zarinPal = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
//                int _status = zarinPal.PaymentVerification("529ade88-fb48-425b-a813-2c485ee8a9ca", Authority, Convert.ToInt32(fact.Price), out refId);

//                switch (_status)
//                {
//                    case -1:
//                        ViewBag.Message = "اطلاعات ارسال شده ناقص است.";
//                        break;
//                    case -11:
//                        ViewBag.Message = "درخواست مورد نظر یافت نشد.";
//                        break;
//                    case -22:
//                        ViewBag.Message = "تراکنش ناموفق می باشد.";
//                        break;
//                    case -33:
//                        ViewBag.Message = "مبلغ تراکنش با مبلغ پرداخت شده مطابقت ندارد.";
//                        break;
//                    case 100:
//                    case 101:
//                        //Success
//                        fact.IsFinish = true;
//                        fact.FlloweCode = refId.ToString();

//                        blFactor.Update(fact);

//                        ViewBag.Message2 = "تراکنش با موفقیت انجام شد. کد رهگیری : " + refId.ToString();

//                        //                break;
//                }
//}
//            else
//            {
//                ViewBag.Message = "مقدار ورودی نا معتبر است";
//            }
//            return View();
//        }

        public ActionResult ShowGroups(string groupName)
        {
            GroupRepository blGroup = new GroupRepository();
            ProductRepository blProduct = new ProductRepository();
            var model = new ShowGroupsViewModel();
            if (string.IsNullOrEmpty(groupName))
            {
                model.Groups = blGroup.Where(p => p.ParentId == null).ToList();
                model.Products = blProduct.Select();
                return View(model);
            }
            else
            {
                var src = blGroup.Where(p => p.Name.Trim() == groupName.Trim());
                if (src.Any())
                {
                    model.Groups = src.Single().Groups.ToList();
                    int groupId = src.Single().Id;
                    model.Products = blProduct.Where(p => p.GroupId == groupId);
                    return View(model);
                }
                else
                {
                    model.Groups = blGroup.Where(p => p.ParentId == null).ToList();
                    model.Products = blProduct.Select();
                    return View(model);
                }
            }
        }

        public ActionResult ShowProducts(string productName)
        {
            ProductRepository blProduct = new ProductRepository();

            if (string.IsNullOrEmpty(productName))
            {
                return RedirectToAction("Index");
            }
            else
            {
                var src = blProduct.Where(p => p.Url.Trim() == productName.Trim());
                if (src.Any())
                {
                    return View(src.Single());
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

    }
}
