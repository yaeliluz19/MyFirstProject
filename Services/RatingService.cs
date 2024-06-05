using MyFirstProject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _RatingRepository;
        public RatingService(IRatingRepository RatingRepository)
        {
            _RatingRepository = RatingRepository;
        }
        public async Task<Rating> CreateRating(Rating rating)
        {
            return await _RatingRepository.CreateRating(rating);
        }
    }
}
