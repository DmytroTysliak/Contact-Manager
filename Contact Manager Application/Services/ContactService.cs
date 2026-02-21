using Contact_Manager_Application.Models;
using Contact_Manager_Application.Services.Interfaces;

namespace Contact_Manager_Application.Services
{
    public class ContactService : IContactService
    {
        private static List<Contact> _contacts = new();

        public Task<List<Contact>> GetAllAsync()
        {
            return Task.FromResult(_contacts);
        }

        public Task<bool> DelteDataAsync(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if(contact == null)
                return Task.FromResult(false);

            _contacts.Remove(contact);
            return Task.FromResult(true);
        }

        public Task<List<string>> ImportDataAsync(IFormFile file)
        {
            return Task.FromResult(new List<string> { "CSV login coming next" });
        }

        public Task<bool> UpdateDataAsync(Contact contact)
        {
            var exist = _contacts.FirstOrDefault(x => x.Id == contact.Id);
            if(exist == null)
                return Task.FromResult(false);

            exist.Name = contact.Name;
            exist.DateOfBirth = contact.DateOfBirth;
            exist.Married = contact.Married;
            exist.Phone = contact.Phone;
            exist.Salary = contact.Salary;

            return Task.FromResult(true);
        }
    }
}
