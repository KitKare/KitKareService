namespace KitKare.Server.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using KitKare.Data.Models;
    using KitKare.Data.Repositories;

    [Authorize]
    public abstract class BaseAuthorizationController : ApiController
    {
        protected readonly IRepository<User> users;

        private string currentUserId;

        public BaseAuthorizationController(IRepository<User> users)
        {
            this.users = users;
        }

        protected string CurrentUserId
        {
            get
            {
                if (string.IsNullOrEmpty(this.currentUserId))
                {
                    this.currentUserId = this.users
                        .All()
                        .Where(x => x.UserName == this.User.Identity.Name)
                        .Select(x => x.Id)
                        .FirstOrDefault();
                }

                return this.currentUserId;
            }
        }
    }
}