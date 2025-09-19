using KD_API.Models;
using KD_API.Models.APIResponse.CreditNote;

namespace KD_API.Service.Interfaces;

public interface ICreditNoteService
{
    public Task<CreditNoteResponse> GetCreditNoteById(int creditNoteId);
    public Task<CreditNoteListResponse> GetAllCreditNotes();
    public Task<bool> CreateCreditNote(CreditNoteDTO creditNoteDto);
    public Task<CreditNoteDTO> UpdateCreditNote(int creditNoteId, CreditNoteDTO creditNoteDto);
    public Task<bool> DeleteCreditNote(int creditNoteId);
}
