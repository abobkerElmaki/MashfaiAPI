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
using HospitalApiSA.Models;

namespace HospitalApiSA.Controllers
{
    public class CitiesController : ApiController
    {
        private myHospitalEntities db = new myHospitalEntities();

        // GET: api/Cities
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        public HttpResponseMessage Get(int id)
        {
            using (myHospitalEntities entities = new myHospitalEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                entities.Configuration.LazyLoadingEnabled = false;

                var entity = entities.Cities.FirstOrDefault(e => e.CityID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "City with Id " + id.ToString() + " not found");
                }
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]City city)
        {
            try
            {
                using (myHospitalEntities entities = new myHospitalEntities())
                {
                    var entity = entities.Cities.FirstOrDefault(e => e.CityID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "City with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.CityName = city.CityName;
                        entity.CityCode = city.CityCode;
                        
                        entities.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.OK);
                        message.Headers.Location = new Uri(Request.RequestUri.ToString());

                        return message;
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody] City city)
        {
            try
            {
                using (myHospitalEntities entities = new myHospitalEntities())
                {
                    entities.Cities.Add(city);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, city);
                    message.Headers.Location = new Uri(Request.RequestUri +"/"+
                        city.CityID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (myHospitalEntities entities = new myHospitalEntities())
                {
                    var entity = entities.Cities.FirstOrDefault(e => e.CityID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "City with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Cities.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.CityID == id) > 0;
        }
    }
}