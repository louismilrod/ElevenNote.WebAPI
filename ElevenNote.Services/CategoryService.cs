using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        private readonly ApplicationUser _user;

        public CategoryService (Guid id)
        {
            _userId = id;
        }

        public bool CreateCategory(CatgoryCreate model)
        {
            var category = new Category()
            {
                OwnerId = _userId,
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
                    
                    .Select(c => new CategoryListItem
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        CreatedUtc = c.CreatedUtc,
                        ModifiedUtc = c.ModifiedUtc
                    });
                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == id);
                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CategoryId,
                        Name = entity.Name,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == model.CategoryId);

                entity.Name = model.Name;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(c => c.CategoryId == noteId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
