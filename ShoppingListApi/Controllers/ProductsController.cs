using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Dtos.Product;
using ShoppingListApi.Helpers;
using ShoppingListApi.Interfaces;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _catRepo;
        public ProductsController(IProductRepository repo, IMapper mapper, ICategoryRepository catRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _catRepo = catRepo;

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCreateDto prod)
        {
             if(!ModelState.IsValid || prod == null)
             {
                return BadRequest(ModelState);
             }

            var prodMapped = _mapper.Map<Product>(prod);

            var cantName = _repo.FindByName(prodMapped.ProductName);
            if(cantName > 0)
            {
                return Conflict();
            }

            if(prodMapped.CategoryId != null)
            {
                var cat = await _catRepo.Get((int)prodMapped.CategoryId);
                if(cat == null) throw new Exception($"Category `{prodMapped.CategoryId}`not found");
                
                prodMapped.Category = cat;
            }

            await _repo.AddAsync(prodMapped);

            if(await _repo.SaveAll())
            {
                var prodToReturn = _mapper.Map<ProductListDto>(prodMapped);
                return CreatedAtRoute("GetProduct", new { id = prodToReturn.ProductId }, prodToReturn);
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
                return Ok(_mapper.Map<ProductListDto>(product));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

       [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams prodParams)
        {
            try
            {
                if (string.IsNullOrEmpty(prodParams.ProductName))
                {
                    prodParams.ProductName = null;
                }

                if (prodParams.CategoryId == 0)
                {
                    prodParams.CategoryId = 0;
                }

                var products = await _repo.ProductByFilters(prodParams);
                if (products == null)
                {
                    return NotFound();
                }
                var prodToReturn = _mapper.Map<IEnumerable<ProductListDto>>(products);
                Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);
                return Ok(prodToReturn);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
            /*try
            {
                var query = _repo.GetQueryable();
                var products = await _repo.Query(_repo.Include(query, "Category"), pagParams); 
                if(products == null)
                {
                    return NotFound();
                }

                var prodToReturn = _mapper.Map<IEnumerable<ProductListDto>>(products);
                Response.AddPagination(products.CurrentPage,products.PageSize,products.TotalCount, products.TotalPages);
                return Ok(prodToReturn);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }*/
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto prodUpDto, int id)
        {
            if (!ModelState.IsValid || prodUpDto == null)
            {
                return BadRequest(ModelState);
            }

            var prodFromRepo = await _repo.Get(id);
            if(prodFromRepo == null)
            {
                return NotFound();
            }

            var prodMapped = _mapper.Map(prodUpDto, prodFromRepo);

            var cantName = _repo.FindByName(prodMapped.ProductName);
            if (cantName > 0)
            {
                return Conflict();
            }

            if (prodMapped.CategoryId != null)
            {
                var cat = await _catRepo.Get((int)prodMapped.CategoryId);
                if (cat == null) throw new Exception($"Category `{prodMapped.CategoryId}`not found");

                prodMapped.Category = cat;
            }

            await _repo.UpdateAsync(prodMapped);

            if(await _repo.SaveAll())
            {
                var result = _mapper.Map<ProductListDto>(prodMapped);
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