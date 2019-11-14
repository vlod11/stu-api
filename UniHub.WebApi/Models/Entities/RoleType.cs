using System.Collections.Generic;

namespace UniHub.WebApi.Models.Entities
{
    public class RoleType : BaseEnum
    {
        public virtual ICollection<User> User { get; set; }
    }
}