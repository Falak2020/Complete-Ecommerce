using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.ProductModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Api.Models.ProductModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlContext _context;

        public object ViewData { get; private set; }

        public ProductController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductsModel>>> GetProducts()
        {
            var products = new List<GetProductsModel>();

            foreach (var product in await _context.Products.Include(x => x.SubCategory).ThenInclude(x=>x.Category).ToListAsync())
                products.Add(new GetProductsModel
                { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageURL = product.ImageURL,
                    InStock = product.InStock,
                    SubCategory = new SubCategoryEntity
                    {
                        Id = product.SubCategory.Id,
                        Name = product.SubCategory.Name,
                        Category = new CategoryEntity
                        {
                            Id = product.SubCategory.Category.Id,
                            Name = product.SubCategory.Category.Name
                        }
                    }
                });

            return products;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductsModel>> GetProductEntity(int id)
        {
            var product = await _context.Products.Include(x => x.SubCategory).ThenInclude(x=>x.Category).Where(x => x.Id == id).FirstOrDefaultAsync();


            if (product == null)
            {
                return NotFound();
            }

            return new GetProductsModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                InStock = product.InStock,
                SubCategory = new SubCategoryEntity
                {
                    Id = product.SubCategory.Id,
                    Name = product.SubCategory.Name,
                    Category = new CategoryEntity
                    {
                        Id = product.SubCategory.Category.Id,
                        Name = product.SubCategory.Category.Name
                    }
                }
            };
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, EditProduct model)
        {
          var  _product = await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);

            if (_product != null)
            {
                _product.Price = model.Price;
                _product.InStock = model.InStock;
                _context.Entry(_product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductEntityExists(id))
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
            else return NotFound();
           
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> PostProductEntity(CreateProductModel model)
        {


            var subcategory = await _context.SubCategories.Where(x => x.Id == model.SubCategoryId).FirstOrDefaultAsync();

            if (subcategory != null)
            {
                var product = new ProductEntity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImageURL = model.ImageURL,
                    InStock = model.InStock,
                    SubCategoryId = model.SubCategoryId
                };


                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProductEntity", new { id = product.Id }, product);

            }
            else
                return new BadRequestResult();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
