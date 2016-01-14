namespace KitKare.Server.Controllers
{
    using Data.Models;
    using Data.Repositories;
    using System;
    using System.Linq;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using ViewModels;
    [Authorize]
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        private IRepository<User> users;
        private IRepository<Feeding> feedings;

        private string userId;

        public ProfileController(IRepository<User> users, IRepository<Feeding> feedings)
        {
            this.users = users;
            this.feedings = feedings;

            this.userId = this.users
                .All()
                .Where(x => x.UserName == this.User.Identity.Name)
                .FirstOrDefault()
                .Id;
        }

        [HttpGet]
        [Route("GetFeedings")]
        public IHttpActionResult GetFeedings()
        {
            var feedings = this.feedings
                .All()
                .Where(x => x.UserId == this.userId)
                .ToList();

            return this.Ok(feedings);
        }

        [HttpGet]
        [Route("GetProfile")]
        public IHttpActionResult GetProfile()
        {
            var profile = this.users
                .All()
                .Where(x => x.Id == this.userId)
                .ProjectTo<ProfileViewModel>()
                .FirstOrDefault();

            return this.Ok(profile);
        }
    }
}