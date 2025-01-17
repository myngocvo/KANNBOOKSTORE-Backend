﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCHS.DTO;
using QLCHS.Entities;

namespace QLCHS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly QLBANSACHContext _context;

        public CartsController(QLBANSACHContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }
        [HttpGet("{idCustomer}/Customer")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartOrders(string idCustomer)
        {
            var cart = await _context.Carts
                .Where(o => o.CustomerId == idCustomer)
                .ToListAsync();

            if (cart == null || cart.Count == 0)
            {
                return NotFound($"No orders found for customer with ID {idCustomer}.");
            }

            return cart;
        }
        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(string id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartDTO>> PostCart(CartDTO cartDTO)
        {
            if (_context == null || _context.Carts == null)
            {
                return Problem("Entity set 'QLCHSContext.Carts' is null or context is not properly initialized.");
            }

            // Map CartDTO to Cart entity
            var cart = new Cart
            {
                Id = cartDTO.Id,
                BookId = cartDTO.BookId,
                CustomerId = cartDTO.CustomerId
                // Map other properties as needed
            };

            _context.Carts.Add(cart);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartExists(cart.Id)) // Adjust if CartExists checks other fields
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCart", new { id = cart.Id }, cartDTO);
        }


        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(string id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
