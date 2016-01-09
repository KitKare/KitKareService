using KitKare.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitKare.Data
{
    public class KitKareDbContext : IdentityDbContext<User>
    {
        public KitKareDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static KitKareDbContext Create()
        {
            return new KitKareDbContext();
        }
    }
}
