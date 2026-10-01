using System.ComponentModel.DataAnnotations;

namespace HRISWebAPI.Api.Models;

public class EmployeeRequest
{
    [Required, StringLength(4)]
    public string TenantId { get; set; } = null!;

    [Required, StringLength(4)]
    public string CompanyId { get; set; } = null!;

    [Required, StringLength(10)]
    public string SiteId { get; set; } = null!;

    [Required, StringLength(10)]
    public string EmployeeNo { get; set; } = null!;

    [Required, StringLength(100)]
    public string EmpName { get; set; } = null!;
    public string? EmpNameSecondLanguage { get; set; }
    public string? Alias { get; set; }
    public string? AliasSecondLanguage { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
    public string? MobileNo { get; set; }

    [Required]
    public string NFCUId { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int EmpCategoryId { get; set; }

    [Required]
    public string EmpCategoryName { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int EmpTypeId { get; set; }

    [Required]
    public string EmpTypeName { get; set; } = null!;

    [Required]
    public DateTime EmpJoinedDate { get; set; }
    public string? UserId { get; set; }
    public string? Image { get; set; }
    public string? PreviousEmployeeNo { get; set; }
    public string? BandId { get; set; }
    public string? ModifiedBy { get; set; }
}

public class ResignEmployeeRequest
{
    [Required]
    public string TenantId { get; set; } = null!;

    [Required]
    public string CompanyId { get; set; } = null!;

    [Required]
    public string SiteId { get; set; } = null!;

    [Required]
    public string EmployeeNo { get; set; } = null!;

    [Required]
    public DateTime ResignDate { get; set; }
    public string? ModifiedBy { get; set; }
}

public record RecordResult(string EmployeeNo, bool Success, string Message);

public class BulkResponse
{
    public int Total { get; set; }
    public int Succeeded { get; set; }
    public int Failed { get; set; }
    public List<RecordResult> Results { get; set; } = [];
}
