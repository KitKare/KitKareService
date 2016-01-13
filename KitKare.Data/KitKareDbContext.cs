using KitKare.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitKare.Data
{
    public class KitKareDbContext : IdentityDbContext<User>
    {
        private const string DefaultConnection = "DefaultConnection";
        private const string AppharborConnection = "AppharborConnection";

        public KitKareDbContext()
            : base(DefaultConnection, throwIfV1Schema: false)
        {
        }

        public virtual DbSet<CatCareTip> CatCareTips { get; set; }

        public virtual DbSet<Video> Videos { get; set; }

        public static KitKareDbContext Create()
        {
            return new KitKareDbContext();
        }
    }
}
