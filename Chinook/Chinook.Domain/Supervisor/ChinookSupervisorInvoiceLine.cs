using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<InvoiceLine>> GetAllInvoiceLine()
    {
        var invoiceLines = await _invoiceLineRepository.GetAll();
        foreach (var invoiceLine in invoiceLines)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("InvoiceLine-", invoiceLine.Id), invoiceLine, (TimeSpan)cacheEntryOptions);
        }
        return invoiceLines;
    }

    public async Task<InvoiceLine> GetInvoiceLineById(int id)
    {
        var invoiceLineCached = _cache.Get<InvoiceLine>(string.Concat("InvoiceLine-", id));

        if (invoiceLineCached != null)
        {
            return invoiceLineCached;
        }
        else
        {
            var invoiceLine = await _invoiceLineRepository.GetById(id);
            if (invoiceLine == null) return null!;
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("InvoiceLine-", invoiceLine.Id), invoiceLine, (TimeSpan)cacheEntryOptions);
            return invoiceLine;
        }
    }

    public async Task<InvoiceLine> AddInvoiceLine(InvoiceLine newInvoiceLine)
    {
        await _invoiceLineValidator.ValidateAndThrowAsync(newInvoiceLine);
        return await _invoiceLineRepository.Add(newInvoiceLine);
    }

    public async Task<bool> UpdateInvoiceLine(InvoiceLine invoiceLine)
    {
        await _invoiceLineValidator.ValidateAndThrowAsync(invoiceLine);
        return await _invoiceLineRepository.Update(invoiceLine);
    }

    public Task<bool> DeleteInvoiceLine(int id) => _invoiceLineRepository.Delete(id);
    
    public async Task<List<InvoiceLine>> GetInvoiceLineByInvoiceId(int id) => await _invoiceLineRepository.GetByInvoiceId(id);

    public async Task<List<InvoiceLine>> GetInvoiceLineByTrackId(int id) => await _invoiceLineRepository.GetByTrackId(id);
}