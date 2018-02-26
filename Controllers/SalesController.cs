using KeysOnboarding2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeysOnboarding2.Controllers
{
    public class SalesController : Controller
    {
        DatabaseContext dbContext;

        public SalesController()
        {
            dbContext = new DatabaseContext();
        }

        // GET: Sales
        public ActionResult Index()
        {
            List<Product> productList = dbContext.Products.ToList();
            ViewBag.Products = new SelectList(productList, "Id", "Name");

            List<Customer> customerList = dbContext.Customers.ToList();
            ViewBag.Customers = new SelectList(customerList, "Id", "Name");

            List<Store> storeList = dbContext.Stores.ToList();
            ViewBag.Stores = new SelectList(storeList, "Id", "Name");

            return View();
        }

        public JsonResult GetSalesList()
        {
            List<ViewModel.SalesViewModel> list = dbContext.ProductSold.Select(x => new ViewModel.SalesViewModel
            {
                Id = x.Id,
                ProductId = x.Product.Id,
                CustomerId = x.CustomerId,
                StoreId = x.StoreId,
                DateSold = x.DateSold,
                ProductName = x.Product.Name,
                CustomerName = x.Customer.Name,
                StoreName = x.Store.Name
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalesById(int salesId)
        {
            var sales = dbContext.ProductSold.FirstOrDefault(x => x.Id == salesId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(sales, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(ViewModel.SalesViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var sales = dbContext.ProductSold.FirstOrDefault(x => x.Id == model.Id);
                    sales.ProductId = model.ProductId;
                    sales.CustomerId = model.CustomerId;
                    sales.StoreId = model.StoreId;
                    sales.DateSold = model.DateSold;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    ProductSold sales = new ProductSold();
                    sales.ProductId = model.ProductId;
                    sales.CustomerId = model.CustomerId;
                    sales.StoreId = model.StoreId;
                    sales.DateSold = model.DateSold;
                    dbContext.ProductSold.Add(sales);
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

        public JsonResult DeleteSales(int salesId)
        {
            bool result = false;
            var sales = dbContext.ProductSold.FirstOrDefault(x => x.Id == salesId);
            if (sales != null)
            {
                dbContext.ProductSold.Remove(sales);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}