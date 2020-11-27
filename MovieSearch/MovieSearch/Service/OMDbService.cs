using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using MovieSearch.Data;
using MovieSearch.Helpness;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearch.Service
{
    public class OMDbService : IOMDbService
    {
        private readonly string _key;

        public OMDbService(IConfiguration configuration)
        {
            _key = configuration.GetValue<string>("OMDb:Key");
        }

        public async Task<Result<IEnumerable<Movie>>> FindMovies(string value)
        {
            var response = await "http://www.omdbapi.com/"
                .SetQueryParams(new
                {
                    s = value,
                    apikey = _key,
                    type = "movie"
                })
                .AllowAnyHttpStatus()
                .GetJsonAsync<MovieSearchDto>();

            if(!response.Response)
            {
                return Result.Error<IEnumerable<Movie>>(response.Error);
            }

            return Result.Ok(response.Search);
        }

        public async Task<Result<MovieData>> GetMovieData(string title)
        {
            var response = await "http://www.omdbapi.com/"
                .SetQueryParams(new
                {
                    t = title,
                    apikey = _key,
                    type = "movie"
                })
                .AllowAnyHttpStatus()
                .GetJsonAsync<MovieData>();

            if (!response.Response)
            {
                return Result.Error<MovieData>(response.Error);
            }

            return Result.Ok(response);
        }
    }
}
