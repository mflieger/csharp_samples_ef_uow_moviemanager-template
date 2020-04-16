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

        public Category GetCategoryWithMostMovies() => _dbContext
            .Categories
            .OrderByDescending(c => c.Movies.Count())
            .First();
    }
}