using System.Collections.Generic;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Linq;

namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public (string, int) GetCategoryWithMostMovies() => _dbContext
        //    .Categories
        //    .Select(c => new
        //    {
        //        CatName = c.CategoryName,
        //        Count = c.Movies.Count()
        //    })
        //    .OrderByDescending(m => m.Count)
        //    .FirstOrDefault();
    }
}