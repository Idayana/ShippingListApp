using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Dtos.Product;
using ShoppingListApi.Interfaces;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseRepository<Product> _repo;
        private readonly IMapper _mapper;
        public ProductsController(IBaseRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCreateDto prod)
        {
             if(!ModelState.IsValid || prod == null)
             {
                return BadRequest(ModelState);
             }

            var result = _mapper.Map<Product>(prod);
            await _repo.AddAsync(result);

            if(await _repo.SaveAll())
            {
                return CreatedAtRoute("GetProduct", new { id = result.Id }, result);
            }
            throw new Exception("Creating the product failed on save");
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _repo.Get(id);
                if(product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _repo.Get();
                if(products == null)
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto prod, int id)
        {
            if (!ModelState.IsValid || prod == null)
            {
                return BadRequest(ModelState);
            }

            var prodFromRepo = await _repo.Get(id);
            if(prodFromRepo == null)
            {
                return NotFound();
            }

            var prodUpdate = _mapper.Map(prod, prodFromRepo);
            await _repo.UpdateAsync(prodUpdate);

            if(await _repo.SaveAll())
            {
                var result = _mapper.Map<ProductUpdateDto>(prodUpdate);
                return Ok(result);
            }
            throw new Exception($"Error updating product {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var prodToRemove = await _repo.Get(id);
            if(prodToRemove == null)
            {
                return NotFound();
            }
            await _repo.RemoveAsync(prodToRemove);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the product");
        }
    }
}