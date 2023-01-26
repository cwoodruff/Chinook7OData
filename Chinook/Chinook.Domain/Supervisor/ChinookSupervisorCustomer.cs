using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Customer>> GetAllCustomer()
    {
        var customers = await _customerRepository.GetAll();

        foreach (var customer in customers)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Customer-", customer.Id), customer, (TimeSpan)cacheEntryOptions);
        }
        return customers;
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        var customerCached = _cache.Get<Customer>(string.Concat("Customer-", id));

        if (customerCached != null)
        {
            return customerCached;
        }
        else
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null) return null!;
            customer.SupportRep =
                await GetEmployeeById(customer.SupportRepId);
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("Customer-", customer.Id), customer, (TimeSpan)cacheEntryOptions);
            return customer;
        }
    }

    public async Task<List<Customer>> GetCustomerBySupportRepId(int id)
    {
        var customers = await _customerRepository.GetBySupportRepId(id);

        foreach (var customer in customers)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Customer-", customer.Id), customer, (TimeSpan)cacheEntryOptions);
        }
        return customers;
    }

    public async Task<Customer> AddCustomer(Customer newCustomer)
    {
        await _customerValidator.ValidateAndThrowAsync(newCustomer);
        return await _customerRepository.Add(newCustomer);;
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        await _customerValidator.ValidateAndThrowAsync(customer);
        return await _customerRepository.Update(customer);
    }

    public Task<bool> DeleteCustomer(int id) => _customerRepository.Delete(id);
}