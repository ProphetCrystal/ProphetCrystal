using Microsoft.AspNetCore.Authorization;

namespace ProphetCrystal.Authorization;

public class RoleHierarchyRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }

    public RoleHierarchyRequirement(string requiredRole)
    {
        RequiredRole = requiredRole;
    }
}