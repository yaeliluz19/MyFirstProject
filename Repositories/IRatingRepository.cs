
using MyFirstProject;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> CreateRating(Rating rating);
    }
}