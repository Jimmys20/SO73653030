using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace SO73653030
{
    public class SampleAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                if (authorizeResult.AuthorizationFailure!.FailedRequirements
                        .OfType<CustomUserRequireClaim>().Any())
                {
                    context.Response.Redirect("/subscriptions/upgrade-to-access");
                    return;
                }

                if (authorizeResult.AuthorizationFailure!.FailedRequirements
                        .OfType<RolesAuthorizationRequirement>().Any())
                {
                    context.Response.Redirect("/accounts/ask-your-manager");
                    return;
                }
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
