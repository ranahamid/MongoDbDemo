using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Models;
using RepositoryNoSQL;

namespace MongoDB.Controllers
{

    public class HomeController : Controller
    {
        MT4Trades obje = new MT4Trades();
        IMongoDbRepository<MT4Trades> obj = new MongoDbRepository<MT4Trades>();
        IEnumerable<MT4Trades> list;

        public ActionResult Insert()
        {
            //insert
            var blog = new MT4Trades()
            {
                Name = "Rana Hamid" + DateTime.Now.Ticks.ToString(),
                Address = "Mirpur 10"+ DateTime.Now.Ticks.ToString(),
                Address2 = "Dhaka"+ DateTime.Now.Ticks.ToString()
            };

            try
            {
              obj.Insert(blog); 
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }


        public ActionResult Index()
        {
            try
            {
                list = obj.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
            return View(list);
        }

        public ActionResult Delete()
        {
            try
            {
                obj.Delete(obj.GetFirstId());//works
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
         public ActionResult Update()
        {
            var blog = new MT4Trades()
            {
                Name = "Md. Rana Hamid" + DateTime.Now.Ticks.ToString(),
                Address = "Mirpur 10",
                Address2 = "Dhaka"
            };

            blog.Id = obj.GetFirstId();

            try
            {
              obj.Update(blog); 
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult About()
        {

            try
            {
                obje = obj.FindById(new Guid("af0a98da-5465-45e4-b608-ada9f0514775"));//works
            }
            catch (Exception)
            {

                throw;
            }
            return View(obje);
        }

     
    }
}