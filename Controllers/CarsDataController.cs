using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UberApplication.Models;

namespace UberApplication.Controllers
{
    public class CarsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CarsData
        [HttpGet]
        [ResponseType(typeof(CarDto))]
        public IHttpActionResult GetCars()
        {
            List<Car> Cars = db.Cars.ToList();
            List<CarDto> CarDtos = new List<CarDto>();
            Cars.ForEach(c => CarDtos.Add(new CarDto()
            {
                CarID = c.CarID,
                CarName = c.CarName,
                CarRemoved = c.CarRemoved
            }));

            return Ok(CarDtos);
        }

        // GET: api/Cars/5
        [HttpGet]
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            CarDto carDto = new CarDto()
            {
                CarID = car.CarID,
                CarName = car.CarName,
                CarRemoved = car.CarRemoved
            };

            return Ok(carDto);
        }

        // PUT: api/CarsData/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarID)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarsData
        [HttpPost]
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car.CarID }, car);
        }

        // DELETE: api/CarsData/5
        [HttpPost]
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.CarID == id) > 0;
        }
    }
}