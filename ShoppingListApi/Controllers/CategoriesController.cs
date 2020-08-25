using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Dtos.Category;
using ShoppingListApi.Helpers;
using ShoppingListApi.Interfaces;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBaseRepository<Category> _repo;
        private readonly IMapper _mapper;
        public CategoriesController(IBaseRepository<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryCreateDto cat)
        {
            if (!ModelState.IsValid || cat == null)
            {
                return BadRequest(ModelState);
            }

                var result = _mapper.Map<Category>(cat);
                await _repo.AddAsync(result);
                if (await _repo.SaveAll())
                {
                    return CreatedAtRoute("GetCategory", new { id = result.Id }, result);
                }

            throw new Exception("Creating the category failed on save");
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _repo.Get(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationParams pagParams)
        {
            try
            {
                var categories = await _repo.Get(pagParams);
                if (categories == null)
                {
                    return NotFound();
                }
                var catToReturn = _mapper.Map<IEnumerable<CategoryListDto>>(categories);
                Response.AddPagination(categories.CurrentPage,categories.PageSize,categories.TotalCount, categories.TotalPages);
                return Ok(catToReturn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto cat, int id)
        {
            if (!ModelState.IsValid || cat == null)
            {
                return BadRequest(ModelState);
            }

            var catFromRepo = await _repo.Get(id);
            if (catFromRepo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map(cat, catFromRepo);
            await _repo.UpdateAsync(result);
 
            if (await _repo.SaveAll())
            {
                var catUpdated = _mapper.Map<CategoryUpdateDto>(result);
                return Ok(catUpdated);
            }

            throw new Exception($"Error updating category {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var catToRemove = await _repo.Get(id);
            if(catToRemove == null)
            {
                return NotFound();
            }
            await _repo.RemoveAsync(catToRemove);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the category");
        }
    }
}