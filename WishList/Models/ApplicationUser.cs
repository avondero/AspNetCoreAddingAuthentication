namespace WishList.Models
{
    #region Usings

    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    #endregion

    public class ApplicationUser : IdentityUser
    {
        #region Propriétés et indexeurs

        public virtual ICollection<Item> Items { get; set; }

        #endregion
    }
}
