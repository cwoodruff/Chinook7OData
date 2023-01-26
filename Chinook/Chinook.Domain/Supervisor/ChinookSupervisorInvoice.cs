using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Invoice>> GetAllInvoice()
    {
        var invoices = await _invoiceRepository.GetAll();
        foreach (var invoice in invoices)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Invoice-", invoice.Id), invoice, (TimeSpan)cacheEntryOptions);
        }
        return invoices;
    }

    public async Task<Invoice?> GetInvoiceById(int id)
    {
        var invoiceCached = _cache.Get<Invoice>(string.Concat("Invoice-", id));

        if (invoiceCached != null)
        {
            return invoiceCached;
        }
        else
        {
            var invoice = await _invoiceRepository.GetById(id);
            if (invoice == null) return null;

            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("Invoice-", invoice.Id), invoice, (TimeSpan)cacheEntryOptions);

            return invoice;
        }
    }

    public async Task<List<Invoice>> GetInvoiceByCustomerId(int id) => await _invoiceRepository.GetByCustomerId(id);

    public async Task<Invoice> AddInvoice(Invoice newInvoice)
    {
        await _invoiceValidator.ValidateAndThrowAsync(newInvoice);
        return await _invoiceRepository.Add(newInvoice);
    }

    public async Task<bool> UpdateInvoice(Invoice invoice)
    {
        await _invoiceValidator.ValidateAndThrowAsync(invoice);
        return await _invoiceRepository.Update(invoice);
    }

    public Task<bool> DeleteInvoice(int id) => _invoiceRepository.Delete(id);


    public async Task<List<Invoice>> GetInvoiceByEmployeeId(int id) => await _invoiceRepository.GetByEmployeeId(id);
}