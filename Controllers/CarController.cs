using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using UberApplication.Models;
using System.Web.Script.Serialization;

namespace UberApplication.Controllers
{
    public class CarController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CarController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/");
        }

        // GET: Car/List
        public ActionResult Index()
        {
            string url = "CarsData/GetCars";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CarDto> cars = response.Content.ReadAsAsync<IEnumerable<CarDto>>().Result;

            return View(cars);
        }

        public ActionResult Details(int id)
        {
            string url = "CarsData/GetCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CarDto selectedCar = response.Content.ReadAsAsync<CarDto>().Result;

            return View(selectedCar);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(CarDto car)
        {
            string url = "CarsData/PostCar";

            string jsonPayload = jss.Serialize(car);

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

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "CarsData/GetCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CarDto selectedCar = response.Content.ReadAsAsync<CarDto>().Result;

            return View(selectedCar);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CarDto car)
        {
            string url = "CarsData/PutCar/" + id;

            string jsonPayload = jss.Serialize(car);

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

        // GET: Car/Delete
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CarsData/GetCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CarDto selectedCar = response.Content.ReadAsAsync<CarDto>().Result;

            return View(selectedCar);
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "CarsData/DeleteCar/" + id;
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
