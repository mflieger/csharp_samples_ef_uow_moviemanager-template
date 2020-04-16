using System.Collections.Generic;
using MovieManager.Core.Entities;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        Category GetCategoryWithMostMovies();
    }
}
