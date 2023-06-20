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
    public class RidesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RidesData/ListRides
        [HttpGet]
        public IEnumerable<RideDto> ListRides()
        {
            List<Ride> Rides = db.Rides.ToList();
            List<RideDto> RideDtos = new List<RideDto>();
            Rides.ForEach(a => RideDtos.Add(new RideDto()
            {
                RideId = a.RideId,
                RideName = a.RideName,
                RideNum = a.RideNum
            }));

            return RideDtos;
        }

        // GET: api/RidesData/FindRides/5
        [HttpGet]
        [ResponseType(typeof(Ride))]
        public IHttpActionResult FindRide(int id)
        {
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return NotFound();
            }

            RideDto rideDto = new RideDto()
            {
                RideId = ride.RideId,
                RideName = ride.RideName,
                RideNum = ride.RideNum
            };

            return Ok(rideDto);
        }

        // PUT: api/RidesData/UpdateRide/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateRide(int id, Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ride.RideId)
            {
                return BadRequest();
            }

            db.Entry(ride).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
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

        // POST: api/RidesData/AddRide
        [HttpPost]
        [ResponseType(typeof(Ride))]
        public IHttpActionResult AddRide(Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rides.Add(ride);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ride.RideId }, ride);
        }

        // DELETE: api/RidesData/DeleteRide/5
        [HttpPost]
        [ResponseType(typeof(Ride))]
        public IHttpActionResult DeleteRide(int id)
        {
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return NotFound();
            }

            db.Rides.Remove(ride);
            db.SaveChanges();

            return Ok(ride);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RideExists(int id)
        {
            return db.Rides.Count(e => e.RideId == id) > 0;
        }
    }
}
