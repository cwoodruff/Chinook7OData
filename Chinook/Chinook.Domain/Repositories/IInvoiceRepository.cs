using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;

namespace Chinook.Domain.Repositories;

public interface IInvoiceRepository : IRepository<Invoice>, IDisposable
{
    Task<List<Invoice>> GetByCustomerId(int id);
    Task<List<Invoice>> GetByEmployeeId(int id);
}