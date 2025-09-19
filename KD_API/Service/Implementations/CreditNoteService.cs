using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIResponse.CreditNote;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class CreditNoteService : ICreditNoteService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public CreditNoteService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreditNoteResponse> GetCreditNoteById(int creditNoteId)
    {
        var creditNote = await _context.CreditNotes.FindAsync(creditNoteId);
        if (creditNote == null)
        {
            throw new ArgumentException($"CreditNote with ID {creditNoteId} not found.");
        }
        
        var customer = await _context.Customers.FindAsync(creditNote.CustomerId);
        var response = _mapper.Map<CreditNoteResponse>(creditNote);
        response.CustomerName = customer?.Name ?? "Unknown Customer";
        response.IsOverdue = creditNote.DueDate < DateTime.Now;
        response.DaysUntilDue = (creditNote.DueDate - DateTime.Now).Days;
        
        return response;
    }

    public async Task<CreditNoteListResponse> GetAllCreditNotes()
    {
        var creditNotes = await _context.CreditNotes.ToListAsync();
        return _mapper.Map<CreditNoteListResponse>(creditNotes);
    }

    public async Task<bool> CreateCreditNote(CreditNoteDTO creditNoteDto)
    {
        try
        {
            _context.CreditNotes.Add(creditNoteDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CreditNoteDTO> UpdateCreditNote(int creditNoteId, CreditNoteDTO creditNoteDto)
    {
        var existingCreditNote = await _context.CreditNotes.FindAsync(creditNoteId);
        if (existingCreditNote == null)
        {
            throw new ArgumentException($"CreditNote with ID {creditNoteId} not found.");
        }

        existingCreditNote.CustomerId = creditNoteDto.CustomerId;
        existingCreditNote.CreditDate = creditNoteDto.CreditDate;
        existingCreditNote.DueDate = creditNoteDto.DueDate;
        existingCreditNote.SubTotal = creditNoteDto.SubTotal;
        existingCreditNote.TaxAmount = creditNoteDto.TaxAmount;
        existingCreditNote.TotalAmount = creditNoteDto.TotalAmount;
        existingCreditNote.Status = creditNoteDto.Status;
        existingCreditNote.Notes = creditNoteDto.Notes;

        await _context.SaveChangesAsync();
        return existingCreditNote;
    }

    public async Task<bool> DeleteCreditNote(int creditNoteId)
    {
        try
        {
            var creditNote = await _context.CreditNotes.FindAsync(creditNoteId);
            if (creditNote == null)
            {
                return false;
            }

            _context.CreditNotes.Remove(creditNote);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
