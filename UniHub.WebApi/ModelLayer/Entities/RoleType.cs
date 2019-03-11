using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class RoleType : BaseEnum
    {
        public virtual ICollection<UsersProfile> UsersProfiles { get; set; }
    }
}