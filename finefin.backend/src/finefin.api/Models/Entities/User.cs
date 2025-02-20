﻿namespace finefin.api.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public virtual ICollection<Wallet> Wallets { get; set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
