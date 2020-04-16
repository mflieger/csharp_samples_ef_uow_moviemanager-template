using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Linq;

namespace MovieManager.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Movie GetLongestMovie() => _dbContext
            .Movies
            .OrderByDescending(m => m.Duration)
            .ThenBy(m => m.Title)
            .First();
            
    }
}