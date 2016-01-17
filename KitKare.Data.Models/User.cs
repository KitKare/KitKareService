namespace KitKare.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        public User()
        {
            this.Feedings = new HashSet<Feeding>();
            this.VetChecks = new HashSet<VetCheck>();
        }

        public string CatName { get; set; }

        public string TeleduinoKey { get; set; }

        public string VetPhone { get; set; }

        public bool LightsAreOn { get; set; }

        public virtual ICollection<Feeding> Feedings { get; set; }

        public virtual ICollection<VetCheck> VetChecks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
