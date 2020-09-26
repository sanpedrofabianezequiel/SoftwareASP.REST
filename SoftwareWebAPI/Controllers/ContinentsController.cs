using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SoftwareWebAPI.Data;
using SoftwareWebAPI.Models;

namespace SoftwareWebAPI.Controllers
{
    public class ContinentsController : ApiController
    {
        private SoftwareWebAPIContext db = new SoftwareWebAPIContext();

        // GET: api/Continents
        public IQueryable<Continent> GetContinents()
        {
            return db.Continents;
        }

        // GET: api/Continents/5
        [ResponseType(typeof(Continent))]
        public async Task<IHttpActionResult> GetContinent(int id)
        {
            Continent continent = await db.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            return Ok(continent);
        }

        // PUT: api/Continents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContinent(int id, Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != continent.Id)
            {
                return BadRequest();
            }

            db.Entry(continent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/Continents
        [ResponseType(typeof(Continent))]
        public async Task<IHttpActionResult> PostContinent(Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Continents.Add(continent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = continent.Id }, continent);
        }

        // DELETE: api/Continents/5
        [ResponseType(typeof(Continent))]
        public async Task<IHttpActionResult> DeleteContinent(int id)
        {
            Continent continent = await db.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            db.Continents.Remove(continent);
            await db.SaveChangesAsync();

            return Ok(continent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContinentExists(int id)
        {
            return db.Continents.Count(e => e.Id == id) > 0;
        }
    }
}