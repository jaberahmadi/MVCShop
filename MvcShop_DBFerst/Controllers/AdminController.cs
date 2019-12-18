using System.IO;
using MvcInternetShop.Helpers.Filters;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiewModels;
using DomainClass;
using Utilities;

namespace MvcInternetShop.Controllers
{
    public class AdminController : Controller
    {
        GroupRepository blGroup = new GroupRepository();
        ProductRepository blProduct = new ProductRepository();

        //
        // GET: /Admin/

        public ActionResult Groups()
        {
            var model = new AddGroupViewModel();
            model.Groups = blGroup.Select().ToList();
            return View(model);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteGroup(int id)
        {
            if (blGroup.Delete(id))
            {
                ViewData["id"] = "Group_ParentId";
                ViewData["name"] = "Group.ParentId";
                return Json(new JsonData()
                {
                    Script = MessageBox.Show("با موفقیت حذف شد", MessageType.Success).Script,
                    Success = true,
                    Html = this.RenderPartialToString("_GroupList", blGroup.Select().ToList())
                });
            }
            else
            {
                return Json(new JsonData
                {
                    Script = MessageBox.Show("حذف نشد", MessageType.Error).Script,
                    Success = false,
                    Html = ""
                });
            }
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            var model = new AddProductViewModel();
            model.Groups = blGroup.Select();
            return View(model);
        }

        
        [AjaxOnly]
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                product.Image = UploadImage.FileName;
                string path = Server.MapPath("~") + "Files\\UploadImages\\" + UploadImage.FileName;
                UploadImage.InputStream.ResizeImageByWidth(500, path, Utilty.ImageComperssion.Normal);
                
                if (blProduct.Add(product))
                {
                    return MessageBox.Show("محصول با موفقیت ثبت شد", MessageType.Success);
                }
                else
                {
                    System.IO.File.Delete(path);
                    return MessageBox.Show("محصول ثبت نشد", MessageType.Error);
                }
            }
            else
            {
                //مقدار ورودی اشتباه
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult AddGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                if (blGroup.Add(group))
                {
                    ViewData["id"] = "Group_ParentId";
                    ViewData["name"] = "Group.ParentId";
                    //موفق
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("با موفقیت ثبت شد", MessageType.Success).Script,
                        Success = true,
                        Html = this.RenderPartialToString("_GroupList", blGroup.Select().ToList())
                    });
                }
                else
                {
                    //نا موفق
                    return Json(new JsonData
                    {
                        Script = MessageBox.Show("ثبت نشد", MessageType.Error).Script,
                        Success = false,
                        Html = ""
                    });
                }
            }
            else
            {
                //خطا مقداری
                return Json(new JsonData
                {
                    Script = MessageBox.Show(ModelState.GetErrors(), MessageType.Warning).Script,
                    Success = false,
                    Html = ""
                });
            }
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult EditGroup(Group group)
        {
            if (ModelState.IsValid && group.Id != 0)
            {
                if (blGroup.Update(group))
                {
                    ViewData["id"] = "Group_ParentId";
                    ViewData["name"] = "Group.ParentId";
                    //موفق
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("با موفقیت ویرایش شد", MessageType.Success).Script,
                        Success = true,
                        Html = this.RenderPartialToString("_GroupList", blGroup.Select().ToList())
                    });
                }
                else
                {
                    //نا موفق
                    return Json(new JsonData
                    {
                        Script = MessageBox.Show("ویرایش نشد", MessageType.Error).Script,
                        Success = false,
                        Html = ""
                    });
                }
            }
            else
            {
                //خطا مقداری
                return Json(new JsonData
                {
                    Script = MessageBox.Show(ModelState.GetErrors(), MessageType.Warning).Script,
                    Success = false,
                    Html = ""
                });
            }
        }

        [HttpGet]
        public ActionResult Products()
        {
            return View(blProduct.Select());
        }

        public ActionResult DeleteProduct(int id)
        {
            blProduct.Delete(id);
            return View("Products", blProduct.Select());
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var model = new AddProductViewModel();
            model.Groups = blGroup.Select();
            model.Product = blProduct.Find(id);
            return View(model);
        }


        [AjaxOnly]
        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;
                if (UploadImage != null)
                {
                    imagePath = UploadImage.FileName;
                    UploadImage.InputStream.ResizeImageByWidth(500, Server.MapPath("~") + "Files\\UploadImages\\" + imagePath, Utilty.ImageComperssion.Normal);
                }

                if (blProduct.Update(product, imagePath))
                {
                    return MessageBox.Show("محصول با موفقیت ویرایش شد", MessageType.Success);
                }
                else
                {
                    if (imagePath != null)
                    {
                        System.IO.File.Delete(Server.MapPath("~") + "Files\\UploadImages\\" + imagePath);
                    }
                    return MessageBox.Show("محصول ویرایش نشد", MessageType.Error);
                }
            }
            else
            {
                //مقدار ورودی اشتباه
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }

        [HttpGet]
        public ActionResult Factors()
        {
            FactorRepository blFactor = new FactorRepository();
            return View(blFactor.Select().OrderByDescending(p => p.IsFinish).ToList());
        }


        [HttpGet]
        public ActionResult SendMail(string email)
        {
            return View(model: email);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMail(string recivers, string title, string text, HttpPostedFileBase attachment)
        {
            bool result = false;
            if (attachment != null)
            {
                string path = Server.MapPath("~") + "\\Files\\Attachment\\" + Path.GetFileName(attachment.FileName);
                attachment.SaveAs(path);
                result = MailSender.SendMailByAttach(title, text, path, recivers.Split(','));
            }
            else
            {
                result = MailSender.SendMail(title, text, recivers.Split(','));
            }
            if (result)
            {
                return MessageBox.Show("پیام با موفقیت ارسال شد", MessageType.Success);
            }
            else
            {
                return MessageBox.Show("پیام ارسال نشد", MessageType.Error);
            }
        }
    }
}
