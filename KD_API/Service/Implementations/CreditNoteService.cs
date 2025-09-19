using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.CreditNote;
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

    public async Task<CreditNoteResponse> GetCreditNoteById(GetCreditNoteByIdRequest request)
    {
        var creditNote = await _context.CreditNotes.FindAsync(request.CreditNoteId);
        if (creditNote == null)
        {
            throw new ArgumentException($"CreditNote with ID {request.CreditNoteId} not found.");
        }
        
        var customer = await _context.Customers.FindAsync(creditNote.CustomerId);
        var response = _mapper.Map<CreditNoteResponse>(creditNote);
        response.CustomerName = customer?.Name ?? "Unknown Customer";
        response.IsOverdue = creditNote.DueDate < DateTime.Now;
        response.DaysUntilDue = (creditNote.DueDate - DateTime.Now).Days;
        
        return response;
    }

    public async Task<CreditNoteListResponse> GetAllCreditNotes(GetAllCreditNotesRequest request)
    {
        var creditNotes = await _context.CreditNotes.ToListAsync();
        return _mapper.Map<CreditNoteListResponse>(creditNotes);
    }

    public async Task<CreditNoteResponse> CreateCreditNote(CreateCreditNoteRequest request)
    {
        var creditNote = _mapper.Map<CreditNoteDTO>(request);
        _context.CreditNotes.Add(creditNote);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CreditNoteResponse>(creditNote);
        return response;
    }

    public async Task<CreditNoteResponse> UpdateCreditNote(UpdateCreditNoteRequest request)
    {
        var existingCreditNote = await _context.CreditNotes.FindAsync(request.CreditNoteId);
        if (existingCreditNote == null)
        {
            throw new ArgumentException($"CreditNote with ID {request.CreditNoteId} not found.");
        }
        
        _mapper.Map(request, existingCreditNote);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CreditNoteResponse>(existingCreditNote);
        return response;
    }

    public async Task<DeleteCreditNoteResponse> DeleteCreditNote(DeleteCreditNoteRequest request)
    {
        try
        {
            var creditNote = await _context.CreditNotes.FindAsync(request.CreditNoteId);
            if (creditNote == null)
            {
                return new DeleteCreditNoteResponse 
                { 
                    Success = false, 
                    Message = $"CreditNote with ID {request.CreditNoteId} not found." 
                };
            }

            _context.CreditNotes.Remove(creditNote);
            await _context.SaveChangesAsync();
            
            return new DeleteCreditNoteResponse 
            { 
                Success = true, 
                Message = "CreditNote deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteCreditNoteResponse 
            { 
                Success = false, 
                Message = $"Error deleting CreditNote: {ex.Message}" 
            };
        }
    }
}
