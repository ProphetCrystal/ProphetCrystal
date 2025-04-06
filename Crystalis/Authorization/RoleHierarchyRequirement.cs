using Microsoft.AspNetCore.Authorization;

namespace Crystalis.Authorization;

public class RoleHierarchyRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }

    public RoleHierarchyRequirement(string requiredRole)
    {
        RequiredRole = requiredRole;
    }
}