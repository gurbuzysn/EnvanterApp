using EnvanterApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Persistence.Context
{
    public class EnvanterAppDbContext : IdentityDbContext<AppUser>
    {
    }
}
