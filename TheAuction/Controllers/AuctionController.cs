using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TheAuction.Infrastructure;
using TheAuction.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Threading;
using CodeFirst;
using System.Text;

namespace TheAuction.Controllers
{
    public class AuctionController : Controller
    {
        IOwinContext _context { get { return HttpContext.GetOwinContext(); } }
        AppUserManager _userManager { get { return _context.GetUserManager<AppUserManager>(); } }
        private IAuthenticationManager _authManager { get { return _context.Authentication; } }
        //MyDbContext _mdb { get { return MyDbContext.Create(); } } // unable to get users from this context
        //DataManager _dManager { get { return new DataManager(_mdb); } }
        DataManager _dManager { get { return new DataManager(_context); } }

        public string FillCat()
        {
            List<Category> CategoriesToFill = new List<Category>
            {
                {new Category {Name = "others" } }
            };
            _dManager.CategoryModel.setCategory(CategoriesToFill[0]);
            return "Успех";
        }
        public string FillConditions()
        {
            /*
     Лоты: "active", "payment_expected (использоваться пока не будет)", "shipment_expected", "unclaimed", "shipped (использоваться пока не будет)", "delivered"
            "processing" (производятся действия методом AddCheck в TimerModule)
     Чеки: "complete", "incomplete (использоваться пока не будет)" 
     Отправки: "shipped (использоваться пока не будет)", "delivered"
     */
            //List<Lot> lots = new List<Lot>();
            List<Condition> ConditionsToFill = new List<Condition>
            {
                //{new Condition {Name = "processing"} },
                //{new Condition {Name = "active" } },
                //{new Condition {Name = "payment_expected" } },
                //{new Condition {Name = "shipment_expected" } },
                //{new Condition {Name = "unclaimed"} },
                //{new Condition {Name = "shipped"} },
                //{new Condition {Name = "delivered" } },
                //{new Condition {Name = "complete"} },
                //{new Condition {Name = "incomplete" } }
            };
            foreach(Condition c in ConditionsToFill)
            {
                _dManager.ConditionModel.setCondition(c);
            }
            string success = "готово";
            return success;
        }
        public string FillLots(int? id)
        {
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            for (int i = 0; i < id; i++)
            {
                //List<LotGroup> groups = _dManager.LotGroupModel.getLotGroups();
                Category category = _dManager.CategoryModel.getCategoryByName("musical");
                Condition condition = _dManager.ConditionModel.getConditionByName("active");
                if (!_authManager.User.Identity.IsAuthenticated)
                {
                    return "Залогинься";
                }
                int startBet = 1000;
                Lot lot = new Lot
                {
                    Name = $"test_item_{new Random().Next()}",
                    Description = "test_desc",
                    Start_bet = startBet,
                    Price = startBet,
                    Min_bet = startBet / 10,
                    Auction_start = DateTime.Now,
                    Auction_end = DateTime.Now.AddDays(99999),
                    Condition = condition,
                    Seller = seller,
                    Location = figure.Location,
                    Category = category,
                    PictureUrl = "/Content/UploadedFiles/eda3aa43-fa7f-4a7e-b7eb-f11961482e2c.15248107768890.jpg"
                };
                //if (groups.Count != 0)
                //{
                //    int range = 50;
                //    LotGroup lastGroup = groups.LastOrDefault();
                //    if ((lastGroup.LotRangeEnd % range) == 0)
                //    {
                //        LotGroup newGroup = new LotGroup
                //        {
                //            LotRangeStart = lastGroup.LotRangeStart + range,
                //            LotRangeEnd = lastGroup.LotRangeStart + range
                //        };
                //        lot.LotGroup = newGroup;
                //        _dManager.LotGroupModel.setLotGroup(newGroup);
                //    }
                //    else
                //    {
                //        lot.LotGroup = lastGroup;
                //        lastGroup.LotRangeEnd += 1;
                //        _dManager.LotGroupModel.editLotGroup(lastGroup);
                //    }
                //}
                //else
                //{
                //    LotGroup newGroup = new LotGroup
                //    {
                //        LotRangeStart = 1,
                //        LotRangeEnd = 1
                //    };
                //    lot.LotGroup = newGroup;
                //    _dManager.LotGroupModel.setLotGroup(newGroup);
                //}
                _dManager.LotModel.setLot(lot);
            }
            return $"Успешно добавлено {id} лотов";
        }
        public string FillBets(int? id)
        {
            if(id == null)
            {
                return "Введите число ставок";
            }
            string userId = _authManager.User.Identity.GetUserId();
            if (!_authManager.User.Identity.IsAuthenticated)
            {
                return "Залогинься";
            }
            AppUser user = _userManager.FindById(userId);
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location)
                .ToList().FirstOrDefault(f => f.AppUser == user);
            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category).Include(l => l.Location).ToList()
                .Where(l => l.Condition.Name == "active" && l.Auction_end > DateTime.Now && l.Bets_count == 0).ToList();

