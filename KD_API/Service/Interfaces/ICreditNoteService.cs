using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface ICreditNoteService
{
    public Task<CreditNoteDTO> GetCreditNoteById(int creditNoteId);
    public Task<IEnumerable<CreditNoteDTO>> GetAllCreditNotes();
    public Task<bool> CreateCreditNote(CreditNoteDTO creditNoteDto);
    public Task<CreditNoteDTO> UpdateCreditNote(int creditNoteId, CreditNoteDTO creditNoteDto);
    public Task<bool> DeleteCreditNote(int creditNoteId);
}
