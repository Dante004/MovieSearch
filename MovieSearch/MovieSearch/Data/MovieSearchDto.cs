using System.Collections.Generic;

namespace MovieSearch.Data
{
    public class MovieSearchDto
    {
        public IEnumerable<Movie> Search { get; set; }
        public int TotalResults { get; set; }
        public bool Response { get; set; }
        public string Error { get; set; }
    }
}
