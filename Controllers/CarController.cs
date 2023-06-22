using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using UberApplication.Models;
using System.Web.Script.Serialization;
using UberApplication.Models.viewmodels;
using UberApplication.Migrations;

namespace UberApplication.Controllers
{
    public class CarController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        public object ResponsibleDrivers { get; private set; }
        public object AvailableDrivers { get; private set; }

        static CarController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
    
        // GET: Car/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/ridesdata/listrides


            string url = "animaldata/listanimals";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<RideDto> animals = response.Content.ReadAsAsync<IEnumerable<RideDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(animals);
        }
        public ActionResult Details(int id)
        {
            DetailsRides ViewModel = new DetailsRides();

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/animaldata/findanimal/{id}

            string url = "ridesdata/findride/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            RideDto SelectedRide = response.Content.ReadAsAsync<RideDto>().Result;
            Debug.WriteLine("Ride arrived : ");
            Debug.WriteLine(SelectedRide.RideName);

            ViewModel.SelectedRide = SelectedRide;

            //show associated keepers with this animal
            url = "cardata/listcars/"+id;
            response = client.GetAsync(url).Result;
            IEnumerable<RideDto> ResponsibleKeepers = response.Content.ReadAsAsync<IEnumerable<RideDto>>().Result;

            ViewModel.ResponsibleDrivers = ResponsibleDrivers;

            url = "keeperdata/listkeepersnotcaringforanimal/" + id;
                response = client.GetAsync(url).Result;
            IEnumerable<RideDto> AvailableKeepers = response.Content.ReadAsAsync<IEnumerable<RideDto>>().Result;

            ViewModel.AvailableDrivers = AvailableDrivers;


            return View(ViewModel);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, object rides)
        {
            Debug.WriteLine("the json payload is :");
            
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/ridesdata/addride 
            string url = "ridesdata/addride";


            string jsonpayload = jss.Serialize(rides);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }


        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateRides ViewModel = new UpdateRides();

            //the existing animal information
            string url = "ridesdata/findride/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RideDto SelectedRide = response.Content.ReadAsAsync<RideDto>().Result;
            ViewModel.SelectedRide = SelectedRide;

          

            return View(ViewModel);
        }


        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ridesdata/findride/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RideDto selectedride = response.Content.ReadAsAsync<RideDto>().Result;
            return View(selectedride);
        }

        // POST: car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "ridesdata/deleteride/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}