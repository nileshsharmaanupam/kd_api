using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class CreditNoteService : ICreditNoteService
{
    private readonly PostgresDbContext _context;

    public CreditNoteService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<CreditNoteDTO> GetCreditNoteById(int creditNoteId)
    {
        var creditNote = await _context.CreditNotes.FindAsync(creditNoteId);
        if (creditNote == null)
        {
            throw new ArgumentException($"CreditNote with ID {creditNoteId} not found.");
        }
        return creditNote;
    }

    public async Task<IEnumerable<CreditNoteDTO>> GetAllCreditNotes()
    {
        return await _context.CreditNotes.ToListAsync();
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
