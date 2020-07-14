using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EShop.BackEnd.Models
{
    public class User : IdentityUser
    {
        public AccountType TypeUser { get; set; }
        public IList<Order> OrderHistories { get; set; }
    }
}
