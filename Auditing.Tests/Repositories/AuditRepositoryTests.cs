using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Infrastructure.Repositories;
using Auditing.Tests.Helpers;

public class AuditRepositoryTests
{
    [Fact]
    public async Task addAsync_getByIdAsync_shouldIncludeOwnerAndFindings()
    {
        using var ctx = DbContextHelper.Create(nameof(addAsync_getByIdAsync_shouldIncludeOwnerAndFindings));
        var owners = new OwnerRepository(ctx);
        var audits = new AuditRepository(ctx);
        var findings = new FindingRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        var audit = Audit.Create(
            title: "Audit Q4",
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow.AddDays(1),
            auditedArea: "Finance",
            ownerId: owner.Id,
            status: AuditStatus.Pending
        );
        await audits.AddAsync(audit);

        var finding = new Finding(audit.Id, SeverityLevel.High, "Missing control");
        await findings.AddAsync(finding);

        var result = await audits.GetByIdAsync(audit.Id);

        Assert.NotNull(result);
        Assert.Equal("Audit Q4", result!.Title);
        Assert.Equal(owner.Id, result.Owner!.Id);
        Assert.Single(result.Findings);
    }

    [Fact]
    public async Task updateAsync_shouldModifyExistingAudit()
    {
        using var ctx = DbContextHelper.Create(nameof(updateAsync_shouldModifyExistingAudit));
        var owners = new OwnerRepository(ctx);
        var audits = new AuditRepository(ctx);

        var owner = Owner.Create("Acme", "acme@mail.com", "Finance");
        await owners.AddAsync(owner);

        var audit = Audit.Create(
            title: "Initial Audit",
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow.AddDays(1),
            auditedArea: "Finance",
            ownerId: owner.Id,
            status: AuditStatus.Pending
        );
        await audits.AddAsync(audit);

        audit.ChangeTitle("Updated Audit");
        audit.ChangeStatus(AuditStatus.InProgress);
        await audits.UpdateAsync(audit);

        var result = await audits.GetByIdAsync(audit.Id);

        Assert.NotNull(result);
        Assert.Equal("Updated Audit", result!.Title);
        Assert.Equal(AuditStatus.InProgress, result.Status);
    }
}