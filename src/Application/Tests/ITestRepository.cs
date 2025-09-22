using Application.Abstractions.Service;
using Domain.Notes;
using SharedKernel;
using OneOf;

namespace Application.Tests;

public interface ITestRepository : IService
{
    Task<OneOf<Error, Trimester>> CreateTrimester();
}
