using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Employee>> GetAllEmployee()
    {
        var employees = await _employeeRepository.GetAll();
        foreach (var employee in employees)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Employee-", employee.Id), employee, (TimeSpan)cacheEntryOptions);
        }
        return employees;
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        var employeeCached = _cache.Get<Employee>(string.Concat("Employee-", id));

        if (employeeCached != null)
        {
            return employeeCached;
        }
        else
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee == null) return null;

            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("Employee-", employee.Id), employee,
                (TimeSpan)cacheEntryOptions);
            return employee;
        }
    }

    public async Task<Employee?> GetEmployeeReportsTo(int id)
    {
        return await _employeeRepository.GetReportsTo(id);
    }

    public async Task<Employee> AddEmployee(Employee newEmployee)
    {
        await _employeeValidator.ValidateAndThrowAsync(newEmployee);
        return await _employeeRepository.Add(newEmployee);
    }

    public async Task<bool> UpdateEmployee(Employee employee)
    {
        await _employeeValidator.ValidateAndThrowAsync(employee);
        return await _employeeRepository.Update(employee);
    }

    public Task<bool> DeleteEmployee(int id) => _employeeRepository.Delete(id);

    public async Task<IEnumerable<Employee>> GetEmployeeDirectReports(int id) => await _employeeRepository.GetDirectReports(id);

    public async Task<IEnumerable<Employee>> GetDirectReports(int id) => await _employeeRepository.GetDirectReports(id);
}