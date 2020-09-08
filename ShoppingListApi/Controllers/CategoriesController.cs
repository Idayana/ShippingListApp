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
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _catRepo;
        public CategoriesController(IMapper mapper, ICategoryRepository catRepo)
        {
  
            _mapper = mapper;
            _catRepo = catRepo;

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryCreateDto cat)
        {
            if (!ModelState.IsValid || cat == null)
            {
                return BadRequest(ModelState);
            }
            
            var result = _mapper.Map<Category>(cat);

            var cantName = _catRepo.FindByName(cat.CategoryName);
            if(cantName > 0)
            {
                return Conflict();
            }

            await _catRepo.AddAsync(result);
            if (await _catRepo.SaveAll())
            {
                var catMapped = _mapper.Map<CategoryListDto>(result);
                return CreatedAtRoute("GetCategory", new { id = catMapped.CategoryId }, catMapped);
            }

            throw new Exception("Creating the category failed on save");
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _catRepo.Get(id);
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
                var categories = await _catRepo.Get(pagParams);
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

        [HttpGet("allCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var cat = await _catRepo.GetCategories();
                if(cat == null)
                {
                    return NoContent();
                }
                var catList = _mapper.Map<IEnumerable<CategoryListDto>>(cat);
                return Ok(catList);
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

            var catFromRepo = await _catRepo.Get(id);
            if (catFromRepo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map(cat, catFromRepo);

            var cantName = _catRepo.FindByName(cat.CategoryName);
            if (cantName > 0)
            {
                return Conflict();
            }

            await _catRepo.UpdateAsync(result);
 
            if (await _catRepo.SaveAll())
            {
                var catUpdated = _mapper.Map<CategoryUpdateDto>(result);
                return Ok(catUpdated);
            }

            throw new Exception($"Error updating category {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var catToRemove = await _catRepo.Get(id);
            if(catToRemove == null)
            {
                return NotFound();
            }
            await _catRepo.RemoveAsync(catToRemove);

            if (await _catRepo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the category");
        }
    }
}