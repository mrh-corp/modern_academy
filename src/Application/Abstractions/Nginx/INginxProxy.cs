using Application.Abstractions.Service;

namespace Application.Abstractions.Nginx;

public interface INginxProxy
{
    Task EncryptDomainName(string domainName);
    Task<bool> RemoveDomainName(string domainName);
}
