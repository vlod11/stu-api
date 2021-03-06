using System;

namespace UniHub.WebApi.Models.ModelDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public int CurrencyCount { get; set; } = 0;
        public DateTime LastVisit { get; set; }
    }
}