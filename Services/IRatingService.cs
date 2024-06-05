using MyFirstProject;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> CreateRating(Rating rating);
    }
}