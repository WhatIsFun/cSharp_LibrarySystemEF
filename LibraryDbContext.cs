using cSharp_LibrarySystemEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace cSharp_LibrarySystemEF
{
    public  class LibraryDbContext : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Patron> Patron { get; set; }
        public DbSet<BorrowingTransaction> BorrowingTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(local);Initial Catalog=LibrarySysem; Integrated Security=true; TrustServerCertificate=True");
        }


    }
}
