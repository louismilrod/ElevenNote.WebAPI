using ElevenNote.Models.CategoryModels;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly int _categoryId;
        private CategoryService CreateCategoryService()
        {
            return new CategoryService(_categoryId);
        }

        public IHttpActionResult Get() 
        {
            CategoryService categoryService = CreateCategoryService();
            var categories = categoryService.GetCategory();
            return Ok(categories);
        }

        public IHttpActionResult Post(CatgoryCreate category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category)) return InternalServerError();

            return Ok();
        }
    }
}
