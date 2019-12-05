using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TourManagement.API.Services;

namespace TourManagement.API.Authorization
{
    public class UserMustBeTourManagerRequirementHandler : AuthorizationHandler<UserMustBeTourManagerRequirement>
    {
        private ITourManagementRepository _tourManagementRepository;
        private readonly IUserInfoService _userInfoService;

        public UserMustBeTourManagerRequirementHandler(ITourManagementRepository tourManagementRepository,
            IUserInfoService userInfoService)
        {
            _tourManagementRepository = tourManagementRepository;
            _userInfoService = userInfoService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserMustBeTourManagerRequirement requirement)
        {
            if (_userInfoService.Role == requirement.Role)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }

            var tourId = filterContext.RouteData.Values["tourId"].ToString();

            if (!(Int32.Parse(tourId) > 0))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            if (!(Int32.Parse(_userInfoService.UserId) > 0))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            context.Succeed(requirement);
            return Task.FromResult(0);
        }
    }
}