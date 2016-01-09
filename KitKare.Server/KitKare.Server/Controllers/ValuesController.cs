using KitKare.Data.Models;
using KitKare.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using KitKare.Server.ViewModels;

namespace KitKare.Server.Controllers
{
    public class ValuesController : ApiController
    {
        private IRepository<User> users;

        public ValuesController(IRepository<User> users)
        {
            this.users = users;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var user = this.users.All().ProjectTo<ProfileViewModel>().FirstOrDefault();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
