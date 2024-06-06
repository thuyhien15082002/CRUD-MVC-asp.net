using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Services
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions options): base(options) {

        }
        public DbSet<Product> products { get; set; }
        



    }

}
