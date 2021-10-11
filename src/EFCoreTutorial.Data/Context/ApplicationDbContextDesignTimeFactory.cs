using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreTutorial.Data.Context
{
    public class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var context = new ApplicationDbContext();

            return context;
        }
    }
}
