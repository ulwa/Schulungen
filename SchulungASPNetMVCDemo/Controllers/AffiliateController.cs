using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SchulungASPNetMVCDemo.Models;
using SchulungASPNetMVCDemo.Services;

namespace SchulungASPNetMVCDemo.Controllers
{
    public class AffiliateController : Controller
    {
        private readonly IAzureTableAccess<AffiliateModel> _azureTableAccessService;

        public AffiliateController(IAzureTableAccess<AffiliateModel> azureTableAccess)
            => _azureTableAccessService = azureTableAccess;


        // GET: Affiliate
        public ActionResult Index()
        {
            return View();
        }

        // GET: Affiliate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Affiliate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Affiliate/Create
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("");

                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create the CloudTable object that represents the "people" table.
                CloudTable table = tableClient.GetTableReference("affiliates");
                await table.CreateIfNotExistsAsync();
                // Create a new customer entity.
                var model = new AffiliateModel();
                model.Affiliate = collection["Affiliate"];
                model.Article = collection["Article"];
                model.PartitionKey = "AffiliateUtil";
                model.RowKey = $"{collection["Affiliate"]}|{collection["Article"]}";

                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.InsertOrReplace(model);

                // Execute the insert operation.
                await table.ExecuteAsync(insertOperation);
               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Affiliate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Affiliate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Affiliate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Affiliate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}