using System.Collections.Generic;

namespace UniHub.Data.Entities
{
    public class RoleType : BaseEnum
    {
        public virtual ICollection<User> User { get; set; }
    }
}