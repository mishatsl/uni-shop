using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class AppContextInitializer : DropCreateDatabaseAlways<Domain.Concrete.EFDbContext>
    {
        protected override void Seed(Domain.Concrete.EFDbContext context)
        {
            

            base.Seed(context);
        }
    }
}