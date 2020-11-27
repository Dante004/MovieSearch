using MovieSearch.Data;
using MovieSearch.Helpness;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearch.Service
{
    public interface IOMDbService
    {
        Task<Result<IEnumerable<Movie>>> FindMovies(string value);
        Task<Result<MovieData>> GetMovieData(string title);
    }
}
