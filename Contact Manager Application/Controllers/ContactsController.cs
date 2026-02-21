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

        public async Task<IActionResult> Index()
        {
            var contacts = await _service.GetAllAsync();
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DelteDataAsync(id);
            if (!result) 
                return NotFound();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Contact contact)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateDataAsync(contact);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
