using Contact_Manager_Application.Models;
using Contact_Manager_Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Manager_Application.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        public async Task<IActionResult> ContactsView()
        {
            var contacts = await _service.GetAllAsync();
            return View(contacts);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DelteDataAsync(id);
            if (!result)
                return NotFound("Contacts not found");

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

                return BadRequest(string.Join("\n", errors));
            }

            var result = await _service.UpdateDataAsync(contact);
            if (!result)
                return NotFound("Contacts not found");


            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new StreamReader(file.OpenReadStream());
                var contacts = new List<Contact>();

                bool isFirstLine = true;

                while (!stream.EndOfStream)
                {
                    var line = await stream.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    var values = line.Split(',');

                    if (!DateTime.TryParse(values[1], out var dob))
                        return BadRequest($"Invalid date format in line: {line}");

                    if (string.IsNullOrWhiteSpace(values[2]))
                        return BadRequest($"Married field is required in line: {line}");

                    if (!bool.TryParse(values[2], out var married))
                        return BadRequest($"Invalid married value in line: {line}");

                    if (!decimal.TryParse(values[4], out var salary))
                        return BadRequest($"Invalid salary format in line: {line}");

                    contacts.Add(new Contact
                    {
                        Name = values[0],
                        DateOfBirth = dob,
                        Married = married,
                        Phone = values[3],
                        Salary = salary
                    });
                }

                await _service.AddRangeAsync(contacts);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
