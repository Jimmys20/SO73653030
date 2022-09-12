using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace SO73653030
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                //var userName = claims.FirstOrDefault(c => c.Type == "UserId").Value;
                var roles = requirement.AllowedRoles;

                validRole = context.User.Claims.Any(c => c.Type == ClaimsHelper.Claim_UserRole
                    && roles.Contains(c.Value));
                //validRole = new Users().GetUsers().Where(p => roles.Contains(p.Role) && p.UserName == userName).Any();

            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}
