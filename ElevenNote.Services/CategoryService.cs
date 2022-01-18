using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService (Guid id)
        {
            _userId = id;
        }

        public bool CreateCategory(CatgoryCreate model)
        {
            var category = new Category()
            {
                Name = model.Name,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(category);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Categories
                    .Where(c => c.OwnerId == _userId)
                    .Select(c => new CategoryListItem
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        CreatedUtc = c.CreatedUtc
                    });
                return query.ToArray();
            }
        }
    }
}
