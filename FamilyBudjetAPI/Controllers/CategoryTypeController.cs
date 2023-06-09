using AutoMapper;
using FamilyBudjetAPI.DTOModels;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudjetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Google,User")]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly IMapper _mapper;

        public CategoryTypeController(ICategoryTypeService categoryTypeService, IMapper mapper)
        {
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryTypeDto>> GetCategoryTypes()
        {
            var categoryTypes = _categoryTypeService.GetCategoryTypes();

            return Ok(categoryTypes);
        }

        [HttpPost]
        public ActionResult<CategoryTypeDto> CreateCategoryType(CategoryTypeDto categoryTypeDto)
        {
            var categoryType = _mapper.Map<CategoryType>(categoryTypeDto);
            var createdCategoryType = _categoryTypeService.CreateCategoryType(categoryType);
            var createdCategoryTypeDto = _mapper.Map<CategoryTypeDto>(createdCategoryType);
            return Ok(createdCategoryTypeDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryTypeDto> GetCategoryType(int id)
        {
            try
            {
                var categoryType = _categoryTypeService.GetCategoryType(id);
                var categoryTypeDto = _mapper.Map<CategoryTypeDto>(categoryType);
                return Ok(categoryTypeDto);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryType(int id, CategoryTypeDto updatedCategoryTypeDto)
        {
            try
            {
                var updatedCategoryType = _mapper.Map<CategoryType>(updatedCategoryTypeDto);
                _categoryTypeService.UpdateCategoryType(id, updatedCategoryType);
                return NoContent();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryType(int id)
        {
            try
            {
                _categoryTypeService.DeleteCategoryType(id);
                return NoContent();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}