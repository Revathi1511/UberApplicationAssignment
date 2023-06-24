using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using UberApplication.Models;
using System.Web.Script.Serialization;

namespace UberApplication.Controllers
{
    public class DriversController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DriversController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/");
        }

        // GET: Driver/List
        public ActionResult Index()
        {
            string url = "DriversData/GetDrivers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DriverDto> drivers = response.Content.ReadAsAsync<IEnumerable<DriverDto>>().Result;

            return View(drivers);
        }

        public ActionResult Details(int id)
        {
            string url = "DriversData/GetDriver/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DriverDto selectedDriver = response.Content.ReadAsAsync<DriverDto>().Result;

            return View(selectedDriver);
        }

        // GET: Driver/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Driver/Create
        [HttpPost]
        public ActionResult Create(DriverDto driver)
        {
            string url = "DriversData/PostDriver/";

            string jsonPayload = jss.Serialize(driver);

            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Driver/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "DriversData/GetDriver/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DriverDto selectedDriver = response.Content.ReadAsAsync<DriverDto>().Result;

            return View(selectedDriver);
        }

        // POST: Driver/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DriverDto driver)
        {
            string url = "DriversData/Putdriver/" + id;

            string jsonPayload = jss.Serialize(driver);

            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Driver/Delete
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DriversData/GetDriver/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DriverDto selectedDriver = response.Content.ReadAsAsync<DriverDto>().Result;

            return View(selectedDriver);
        }

        // POST: Driver/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DriversData/DeleteDriver/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
