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
    public class OrderDetailsController : ControllerBase
    {
        private readonly WOBContext _context;

        public OrderDetailsController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<IEnumerable<OrderDetailDTO>> GetOrderDetail()
        {
            return  _context.OrderDetail.Select(ordX => new OrderDetailDTO { OrderId = ordX.OrderId, ProductId = ordX.ProductId,Amount =ordX.Amount });
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetail(int id)
        {
            var OrderDetail = await _context.OrderDetail.FindAsync(id);

            OrderDetailDTO ordX=new OrderDetailDTO
            {
                OrderId =OrderDetail.OrderId,
               ProductId=OrderDetail.ProductId,
               Amount=OrderDetail.Amount,
               
            };




            if (ordX == null)
            {
                return NotFound();
            }

            return ordX;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutOrderDetail(int id, OrderDetailDTO ordXDTO)
        {
            if (id != ordXDTO.OrderId)
            {
                return "FAILED";
            }

            OrderDetail ordX =await _context.OrderDetail.FindAsync(ordXDTO.OrderId);
            ordX.OrderId = ordXDTO.OrderId;
            ordX.ProductId = ordXDTO.ProductId;
            ordX.Amount = ordXDTO.Amount;
            

            _context.Entry(ordXDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return "NotFound";
                }
                else
                {
                    throw;
                }
            }

            return "SUCCESS";
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<OrderDetail> PostOrderDetail(OrderDetailDTO orderDetail)
        {
            OrderDetail ordX = new OrderDetail
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Amount = orderDetail.Amount,
                
            };

            _context.OrderDetail.Add(ordX);
            await _context.SaveChangesAsync();

                    return ordX;
        }    
           

        

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return "Nothing to delete";
            }

            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return "成功刪除";
        }

        [HttpPost("Filter")]
        //Uri:api/OrderDetails/Filter
        public async Task<IEnumerable<OrderDetailDTO>> FilterOrderDetail([FromBody] OrderDetailDTO order)
        {
            return _context.OrderDetail.Where(ordX => ordX.OrderId==order.OrderId).Select(ordX => new OrderDetailDTO
            {
                OrderId = ordX.OrderId,
                ProductId = ordX.ProductId,
                Amount = ordX.Amount,                
            });
        }

        //[HttpPost("AddCar")]
        ////Uri:api/OrderDetails/AddCar
        //public async Task<IEnumerable<OrderDetailDTO>> AddCar([FromBody] OrderDetailDTO order)
        //{
        //    //取得目前通過驗證的使用者名稱
        //    //string userId = User.Identity.Name;

        //    //取得該使用者目前購物車內是否已有此商品，且尚未形成訂單的資料
        //    return _context.OrderDetail.Where(ordX => ordX.ProductId == order.ProductId).Select(ordX => new OrderDetailDTO);
        //    if (AddCar == null)
        //    {
        //        //如果篩選條件資料為null，代表要新增全新一筆訂單明細資料
        //        //將產品資料欄位一一對照至訂單明細的欄位
        //        var product = Product.Where(ordX => ordX.ProductId == order.ProductId).FirstOrDefault();
        //        var orderDetail = new table_OrderDetail();
        //        orderDetail.UserId = userId;
        //        orderDetail.ProductId = product.ProductId;
        //        orderDetail.Name = product.Name;
        //        orderDetail.Price = product.Price;
        //        orderDetail.Quantity = 1;
        //        orderDetail.IsApproved = "否";
        //        db.table_OrderDetail.Add(orderDetail);
        //    }
        //    else
        //    {
        //        //如果購物車已有此商品，僅需將數量加1
        //        currentCar.Quantity++;
        //    }

        //    //儲存資料庫並導至購物車檢視頁面
        //    db.SaveChanges();
        //    return RedirectToAction("ShoppingCar");
        //}


        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderId == id);
        }
    }
}