            List<Lot> decimated = new List<Lot>();
            for (int i = 0; i < lots.Count; i += id.Value)
            {
                decimated.Add(lots[i]);
            }
            foreach (Lot l in decimated)
            {
                l.Auction_end = DateTime.Now.AddSeconds(40);
                _dManager.LotModel.editLot(l);
                Bet bet = new Bet()
                {
                    Lot = l,
                    Amount = 100,
                    Date = DateTime.Now,
                    Customer = customer,
                };
                _dManager.BetModel.setBet(bet);
                l.Price = bet.Amount + l.Price;
                l.Bets_count++;
                _dManager.LotModel.editLot(l);
            }
            return $"Успешно добавлено ставок: {decimated.Count}";
        }
        public string DeleteLots(long? id)
        {
            if(id == null)
            {
                return "Введи диапазон удалаяемых лотов (через 000)";
            }
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category).Include(l => l.Location).Include(l => l.Winner).
                Include(l => l.Seller).ToList();
            if (id.Value == 0)
            {
                lots.RemoveAll(l => true);
                _dManager._dbContext.SaveChanges();
                return "Удалены все лоты";
            }
            string range = id.ToString();
            string separator = "000";
            int sepL = separator.Length;
            if (range.Length < sepL - 2)
            {
                return "Должно получиться не меньше 8 символов";
            }
            char[] rangeArr = range.ToCharArray();
            int start, end;
            for(int i = 0; i < rangeArr.Length - sepL; i++)
            {
                char[] tempArr = new char[sepL];
                for(int j = 0; j < sepL; j++)
                {
                    tempArr[j] = rangeArr[j + i];
                }
                if(new string(tempArr) == separator)
                {
                    string startStr = "";
                    string endStr = "";
                    for (int j = 0; j < i; j++)
                    {
                        startStr += rangeArr[j];
                    }
                    for (int j = i + sepL; j < rangeArr.Length; j++)
                    {
                        endStr += rangeArr[j];
                    }
                    start = int.Parse(startStr);
                    end = int.Parse(endStr);
                    if(end < start)
                    {
                        return "Диапазон указан неверно";
                    }
                    lots.RemoveAll(l => l.Lot_id >= start && l.Lot_id <= end);
                    _dManager._dbContext.SaveChanges();
                    return $"Успешно удалено {(end - start) + 1} лотов";
                }
            }
            return "Разделитель (000) не обнаружен";
        }

        public ActionResult IndexLots(string categoryName, string sortBy, int? currPage)
        {
            if (User.Identity.IsAuthenticated) // for authorized
            {
                string userId = _authManager.User.Identity.GetUserId();
                AppUser user = _userManager.FindById(userId);
                /* List<AppUser> users = _userManager.Users.ToList();*///сложно объяснить, но этот лист нужен, чтобы при user==null не "находился" figure.
                Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).ToList().FirstOrDefault(f => f.AppUser == user);
                bool isInRoleSellers = _userManager.IsInRole(userId, "Sellers");
                bool isInRoleCustomers = _userManager.IsInRole(userId, "Customers");
                if (isInRoleSellers == true)
                {
                    Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
                    ViewData["seller"] = seller;
                    ViewData["callingMethodName"] = "IndexLotsForSeller";
                }
                if (isInRoleCustomers == true)
                {
                    ViewData["callingMethodName"] = "IndexLotsForCustomer";
                }
            }
            else // for nonauthorized
            {
                ViewData["callingMethodName"] = "IndexLotsForNonAuthorized";
            }

            // For both authorized and nonauthorized users
            if (currPage == null)
            {
                currPage = 1;
            }
            ViewData["currPage"] = currPage;

            if (sortBy == null || sortBy == "")
            {
                sortBy = "Auction_end descending";
            }
            ViewData["sortBy"] = sortBy;

            if (categoryName == null || categoryName == "all")
            {
                categoryName = "all";
            }
            ViewData["categoryName"] = categoryName;

            int pageSize = 10;
            int pagesCount;
            List<Lot> lots = SortLots.Sort(sortBy, pageSize, currPage.Value, out pagesCount, _dManager);
            ViewData["pagesCount"] = pagesCount;
            return View(lots);
        }

        [Authorize(Roles ="Sellers")]
        public ActionResult SellerProfile()
        {
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
                //сложно объяснить, но этот лист нужен, чтобы при user==null не "находился" figure.
                //
            /* List<AppUser> users = _userManager.Users.ToList();*/
            //List<Location> locations = _dManager.LocationModel.getLocations();
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            //List<Condition> conditions = _dManager.ConditionModel.getConditions();
            //
                //Double ToList() in order to avoid some strange exception
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                            .Include(l => l.Location).ToList().Where(l => l.Seller == seller).OrderByDescending(l => l.Auction_end).ToList();
            List<string> conditionsNames = new List<string>();
            foreach (Lot lot in lots)
            {
                conditionsNames.Add(lot.Condition.Name);
            }
            ViewData["conditionsNames"] = conditionsNames;
            ViewData["seller"] = seller;
            ViewData["lotsCount"] = lots.Count;
            ViewData["currentConditionName"] = "all";
            ViewData["callingMethodName"] = "SellerProfile";
            return View(lots);
        }

        [Authorize(Roles = "Sellers")]
        public ActionResult SellerProfileFiltered(string conditionName)
        {
            if(conditionName == "all")
            {
                return RedirectToAction("SellerProfile");
            }
            if(conditionName == null)
            {
                conditionName = "active";
            }
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
            //List<Location> locations = _dManager.LocationModel.getLocations();
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            //List<Condition> conditions = _dManager.ConditionModel.getConditions();
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                            .Include(l => l.Location).ToList().Where(l => l.Seller == seller).OrderByDescending(l => l.Auction_end).ToList();
            List<string> conditionsNames = new List<string>();
            foreach (Lot lot in lots)
            {
                conditionsNames.Add(lot.Condition.Name);
            }
            List<Lot> filteredLots = lots.Where(l => l.Condition.Name == conditionName).OrderByDescending(l => l.Auction_end).ToList();

            ViewData["conditionsNames"] = conditionsNames;
            ViewData["seller"] = seller;
            ViewData["lotsCount"] = lots.Count;
            ViewData["currentConditionName"] = conditionName;
            ViewData["callingMethodName"] = "SellerProfile";
            return View("SellerProfile", filteredLots);
        }

        [Authorize(Roles = "Customers")]
        public ActionResult CustomerProfile()
        {
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
            //List<AppUser> users = _userManager.Users.ToList();//сложно объяснить, но этот лист нужен, чтобы при user==null не "находился" figure.
            //List<Location> locations = _dManager.LocationModel.getLocations();
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            //List<Condition> conditions = _dManager.ConditionModel.getConditions();
                //Taking bets made by the current user
            List<Bet> bets = _dManager._dbContext.Bets.Include(b => b.Customer).Include(b => b.Lot).ToList().Where(b => b.Customer == customer).ToList();
                //Taking every lot
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                           .Include(l => l.Location).ToList();
                //Selecting only those lots on which bets has been made by the current user, and then excluding duplicates by Distinct()
            lots = bets.Select(b => b.Lot).Distinct().OrderByDescending(l => l.Auction_end).ToList();

            List<string> conditionsNames = new List<string>();
            foreach (Lot lot in lots)
            {
                conditionsNames.Add(lot.Condition.Name);
            }
            ViewData["conditionsNames"] = conditionsNames;
            ViewData["customer"] = customer;
            ViewData["lotsCount"] = lots.Count;
            ViewData["currentConditionName"] = "all";
            ViewData["callingMethodName"] = "CustomerProfile";
            return View(lots);
        }

        [Authorize(Roles = "Customers")]
        public ActionResult CustomerProfileFiltered(string conditionName)
        {
            if (conditionName == "all")
            {
                return RedirectToAction("CustomerProfile");
            }
            if (conditionName == null)
            {
                conditionName = "active";
            }
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
            //List<Location> locations = _dManager.LocationModel.getLocations();
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            //List<Condition> conditions = _dManager.ConditionModel.getConditions();
                //Taking bets made by the current user
            List<Bet> bets = _dManager._dbContext.Bets.Include(b => b.Customer).Include(b => b.Lot).ToList().Where(b => b.Customer == customer).ToList();
                //Taking every lot
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                           .Include(l => l.Location).ToList();
                //Selecting only those lots on which bets has been made by the current user, and then excluding duplicates by Distinct()
            lots = bets.Select(b => b.Lot).Distinct().OrderByDescending(l => l.Auction_end).ToList();
            List<string> conditionsNames = new List<string>();
            foreach (Lot lot in lots)
            {
                conditionsNames.Add(lot.Condition.Name);
            }
            List<Lot> filteredLots = lots.Where(l => l.Condition.Name == conditionName).ToList();

            ViewData["conditionsNames"] = conditionsNames;
            ViewData["customer"] = customer;
            ViewData["lotsCount"] = lots.Count;
            ViewData["currentConditionName"] = conditionName;
            ViewData["callingMethodName"] = "CustomerProfile";
            return View("CustomerProfile", filteredLots);
        }

        //public ActionResult IndexShipmentsForCustomer()
        //{
        //    string userId = _authManager.User.Identity.GetUserId();
        //    AppUser user = _userManager.FindById(userId);
        //    Figure figure = _dManager.FigureModel.getFigures().FirstOrDefault(f => f.AppUser == user);
        //    Customer сustomer = _dManager.CustomerModel.getCustomers().FirstOrDefault(s => s.Figure == figure);

        //}

        public ActionResult AddLot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLot(AddLotModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string userId = _authManager.User.Identity.GetUserId();
            AppUser user = _userManager.FindById(userId);
            //List<Location> locations = _dManager.LocationModel.getLocations();
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
            Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            Category category = _dManager.CategoryModel.getCategoryByName(model.CategoryName);
            Condition condition = _dManager.ConditionModel.getConditionByName("active");
            //List<LotGroup> groups = _dManager.LotGroupModel.getLotGroups();
            //if(category == null) // Закоментить после добавления всех категорий
            //{
            //    category = new Category
            //    {
            //        Name = model.CategoryName
            //    };
            //    _dManager.CategoryModel.setCategory(category);
            //}
            if (seller == null)
            {
                return View("Error", new string[] { "Продавца не существует" });
            }
            Lot lot = new Lot
            {
                Name = model.Name,
                Description = model.Description,
                Start_bet = model.Start_bet,
                Price = model.Start_bet,
                Min_bet = model.Start_bet / 10,
                Auction_start = DateTime.Now,
                //Auction_end = DateTime.Now.AddDays(99999),
                Auction_end = DateTime.Now.AddSeconds(40),
                Condition = condition,
                Seller = seller,
                Location = figure.Location,
                Category = category,
                PictureUrl = model.PictureUrl
            };
            _dManager.LotModel.setLot(lot);
            return RedirectToAction("SellerProfile", "Auction");
        }
        //public ActionResult AddLot(AddLotModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        string userId = _authManager.User.Identity.GetUserId(); 
        //        AppUser user = _userManager.FindById(userId);
        //        //List<Location> locations = _dManager.LocationModel.getLocations();
        //        Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).Include(f => f.Location).ToList().FirstOrDefault(f => f.AppUser == user);
        //        Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
        //        Category category = _dManager.CategoryModel.getCategoryByName(model.CategoryName);
        //        Condition condition = _dManager.ConditionModel.getConditionByName("active");
        //        //List<LotGroup> groups = _dManager.LotGroupModel.getLotGroups();
        //        //if(category == null) // Закоментить после добавления всех категорий
        //        //{
        //        //    category = new Category
        //        //    {
        //        //        Name = model.CategoryName
        //        //    };
        //        //    _dManager.CategoryModel.setCategory(category);
        //        //}
        //        if(seller != null)
        //        {
        //            Lot lot = new Lot
        //            {
        //                Name = model.Name,
        //                Description = model.Description,
        //                Start_bet = model.Start_bet,
        //                Price = model.Start_bet,
        //                Min_bet = model.Start_bet / 10,
        //                Auction_start = DateTime.Now,
        //                //Auction_end = DateTime.Now.AddDays(99999),
        //                Auction_end = DateTime.Now.AddSeconds(40),
        //                Condition = condition,
        //                Seller = seller,
        //                Location = figure.Location,
        //                Category = category,
        //                PictureUrl = model.PictureUrl
        //            };
        //            _dManager.LotModel.setLot(lot);
        //            return RedirectToAction("SellerProfile", "Auction");
        //        }
        //        else
        //        {
        //            return View("Error", new string[] { "Продавца не существует" });
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult EndAuction(int lotId)
        {
            Lot lot = _dManager._dbContext.Lots.ToList().FirstOrDefault(l => l.Lot_id == lotId);
            lot.Auction_end = DateTime.Now;
            _dManager.LotModel.editLot(lot);
            return RedirectToAction("IndexLots", "Auction", null);
        }

        [HttpPost]
        public ActionResult AddBet(AddBetModel model, int lotId) // См. "TestAppUserNull()"
        {
            ViewData["callingMethodName"] = "IndexLotsForCustomer";

            //List<Location> locations = _dManager.LocationModel.getLocations();
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            //List<Condition> conditions = _dManager.ConditionModel.getConditions();
            Lot lot = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                            .Include(l => l.Location).ToList().FirstOrDefault(l => l.Lot_id == lotId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Невалидно");
                return PartialView("newLotCard", lot);
            }
            string userId = _authManager.User.Identity.GetUserId();
            if (userId == null)
            {
                ModelState.AddModelError("", "Залогинься");
                return PartialView("newLotCard", lot);
            }
            AppUser user = _userManager.FindById(userId);
            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).ToList().FirstOrDefault(f => f.AppUser == user);
            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
            if (lot.Auction_end <= DateTime.Now)
            {
                ViewData["callingMethodName"] = "AddBetEndOfAuction";
                return PartialView("newLotCard", lot);
            }
            if (customer == null)
            {
                ModelState.AddModelError("", "Покупателя не существует");
                return PartialView("newLotCard", lot);
            }
            if (model.Amount < lot.Min_bet)
            {
                ModelState.AddModelError("", "Размер ставки должен быть равен или больше минимального");
                return PartialView("newLotCard", lot);
            }
            Bet bet = new Bet
            {
                Date = DateTime.Now,
                Amount = model.Amount,
                Lot = lot,
                Customer = customer
            };
            _dManager.BetModel.setBet(bet);

            lot.Price = model.Amount + lot.Price;
            lot.Bets_count++;
            _dManager.LotModel.editLot(lot);
            return PartialView("newLotCard", lot);
        }
        //public ActionResult AddBet(AddBetModel model, int lotId) // См. "TestAppUserNull()"
        //{
        //    //List<Location> locations = _dManager.LocationModel.getLocations();
        //    //List<Category> categories = _dManager.CategoryModel.getCategories();
        //    //List<Condition> conditions = _dManager.ConditionModel.getConditions();
        //    Lot lot = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
        //                    .Include(l => l.Location).ToList().FirstOrDefault(l => l.Lot_id == lotId);
        //    string modelError;
        //    if (ModelState.IsValid)
        //    {
        //        string userId = _authManager.User.Identity.GetUserId();
        //        if (userId != null)
        //        {
        //            AppUser user = _userManager.FindById(userId);
        //            Figure figure = _dManager._dbContext.Figures.Include(f => f.AppUser).ToList().FirstOrDefault(f => f.AppUser == user);
        //            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(s => s.Figure == figure);
        //            if (lot.Auction_end <= DateTime.Now)
        //            {
        //                ViewData["callingMethodName"] = "AddBetEndOfAuction";
        //                return PartialView("newLotCard", lot);
        //            }

        //            if (customer != null)
        //            {
        //                if (model.Amount >= lot.Min_bet)
        //                {
        //                    Bet bet = new Bet
        //                    {
        //                        Date = DateTime.Now,
        //                        Amount = model.Amount,
        //                        Lot = lot,
        //                        Customer = customer
        //                    };
        //                    _dManager.BetModel.setBet(bet);

        //                    lot.Price = model.Amount + lot.Price;
        //                    lot.Bets_count++;
        //                    _dManager.LotModel.editLot(lot);
        //                    ViewData["callingMethodName"] = "IndexLotsForCustomer";
        //                    return PartialView("newLotCard", lot);
        //                }
        //                else { modelError = "Размер ставки должен быть равен или больше минимального"; }
        //            }
        //            else { modelError = "Покупателя не существует"; }
        //        }
        //        else { modelError = "Залогинься"; }
        //    }
        //    else { modelError = "Невалидно"; }

        //    ModelState.AddModelError("", modelError);
        //    ViewData["callingMethodName"] = "IndexLotsForCustomer";
        //    return PartialView("newLotCard", lot);
        //}

        [HttpPost]
        public ActionResult AddCheck(int _lotId)
        {
            string userId = _authManager.User.Identity.GetUserId();
            AppUser currentUser = _userManager.FindById(userId);
            Figure currentFigure = _dManager._dbContext.Figures.Include(f => f.AppUser).ToList().FirstOrDefault(f => f.AppUser == currentUser);
            Customer currentCustomer = _dManager._dbContext.Customers.Include(c => c.Figure)
                .ToList().FirstOrDefault(s => s.Figure == currentFigure);
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                           .Include(l => l.Location).ToList();
            Lot lot = _dManager.LotModel.getLotById(_lotId, lots);
            Bet bet = _dManager._dbContext.Bets.Include(b => b.Customer).Include(b => b.Lot).ToList().LastOrDefault(b => b.Lot == lot);
            if (bet == null)
            {
                lot.Condition = _dManager.ConditionModel.getConditionByName("unclaimed");
                _dManager.LotModel.editLot(lot);
                return PartialView("EmptyPartial");
            }
            Customer customer = _dManager._dbContext.Customers.Include(c => c.Figure).ToList().FirstOrDefault(c => c == bet.Customer);
            Seller seller = _dManager._dbContext.Sellers.Include(s => s.Figure).ToList().FirstOrDefault(s => s == lot.Seller);
            //List<Figure> figures = _dManager.FigureModel.getFigures();
            //List<Location> locations = _dManager.LocationModel.getLocations();
            //List<Category> categories = _dManager.CategoryModel.getCategories();
            List<ShipmentOption> shipOps = _dManager._dbContext.ShipmentOptions.Include(shOp => shOp.Source).Include(shOp => shOp.Destination).ToList();
            ShipmentOption shipOp = _dManager.ShipmentOptionModel.getShipmentOptionBySD(seller.Figure.Location, customer.Figure.Location, shipOps);

            if (shipOp == null)
            {
                shipOp = new ShipmentOption()
                {
                    Cost = 500,
                    Source = lot.Seller.Figure.Location,
                    Destination = customer.Figure.Location
                };
                _dManager.ShipmentOptionModel.setShipmentOption(shipOp);
            }
            Condition conditionComplete = _dManager.ConditionModel.getConditionByName("complete");
            Condition conditionShipmentExpected = _dManager.ConditionModel.getConditionByName("shipment_expected");
            Check check = new Check()
            {
                Cost = lot.Price + shipOp.Cost,
                Lot = lot,
                Bet = bet,
                ShipmentOption = shipOp,
                Condition = conditionComplete,
                ShipmentLastDate = DateTime.Now
            };
            _dManager.CheckModel.setCheck(check);

            lot.Winner = customer;
            lot.Condition = conditionShipmentExpected;
            _dManager.LotModel.editLot(lot);
            return PartialView("EmptyPartial");
        }

        public ActionResult AddShipmentForm(int _lotId)
        {
            ViewData["lotId"] = _lotId;
            return View();
        }
        [HttpPost]
        public ActionResult AddShipment(int _lotId, string track)
        {
            List<Lot> lots = _dManager._dbContext.Lots.Include(l => l.Condition).Include(l => l.Category)
                          .Include(l => l.Location).ToList();
            Lot lot = _dManager.LotModel.getLotById(_lotId, lots);
            Check check = _dManager.CheckModel.getChecks().ToList().FirstOrDefault(chk => chk.Lot == lot);
            Condition conditionDelivered = _dManager.ConditionModel.getConditionByName("delivered");
            Shipment shipment = new Shipment
            {
                Check = check,
                Condition = conditionDelivered,
                Track = track
            };
            _dManager.ShipmentModel.setShipment(shipment);
            lot.Condition = conditionDelivered;
            _dManager.LotModel.editLot(lot);
            
            return RedirectToAction("SellerProfile", "Auction");
        }

        public ActionResult TestView()
        {
            return View();
        }

        public string TestAppUserNull() //Прежде чем получать или проверять Figure.AppUser, нужно получить лист AppUsers (уже нет)
        {
            //List<AppUser> users = _userManager.Users.ToList();
            List<Figure> figures = _dManager._dbContext.Figures.Include(f => f.AppUser).ToList().Where(figure => figure.AppUser == null).ToList(); //Если не получить лист AppUsers, то в результатах поиска будут все фигуры (даже если у них figure.AppUser на самом деле != null).

            if (figures.Count == 0)
            {
                return "ничего не найдено";
            }
            else
            {
                return $"{figures.Count}";
            }
        }
    }
}