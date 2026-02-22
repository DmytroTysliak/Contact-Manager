using Contact_Manager_Application.Data;
using Contact_Manager_Application.Models;
using Contact_Manager_Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contact_Manager_Application.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<bool> DelteDataAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact == null)
                return false;

           _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task AddRangeAsync(List<Contact> contacts)
        {
            if (contacts == null || !contacts.Any()) return;

            await _context.Contacts.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDataAsync(Contact contact)
        {
            var exist = await _context.Contacts.FindAsync(contact.Id);
            if(exist == null)
                return false;

            exist.Name = contact.Name;
            exist.DateOfBirth = contact.DateOfBirth;
            exist.Married = contact.Married;
            exist.Phone = contact.Phone;
            exist.Salary = contact.Salary;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
