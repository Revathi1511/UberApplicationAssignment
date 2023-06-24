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
    public class DriversDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DriversData
        [HttpGet]
        [ResponseType(typeof(DriverDto))]
        public IHttpActionResult GetDrivers()
        {
            
            List<Driver> Drivers = db.Drivers.ToList();
            List<DriverDto> DriverDto = new List<DriverDto>();
            Drivers.ForEach(c => DriverDto.Add(new DriverDto()
            {
                DriverID = c.DriverID,
                DriverFirstName = c.DriverFirstName,
                DriverLastName = c.DriverLastName
            }));

            return Ok(DriverDto);
        }

        // GET: api/DriversData/5
        [HttpGet]
        [ResponseType(typeof(void))]
        public IHttpActionResult GetDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            DriverDto DriverDto = new DriverDto()
            {
                DriverID = driver.DriverID,
                DriverFirstName = driver.DriverFirstName,
                DriverLastName = driver.DriverLastName
            };

            return Ok(DriverDto);
        }

        // PUT: api/DriversData/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.DriverID)
            {
                return BadRequest();
            }

            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/DriversData
        [ResponseType(typeof(Driver))]
        [HttpPost]
        public IHttpActionResult PostDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drivers.Add(driver);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = driver.DriverID }, driver);
        }

        // DELETE: api/DriversData/5
        [ResponseType(typeof(Driver))]
        [HttpPost]
        public IHttpActionResult DeleteDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            db.Drivers.Remove(driver);
            db.SaveChanges();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DriverExists(int id)
        {
            return db.Drivers.Count(e => e.DriverID == id) > 0;
        }
    }
}