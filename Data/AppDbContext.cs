
using HRISWebAPI.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HRISWebAPI.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Employee>()
            .HasIndex(x => new
            {
                x.TenantId,
                x.CompanyId,
                x.SiteId,
                x.EmployeeNo,
            })
            .IsUnique();
    }
}
