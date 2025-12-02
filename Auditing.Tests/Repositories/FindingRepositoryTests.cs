using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Infrastructure.Repositories;
using Auditing.Tests.Helpers;

public class FindingRepositoryTests
{
    [Fact]
    public async Task addAsync_getByIdAsync_shouldPersistAndRetrieve()
    {
        using var ctx = DbContextHelper.Create(nameof(addAsync_getByIdAsync_shouldPersistAndRetrieve));
        var owners = new OwnerRepository(ctx);
        var audits = new AuditRepository(ctx);
        var findings = new FindingRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        var audit = Audit.Create(
            title: "Q4",
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow.AddDays(1),
            auditedArea: "Finance",
            ownerId: owner.Id,
            status: AuditStatus.Pending
        );
        await audits.AddAsync(audit);

        var finding = new Finding(audit.Id, SeverityLevel.Medium, "Medium severity finding");
        await findings.AddAsync(finding);

        var result = await findings.GetByIdAsync(finding.Id);

        Assert.NotNull(result);
        Assert.Equal(SeverityLevel.Medium, result!.Severity);
        Assert.Equal("Medium severity finding", result.Description);
    }

    [Fact]
    public async Task getByAuditAndSeverityAsync_shouldFilterCorrectly()
    {
        using var ctx = DbContextHelper.Create(nameof(getByAuditAndSeverityAsync_shouldFilterCorrectly));
        var owners = new OwnerRepository(ctx);
        var audits = new AuditRepository(ctx);
        var findings = new FindingRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        var audit = Audit.Create(
            title: "Q4",
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow.AddDays(1),
            auditedArea: "Finance",
            ownerId: owner.Id,
            status: AuditStatus.Pending
        );
        await audits.AddAsync(audit);

        await findings.AddAsync(new Finding(audit.Id, SeverityLevel.High, "High 1"));
        await findings.AddAsync(new Finding(audit.Id, SeverityLevel.Low, "Low 1"));
        await findings.AddAsync(new Finding(audit.Id, SeverityLevel.High, "High 2"));

        var result = await findings.GetByAuditAndSeverityAsync(audit.Id, (int)SeverityLevel.High);

        Assert.Equal(2, result.Count);
        Assert.All(result, f => Assert.Equal(SeverityLevel.High, f.Severity));
    }
}