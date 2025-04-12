using Microsoft.AspNetCore.Authorization;

namespace Crystalis.Authorization;

public class RoleHierarchyHandler : AuthorizationHandler<RoleHierarchyRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleHierarchyRequirement requirement)
    {
        if (context.User == null) return Task.CompletedTask;

        // Define role hierarchy
        Dictionary<string, List<string>> roleHierarchy = new()
        {
            { "Admin", new List<string> { "Admin", "GameMaster", "Player" } },
            { "GameMaster", new List<string> { "GameMaster", "Player" } },
            { "Player", new List<string> { "Player" } }
        };

        // Check if user has any of the required roles
        foreach (string role in roleHierarchy.Keys)
            if (context.User.IsInRole(role) && roleHierarchy[role].Contains(requirement.RequiredRole))
            {
                context.Succeed(requirement);
                break;
            }

        return Task.CompletedTask;
    }
}