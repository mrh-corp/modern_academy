using Domain.Users;
using Facet;
using SharedKernel;

namespace Web.Api.Responses;

[Facet(typeof(Entity), exclude: nameof(Entity.DomainEvents))]
public partial class EntityResponse;

[Facet(
    typeof(User), 
    nameof(User.PasswordHash), nameof(User.DomainEvents))]
public partial class UserResponse;
