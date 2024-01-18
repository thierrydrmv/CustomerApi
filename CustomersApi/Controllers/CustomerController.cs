using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomersApi.Models;
using CustomersApi.Models.Dto;

namespace CustomersApi.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        private static CustomerDTO CustomerToDTO(Customer customer) 
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
            };
        }

        // GET: api/CustomerItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomerItems()
        {
            return await _context.CustomerItems.Select(x => CustomerToDTO(x)).ToListAsync();
        }


        // GET: api/CustomerItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);

            if (customerItem == null)
            {
                return NotFound();
            }

            return CustomerToDTO(customerItem);
        }

        // PUT: api/CustomerItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerItem(long id, Customer customerItem)
        {
            if (id != customerItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomerItem(Customer customerItem)
        {
            _context.CustomerItems.Add(customerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerItem), new { id = customerItem.Id }, CustomerToDTO(customerItem));
        }

        // DELETE: api/CustomerItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(customerItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerItemExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
