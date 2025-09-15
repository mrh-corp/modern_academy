namespace Web.Api.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class TenantRequiredAttribute : Attribute
{
    
}
