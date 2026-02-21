using Contact_Manager_Application.Models;

namespace Contact_Manager_Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<string>> ImportDataAsync(IFormFile file);
        Task<List<Contact>> GetAllAsync();
        Task<bool> UpdateDataAsync(Contact contact);
        Task<bool> DelteDataAsync(int id);
    }
}
