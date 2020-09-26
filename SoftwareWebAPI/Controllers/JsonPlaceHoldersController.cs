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
    public class JsonPlaceHoldersController : ApiController
    {
        private JsonPlaceHolderContext db = new JsonPlaceHolderContext();

        // GET: api/JsonPlaceHolders
        public IQueryable<JsonPlaceHolder> GetJsonPlaceHolders()
        {
            return db.JsonPlaceHolders;
        }

        // GET: api/JsonPlaceHolders/5
        [ResponseType(typeof(JsonPlaceHolder))]
        public async Task<IHttpActionResult> GetJsonPlaceHolder(int id)
        {
            JsonPlaceHolder jsonPlaceHolder = await db.JsonPlaceHolders.FindAsync(id);
            if (jsonPlaceHolder == null)
            {
                return NotFound();
            }

            return Ok(jsonPlaceHolder);
        }

        // PUT: api/JsonPlaceHolders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJsonPlaceHolder(int id, JsonPlaceHolder jsonPlaceHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jsonPlaceHolder.id)
            {
                return BadRequest();
            }

            db.Entry(jsonPlaceHolder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JsonPlaceHolderExists(id))
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

        // POST: api/JsonPlaceHolders
        [ResponseType(typeof(JsonPlaceHolder))]
        public async Task<IHttpActionResult> PostJsonPlaceHolder(JsonPlaceHolder jsonPlaceHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JsonPlaceHolders.Add(jsonPlaceHolder);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = jsonPlaceHolder.id }, jsonPlaceHolder);
        }

        // DELETE: api/JsonPlaceHolders/5
        [ResponseType(typeof(JsonPlaceHolder))]
        public async Task<IHttpActionResult> DeleteJsonPlaceHolder(int id)
        {
            JsonPlaceHolder jsonPlaceHolder = await db.JsonPlaceHolders.FindAsync(id);
            if (jsonPlaceHolder == null)
            {
                return NotFound();
            }

            db.JsonPlaceHolders.Remove(jsonPlaceHolder);
            await db.SaveChangesAsync();

            return Ok(jsonPlaceHolder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JsonPlaceHolderExists(int id)
        {
            return db.JsonPlaceHolders.Count(e => e.id == id) > 0;
        }
    }
}