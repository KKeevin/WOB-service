using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using Microsoft.AspNetCore.Cors;

namespace iSpan_final_service.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WOBContext _context;

        public OrdersController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetOrder()
        {
            return _context.Order.Select(ord => new OrderDTO { OrderId = ord.OrderId, MemberId = ord.MemberId, Address = ord.Address });

            // return await _context.Order.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            //var order = await _context.Order.FindAsync(id);
            var Order = await _context.Order.FindAsync(id);

            OrderDTO ord = new OrderDTO
            {
                OrderId = Order.OrderId,
                MemberId = Order.MemberId,
                Address = Order.Address
                

            };

            if (ord == null)
            {
                return NotFound();
            }

            return ord;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutOrder(int id, OrderDTO ordDTO)
        {
            if (id != ordDTO.OrderId)
            {
                return"failed";
            }

            Order ord=await _context.Order.FindAsync
                (ordDTO.OrderId);
            ord.OrderId=ordDTO.OrderId;
            ord.MemberId = ordDTO.MemberId;
            ord.Address = ordDTO.Address;

            _context.Entry(ord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return "NotFound";
                }
                else
                {
                    throw;
                }
            }

            return "Success";
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Order> PostOrder(OrderDTO order)
        {
            Order ord = new Order
            {
                OrderId= order.OrderId,
                MemberId= order.MemberId,
                Address= order.Address
            };

            _context.Order.Add(ord);
            await _context.SaveChangesAsync();

            return ord;
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return "Nothing to delete";
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return"成功刪除";
        }

        [HttpPost("Filter")]
        //Uri:api/Orders/Filter
        public async Task<IEnumerable<OrderDTO>> FilterOrder([FromBody] OrderDTO order)
        {
            return _context.Order.Where(ord => ord.Address.Contains(order.Address)).Select(ord => new OrderDTO
            {
              OrderId = ord.OrderId,
              Address = ord.Address,
              MemberId = ord.MemberId,
            
            });
        }



        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
