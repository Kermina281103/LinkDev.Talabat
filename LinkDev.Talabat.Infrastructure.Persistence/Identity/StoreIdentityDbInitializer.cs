using LinkDev.Talabat.Domain.Contract.Persistence.DbInitializer;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity
{
     class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext,UserManager<ApplicationUser> _userManager) :DbInitializer(_dbContext), IStoreIdentityDbInitializer
    {


        public override async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {

            var user = new ApplicationUser()
            {
                DisplayName = "Kermina Maged",
                UserName = "Kermina.Maged",
                Email = "carmina.maged.matta@gmail.com",
                PhoneNumber = "01229548237"

            };
            await _userManager.CreateAsync(user, "P@ssw0rd");
            }
           
        }
    }
}
