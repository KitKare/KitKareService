namespace KitKare.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using KitKare.Server.ViewModels;
    using KitKare.Data.Models;
    using KitKare.Data.Repositories;

    [RoutePrefix("api/CatCareTips")]
    public class CatCareTipsController : ApiController
    {
        private IRepository<CatCareTip> tips;

        public CatCareTipsController(IRepository<CatCareTip> tips)
        {
            this.tips = tips;
        }

        [Route("AllTips")]
        [HttpGet]
        public IHttpActionResult AllTips()
        {
            var tips = this.tips
                .All()
                .ProjectTo<CatCareTipViewModel>()
                .ToList();

            return this.Ok(tips);
        }
        
        [Route("LastModified")]
        [HttpGet]
        public IHttpActionResult LastModified()
        {
            var lastModifiedTip = this.tips
                .All()
                .OrderByDescending(x => x.ModifiedOn)
                .FirstOrDefault();

            if (lastModifiedTip == null)
            {
                return this.Ok(default(DateTime?));
            }
            else
            {
                return this.Ok(lastModifiedTip.ModifiedOn.Value);
            }
        }
   } 
}