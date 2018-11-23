using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheAuction.Infrastructure;
using TheAuction.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using CodeFirst;
using System.IO;

namespace TheAuction.Controllers
{
    public class HomeController : Controller
    {   
        [AllowAnonymous]
        public ActionResult Reg()
        {
            _authManager.SignOut();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Reg(RegModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Reg_date = DateTime.Now
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    AppUser createdUser = await _userManager.FindByNameAsync(model.Name);

                    Location location = _dManager.LocationModel.getLocationByName(model.LocationName);
                    if(location == null)
                    {
                        location = new Location
                        {
                            Name = model.LocationName
                        };
                    }
                    
                    Figure figure = new Figure
                    {
                        Name = model.FigureName,
                        Surname = model.FigureSurname,
                        Patronymic = model.FigurePatronymic,
                        Location = location,
                        AppUser = createdUser,
                        PictureUrl = "/Content/UploadedFiles/45245724-4d65-48aa-ab76-17789d07c72b.14935821186341.jpg"
                    };
                    _dManager.FigureModel.setFigure(figure);

                    Customer customer = new Customer
                    {
                        Figure = figure,
                        Lots = new List<Lot>(),
                        Favorites = new List<Favorite>(),
                        Bets = new List<Bet>()
                    };
                    _dManager.CustomerModel.setCustomer(customer);

                    Seller seller = new Seller
                    {
                        Figure = figure
                    };
                    _dManager.SellerModel.setSeller(seller);

                    return RedirectToAction("IndexLots", "Auction");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        //[Authorize(Roles ="Sellers")]
        //public async Task<ActionResult> SellerAccount()
        //{
        //    AppUser user = await _userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());
        //    return View(user);
        //}
        //[Authorize(Roles = "Sellers")]
        //public async Task<ActionResult> MyLots()
        //{
        //    AppUser user = await _userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());  
        //    Figure figure = _dManager.FigureModel.getFigures().FirstOrDefault(f => f.AppUser == user);
        //    Seller seller = _dManager.SellerModel.getSellers().FirstOrDefault(s => s.Figure == figure);
        //    List<Lot> lots = _dManager.LotModel.getLots().Where(l => l.Seller == seller).ToList();
        //    return View(lots);
        //}
        //public ActionResult CreateTest()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateTest(CreateModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if(model.Name == model.Password || model.Name == model.Email)
        //        {
        //            ModelState.AddModelError("", "Пароль не должен совпадать с именем пользователя или электронным адресом");
        //        }
        //        RedirectToAction("CreateTest");
        //    }
        //    return View("CreateTest", model);
        //}
        //public ActionResult AddTest()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddTest(AddLotModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        RedirectToAction("AddTest");
        //    }
        //    return View("AddTest", model);
        //}


        [HttpGet]
        public JsonResult CheckDescription(string Description)
        {
            return Json(!Description.Equals("Плохоеописание"), JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult IndexFigures()
        {
            List<AppUser> Users = _userManager.Users.ToList();
            List<Figure> figures = _dManager.FigureModel.getFigures();
            return View(figures);
        }

        //[Authorize()]
        //public ActionResult Home()
        //{
        //    return View();
        //}

        //[Authorize]
        //public ActionResult Index()
        //{
        //    return View(GetData("Index"));
        //}

        //[Authorize(Roles = "Users")]
        //public ActionResult OtherAction()
        //{
        //    return View("Index", GetData("OtherAction"));
        //}

        //private Dictionary<string, object> GetData(string actionName)
        //{
        //    Dictionary<string, object> Info = new Dictionary<string, object>();

        //    Info.Add("Action", actionName);
        //    Info.Add("Пользователь", HttpContext.User.Identity.Name);
        //    Info.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
        //    Info.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
        //    Info.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

        //    return Info;
        //}

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult ImageUploadPage()
        {
            //int _id = 81;
            //Lot lot = _dManager.LotModel.getLotById(_id);
            return View();
        }
        [HttpPost]
        public ActionResult ImageUploadPage(string url)
        {
            Category cat = new Category
            {
                Name = url
            };
            _dManager.CategoryModel.setCategory(cat);
            return RedirectToAction("IndexLots", "Auction");
        }
        public JsonResult UploadFile(/*int _id*/)
        {
            string __filepath = Server.MapPath("~/Content/UploadedFiles");
            int __maxSize = 2 * 1024 * 1024;    // максимальный размер файла 2 Мб
            // допустимые MIME-типы для файлов
            List<string> mimes = new List<string>
            {
                "image/jpeg", "image/jpg", "image/png"
            };

            var result = new UploadingResult
            {
                Files = new List<string>()
            };

            if (Request.Files.Count > 0)
            {
                foreach (string f in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[f];

                    // Выполнить проверки на допустимый размер файла и формат
                    if (file.ContentLength > __maxSize)
                    {
                        result.Error = "Размер файла не должен превышать 2 Мб";
                        break;
                    }
                    else if (mimes.FirstOrDefault(m => m == file.ContentType) == null)
                    {
                        result.Error = "Недопустимый формат файла";
                        break;
                    }

                    // Сохранить файл и вернуть URL
                    if (Directory.Exists(__filepath))
                    {
                        Guid guid = Guid.NewGuid();
                        file.SaveAs($@"{__filepath}\{guid}.{file.FileName}");
                        result.Files.Add($"/Content/UploadedFiles/{guid}.{file.FileName}");
                    }
                }
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteFile(string url)
        {
            // string fullPath = $"C:/Users/Bolsho_Peka/Documents/Visual Studio 2015/Projects/TheAuction/TheAuction/{url}";
            //System.IO.File.Delete($@".\{url}"); // Ищет на IIS зачем-то
            System.IO.File.Delete($"C:/Users/Bolsho_Peka/Documents/Visual Studio 2015/Projects/TheAuction/TheAuction/{url}");
            return RedirectToAction("AddLot", "Auction");
        }

        public ActionResult IndexCounter()
        {
            return View();
        }

        //public ActionResult IndexPictures()
        //{
        //    return View(_dManager.LotPictureModel.getLotPictures());
        //}

        //public ActionResult CreatePictures()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreatePictures(LotPicture pic, HttpPostedFileBase uploadImage)
        //{
        //    if (ModelState.IsValid && uploadImage != null)
        //    {
        //        byte[] imageData = null;
        //        // считываем переданный файл в массив байтов
        //        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
        //        {
        //            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
        //        }
        //        // установка массива байтов
        //        pic.Image = imageData;

        //        _dManager.LotPictureModel.setLotPicture(pic);

        //        return RedirectToAction("IndexPictures");
        //    }
        //    return View(pic);
        //}

        private IOwinContext _context
        {
            get
            {
                return HttpContext.GetOwinContext();
            }
        }
        protected DataManager _dManager
        {
            get
            {
                return new DataManager(_context);
            }
        }
        private AppUserManager _userManager
        {
            get
            {
                return _context.GetUserManager<AppUserManager>();
            }
        }
        private IAuthenticationManager _authManager
        {
            get
            {
                return _context.Authentication;
            }
        }
    }
}