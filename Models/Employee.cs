using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HRISWebAPI.Api.Models;

[Table("ADMINEmployeeMaster")]
public class Employee
{
    public long Id { get; set; }
    [MaxLength(4)] public string TenantId { get; set; } = null!; [MaxLength(4)] public string CompanyId { get; set; } = null!; [MaxLength(10)] public string SiteId { get; set; } = null!; [MaxLength(10)] public string EmployeeNo { get; set; } = null!; [MaxLength(100)] public string EmpName { get; set; } = null!; [MaxLength(100)] public string? EmpNameSecondLanguage { get; set; }
    [MaxLength(10)] public string? Alias { get; set; }
    [MaxLength(10)] public string? AliasSecondLanguage { get; set; }
    [MaxLength(100)] public string? Email { get; set; }
    [MaxLength(20)] public string? MobileNo { get; set; }
    [MaxLength(30)] public string NFCUId { get; set; } = null!; public int EmpCategoryId { get; set; }
    [MaxLength(20)] public string EmpCategoryName { get; set; } = null!; public int EmpTypeId { get; set; }
    [MaxLength(20)] public string EmpTypeName { get; set; } = null!; public DateTime EmpJoinedDate { get; set; }
    [MaxLength(100)] public string? UserId { get; set; }
    [MaxLength(500)] public string? Image { get; set; }
    [MaxLength(10)] public string? PreviousEmployeeNo { get; set; }
    [MaxLength(10)] public string? BandId { get; set; }
    public bool IsActive { get; set; } = true; public DateTime? ResignDate { get; set; }
    [MaxLength(100)] public string CreatedBy { get; set; } = "SYSTEM"; public DateTime CreatedDate { get; set; }
    [MaxLength(100)] public string ModifiedBy { get; set; } = "SYSTEM"; public DateTime ModifiedDate { get; set; } }
