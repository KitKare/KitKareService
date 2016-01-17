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
    
    [RoutePrefix("api/Profile")]
    public class ProfileController : BaseAuthorizationController
    {
        private IRepository<Feeding> feedings;

        public ProfileController(IRepository<User> users, IRepository<Feeding> feedings)
            : base(users)
        {
            this.feedings = feedings;
        }

        [HttpGet]
        [Route("GetFeedings")]
        public IHttpActionResult GetFeedings()
        {
            var feedings = this.feedings
                .All()
                .Where(x => x.UserId == this.CurrentUserId)
                .ToList();

            return this.Ok(feedings);
        }

        [HttpGet]
        [Route("GetProfile")]
        public IHttpActionResult GetProfile()
        {
            var profile = this.users
                .All()
                .Where(x => x.Id == this.CurrentUserId)
                .ProjectTo<ProfileViewModel>()
                .FirstOrDefault();

            return this.Ok(profile);
        }
    }
}