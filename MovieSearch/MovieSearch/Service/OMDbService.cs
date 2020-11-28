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

        public async Task<Result<IEnumerable<MovieShortData>>> FindMovies(string value)
        {
            var response = await "http://www.omdbapi.com/"
                .SetQueryParams(new
                {
                    s = value,
                    apikey = _key,
                    type = "movie"
                })
                .AllowAnyHttpStatus()
                .GetJsonAsync<MovieSearchResult>();

            if(!response.Response)
            {
                return Result.Error<IEnumerable<MovieShortData>>(response.Error);
            }

            return Result.Ok(response.Search);
        }

        public async Task<Result<MovieFullData>> GetMovieData(string title)
        {
            var response = await "http://www.omdbapi.com/"
                .SetQueryParams(new
                {
                    t = title,
                    apikey = _key,
                    type = "movie"
                })
                .AllowAnyHttpStatus()
                .GetJsonAsync<MovieFullData>();

            if (!response.Response)
            {
                return Result.Error<MovieFullData>(response.Error);
            }

            return Result.Ok(response);
        }
    }
}
