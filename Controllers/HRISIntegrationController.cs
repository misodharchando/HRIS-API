
using HRISWebAPI.Api.Models;
using HRISWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRISWebAPI.Api.Controllers;

[ApiController, Route("api/[controller]"), Authorize]
public class HRISIntegrationController(IEmployeeRepository repo) : ControllerBase
{
    [HttpPost("InsertEmployees")]
    public async Task<IActionResult> Insert(List<EmployeeRequest> r, CancellationToken c) =>
        r.Count == 0
            ? BadRequest("At least one employee is required")
            : Ok(await repo.InsertAsync(r, c));

    [HttpPost("ResignEmployees")]
    public async Task<IActionResult> Resign(List<ResignEmployeeRequest> r, CancellationToken c) =>
        r.Count == 0 ? BadRequest() : Ok(await repo.ResignAsync(r, c));

    [HttpPut("UpdateEmployees")]
    public async Task<IActionResult> Update(List<EmployeeRequest> r, CancellationToken c) =>
        r.Count == 0 ? BadRequest() : Ok(await repo.UpdateAsync(r, c));
}
