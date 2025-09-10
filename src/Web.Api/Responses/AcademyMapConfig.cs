using Domain.Academies;
using Domain.Users;
using Facet.Extensions;
using Facet.Mapping;

namespace Web.Api.Responses;

public class AcademyMapConfig : IFacetMapConfiguration<Academy, AcademyResponse>
{
    public static void Map(Academy source, AcademyResponse target)
    {
        target.Administrators = source.Administrators.SelectFacets<User, UserResponse>().ToList();
    }
}
