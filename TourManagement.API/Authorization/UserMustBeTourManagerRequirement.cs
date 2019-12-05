using Microsoft.AspNetCore.Authorization;

namespace TourManagement.API.Authorization
{
    public class UserMustBeTourManagerRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }

        public UserMustBeTourManagerRequirement(string role)
        {
            Role = role;
        }
    }
}