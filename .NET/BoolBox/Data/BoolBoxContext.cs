using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoolBox.Models;

namespace BoolBox.Data
{
    public class BoolBoxContext : DbContext
    {
        public BoolBoxContext (DbContextOptions<BoolBoxContext> options)
            : base(options)
        {
        }

        public DbSet<BoolBox.Models.Bool> Bool { get; set; }
    }
}
