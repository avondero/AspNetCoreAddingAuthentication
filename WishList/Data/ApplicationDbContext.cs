namespace WishList.Data
{
    #region Usings

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using WishList.Models;

    #endregion

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructeurs et destructeurs

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        #endregion

        #region Propriétés et indexeurs

        public DbSet<Item> Items { get; set; }

        #endregion
    }
}
