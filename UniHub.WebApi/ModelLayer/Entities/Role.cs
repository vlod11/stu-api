using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Role : BaseEnum
    {
        //Relation to UsersProfiles
        public virtual ICollection<UsersProfile> UsersProfiles { get; set; }
    }
}