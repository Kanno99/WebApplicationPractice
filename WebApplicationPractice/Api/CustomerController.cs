using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationPractice.Data;
using WebApplicationPractice.Data.Entities;
using WebApplicationPractice.Models;

namespace WebApplicationPractice.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<List<CustomerModel>> GetRecords()
        {
            var records = await _context.Customer.ToListAsync();

            var viewModel = new List<CustomerModel>();
            foreach (var record in records)
            {
                viewModel.Add(new CustomerModel
                {
                    Id = record.Id,
                    Email = record.Email,
                    Name = record.Name,
                    PhoneNumber = record.PhoneNumber,
                    DateOfBirth = record.DateOfBirth
                });
            }

            return viewModel;
        }

        // GET: api/Customer/id
        [HttpGet("{*id}")]
        public async Task<ActionResult<CustomerModel>> GetRecord(long id)
        {
            var record = await _context.Customer.FirstOrDefaultAsync(x => x.Id == id);
            if (record == null) return NotFound();

            return new CustomerModel
            {
                Id = record.Id,
                Email = record.Email,
                Name = record.Name,
                PhoneNumber = record.PhoneNumber,
                DateOfBirth = record.DateOfBirth
            };
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<object>> CreateRecord(CustomerModel input)
        {
            var newRecord = new Customer
            {
                Name = input.Name,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = input.DateOfBirth,
                CreatedTimestamp = DateTime.Now,
                LastModifiedTimestamp = DateTime.Now
            };

            _context.Customer.Add(newRecord);
            await _context.SaveChangesAsync();

            return newRecord;
        }

        // DELETE: api/Customer/id
        [HttpDelete("{*id}")]
        public async Task<IActionResult> DeleteRecord(long id)
        {
            var record = await _context.Customer.FirstOrDefaultAsync(x => x.Id == id);
            if (record == null) return NotFound();

            _context.Customer.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
