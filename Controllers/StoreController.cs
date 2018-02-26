using KeysOnboarding2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeysOnboarding2.Controllers
{
    public class StoreController : Controller
    {
        DatabaseContext dbContext;

        public StoreController()
        {
            dbContext = new DatabaseContext();
        }

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStoreList()
        {
            List<ViewModel.StoreViewModel> list = dbContext.Stores.Select(x => new ViewModel.StoreViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreById(int storeId)
        {
            var store = dbContext.Stores.FirstOrDefault(x => x.Id == storeId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(store, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(ViewModel.StoreViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var store = dbContext.Stores.FirstOrDefault(x => x.Id == model.Id);
                    store.Name = model.Name;
                    store.Address = model.Address;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    Store store = new Store();
                    store.Name = model.Name;
                    store.Address = model.Address;
                    dbContext.Stores.Add(store);
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStore(int storeId)
        {
            bool result = false;
            var store = dbContext.Stores.FirstOrDefault(x => x.Id == storeId);
            if (store != null)
            {
                dbContext.Stores.Remove(store);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}