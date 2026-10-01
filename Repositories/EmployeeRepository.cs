using HRISWebAPI.Api.Data;
using HRISWebAPI.Api.Models;
using HRISWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HRISWebAPI.Api.Repositories;


public class EmployeeRepository(AppDbContext db) : IEmployeeRepository
{
    public async Task<BulkResponse> InsertAsync(List<EmployeeRequest> rs, CancellationToken ct) =>
        await Process(
            rs,
            async r =>
            {
                var e = await Find(r.TenantId, r.CompanyId, r.SiteId, r.EmployeeNo, ct);
                if (e == null)
                {
                    e = new Employee
                    {
                        TenantId = r.TenantId,
                        CompanyId = r.CompanyId,
                        SiteId = r.SiteId,
                        EmployeeNo = r.EmployeeNo,
                        CreatedBy = r.ModifiedBy ?? "HRIS",
                        CreatedDate = DateTime.UtcNow,
                    };
                    db.Add(e);
                }
                Map(r, e);
                e.IsActive = true;
                e.ResignDate = null;
                e.ModifiedBy = r.ModifiedBy ?? "HRIS";
                e.ModifiedDate = DateTime.UtcNow;
                await db.SaveChangesAsync(ct);
                return "Inserted/refreshed successfully";
            }
        );

    public async Task<BulkResponse> UpdateAsync(List<EmployeeRequest> rs, CancellationToken ct)
    {
        var seen = new HashSet<string>();
        return await Process(
            rs,
            async r =>
            {
                var k = Key(r.TenantId, r.CompanyId, r.SiteId, r.EmployeeNo);
                if (!seen.Add(k))
                    throw new InvalidOperationException(
                        "Duplicate employee in request; only first processed"
                    );
                var e =
                    await Find(r.TenantId, r.CompanyId, r.SiteId, r.EmployeeNo, ct)
                    ?? throw new KeyNotFoundException("Employee not found");
                if (!e.IsActive)
                    throw new InvalidOperationException("Employee is inactive");
                Map(r, e);
                e.ModifiedBy = r.ModifiedBy ?? "HRIS";
                e.ModifiedDate = DateTime.UtcNow;
                await db.SaveChangesAsync(ct);
                return "Updated successfully";
            }
        );
    }

    public async Task<BulkResponse> ResignAsync(
        List<ResignEmployeeRequest> rs,
        CancellationToken ct
    )
    {
        var seen = new HashSet<string>();
        return await Process(
            rs,
            async r =>
            {
                var k = Key(r.TenantId, r.CompanyId, r.SiteId, r.EmployeeNo);
                if (!seen.Add(k))
                    throw new InvalidOperationException(
                        "Duplicate employee in request; only first processed"
                    );
                var e =
                    await Find(r.TenantId, r.CompanyId, r.SiteId, r.EmployeeNo, ct)
                    ?? throw new KeyNotFoundException(
                        "Employee not found - resignation cannot be processed"
                    );
                e.ResignDate = r.ResignDate.Date;
                e.IsActive = false;
                e.ModifiedBy = r.ModifiedBy ?? "HRIS";
                e.ModifiedDate = DateTime.UtcNow;
                await db.SaveChangesAsync(ct);
                return "Resignation processed successfully";
            }
        );
    }

    async Task<BulkResponse> Process<T>(List<T> rs, Func<T, Task<string>> action)
    {
        var o = new BulkResponse { Total = rs.Count };
        foreach (var r in rs)
        {
            var no = (string)(typeof(T).GetProperty("EmployeeNo")!.GetValue(r) ?? "");
            try
            {
                o.Results.Add(new(no, true, await action(r)));
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                o.Results.Add(new(no, false, ex.GetBaseException().Message));
            }
        }
        o.Succeeded = o.Results.Count(x => x.Success);
        o.Failed = o.Total - o.Succeeded;
        return o;
    }

    Task<Employee?> Find(string t, string c, string s, string n, CancellationToken ct) =>
        db.Employees.FirstOrDefaultAsync(
            x => x.TenantId == t && x.CompanyId == c && x.SiteId == s && x.EmployeeNo == n,
            ct
        );

    static string Key(params string[] v) => string.Join('|', v);

    static void Map(EmployeeRequest r, Employee e)
    {
        e.EmpName = r.EmpName;
        e.EmpNameSecondLanguage = r.EmpNameSecondLanguage;
        e.Alias = r.Alias;
        e.AliasSecondLanguage = r.AliasSecondLanguage;
        e.Email = r.Email;
        e.MobileNo = r.MobileNo;
        e.NFCUId = r.NFCUId;
        e.EmpCategoryId = r.EmpCategoryId;
        e.EmpCategoryName = r.EmpCategoryName;
        e.EmpTypeId = r.EmpTypeId;
        e.EmpTypeName = r.EmpTypeName;
        e.EmpJoinedDate = r.EmpJoinedDate.Date;
        e.UserId = r.UserId;
        e.Image = r.Image;
        e.PreviousEmployeeNo = r.PreviousEmployeeNo;
        e.BandId = r.BandId;
    }
}
