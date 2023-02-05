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
using Newtonsoft.Json.Linq;

namespace iSpan_final_service.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WOBContext _context;

        public ProductsController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProduct()
        {
            return _context.Product.Select(emp => new ProductDTO { ProductId = emp.ProductId, ClassId = emp.ClassId, BrandId = emp.BrandId, Price = emp.Price, Describe = emp.Describe, Image = emp.Image, Stock = emp.Stock, Validity = emp.Validity, Discount = emp.Discount, ProductName = emp.ProductName });

            //return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            // var product = await _context.Product.FindAsync(id);
            var Product = await _context.Product.FindAsync(id);

            ProductDTO emp = new ProductDTO
            {
                ProductId = Product.ProductId,             
                ProductName = Product.ProductName,
                Image = Product.Image,
                Price = Product.Price,
              
            };


            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }
        // Post: api/Products/5
        [HttpPost("List")]
        public async Task<ProductDTO[]> GetProductList(int[] ids )
        {
            

            ProductDTO[] productDTO =new ProductDTO[0];
            for(int i=0;i< ids.Count(); i++)
            {
                var Product = await _context.Product.FindAsync(ids[i]);
                

                ProductDTO emp = new ProductDTO
                {
                    ProductId = Product.ProductId,
                    ProductName = Product.ProductName,
                    Image = Product.Image,
                    Price = Product.Price,

                };
                ;
                System.Array.Resize(ref productDTO, productDTO.Length + 1);
                productDTO[productDTO.Length - 1] = emp;
            }
            return(productDTO);

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutProduct(int id, ProductDTO empDTO)
        {
            if (id != empDTO.ProductId)
            {
                return "Failed";
            }

            Product emp = await _context.Product.FindAsync(empDTO.ProductId);
            emp.ProductId = empDTO.ProductId;
            emp.ClassId = empDTO.ClassId;
            emp.BrandId = empDTO.BrandId;
            emp.ProductName = empDTO.ProductName;
            emp.Image = empDTO.Image;
            emp.Price = empDTO.Price;
            emp.Describe = empDTO.Describe;
            emp.Discount = empDTO.Discount;
            emp.Stock = empDTO.Stock;
            emp.Validity = empDTO.Validity;

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return "No result";
                }

                else
                {
                    throw;
                }
            }
            return "Success";
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Product> PostProduct(ProductDTO Product)
        {

            Product emp = new Product
            {
                ProductId = Product.ProductId,
                ClassId = Product.ClassId,
                BrandId = Product.BrandId,
                ProductName = Product.ProductName,
                Image = Product.Image,
                Price = Product.Price,
                Describe = Product.Describe,
                Discount = Product.Discount,
                Stock = Product.Stock,
                Validity = Product.Validity,
            };

            _context.Product.Add(emp);
            await _context.SaveChangesAsync();

            return emp;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return "Nothing to dalete";
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return "Delete Success";
        }

        [HttpPost("Filter")]
        //Uri:api/Products/Filter
        public async Task<IEnumerable<ProductDTO>> FilterProduct([FromBody] ProductDTO product)
        {
            return _context.Product.Where(emp => emp.ProductName.Contains(product.ProductName)).Select(emp => new ProductDTO
            {
                ProductId = emp.ProductId,
                ClassId = emp.ClassId,
                BrandId = emp.BrandId,
                ProductName = emp.ProductName,
                Image = emp.Image,

                Price = emp.Price,

                Describe = emp.Describe,
                Discount = emp.Discount,
                Stock = emp.Stock,

            });
        }


        [HttpGet("Single")]


        [HttpPost("Single")]

        public async Task<IEnumerable<ProductDTO>> SinglerProduct(int id)
        {
            var aaa = _context.Product.Where(emp => emp.ProductId == id && emp.Validity).ToList().Select(emp => new ProductDTO
            {
                ProductId = emp.ProductId,
                ClassId = emp.ClassId,
                BrandId = emp.BrandId,
                ProductName = emp.ProductName,
                Image = emp.Image,
                Price = emp.Price,
                Describe = emp.Describe,
                Discount = emp.Discount,
                Stock = emp.Stock,

            });
            return aaa;
        }


        private bool ProductExists(int id)

        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
