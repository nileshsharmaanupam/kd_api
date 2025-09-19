using KD_API.Models;
using KD_API.Models.APIRequests.CreditNote;
using KD_API.Models.APIResponse.CreditNote;

namespace KD_API.Service.Interfaces;

public interface ICreditNoteService
{
    public Task<CreditNoteResponse> GetCreditNoteById(GetCreditNoteByIdRequest request);
    public Task<CreditNoteListResponse> GetAllCreditNotes(GetAllCreditNotesRequest request);
    public Task<CreditNoteResponse> CreateCreditNote(CreateCreditNoteRequest request);
    public Task<CreditNoteResponse> UpdateCreditNote(UpdateCreditNoteRequest request);
    public Task<DeleteCreditNoteResponse> DeleteCreditNote(DeleteCreditNoteRequest request);
}
