using HRISWebAPI.Api.Models;

namespace HRISWebAPI.Repositories
{
  
        public interface IEmployeeRepository
        {
            Task<BulkResponse> InsertAsync(List<EmployeeRequest> r, CancellationToken c);
            Task<BulkResponse> ResignAsync(List<ResignEmployeeRequest> r, CancellationToken c);
            Task<BulkResponse> UpdateAsync(List<EmployeeRequest> r, CancellationToken c);
        }
    
}
