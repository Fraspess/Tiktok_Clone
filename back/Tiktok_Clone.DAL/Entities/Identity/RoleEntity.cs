using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiktok_Clone.DAL.Entities.Identity
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; } = new List<UserRoleEntity>();
        public RoleEntity() : base() { }

        public RoleEntity(string roleName) : base(roleName) { }
    }
}
