using System.Threading.Tasks;
using Auditing.Domain.Entities;
using Auditing.Infrastructure.Repositories;
using Auditing.Tests.Helpers;
using Xunit;

namespace Auditing.Tests.Repositories;

public class OwnerRepositoryTests
{
    [Fact]
    public async Task addAsync_getByIdAsync_shouldPersistAndRetrieve()
    {
        using var ctx = DbContextHelper.Create(nameof(addAsync_getByIdAsync_shouldPersistAndRetrieve));
        var owners = new OwnerRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        var result = await owners.GetByIdAsync(owner.Id);

        Assert.NotNull(result);
        Assert.Equal("Acme", result!.Name);
        Assert.Equal("acme@mail.com", result.Email);
        Assert.Equal("Finance", result.Area);
    }

    [Fact]
    public async Task updateAsync_shouldModifyOwner()
    {
        using var ctx = DbContextHelper.Create(nameof(updateAsync_shouldModifyOwner));
        var owners = new OwnerRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        owner.Update("Beta", "beta@mail.com", "Operations");
        await owners.UpdateAsync(owner);

        var result = await owners.GetByIdAsync(owner.Id);

        Assert.NotNull(result);
        Assert.Equal("Beta", result!.Name);
        Assert.Equal("beta@mail.com", result.Email);
        Assert.Equal("Operations", result.Area);
    }
}