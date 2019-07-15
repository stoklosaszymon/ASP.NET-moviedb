using System.Collections.Generic;
using System.Linq;

namespace aaaaaa.Classes
{
    public class DbQuerry
    {
        public MovieDBEntities context = new MovieDBEntities();
        public Movies GetMovie(string title)
        {
            return context.Movies.Where(n => n.Title == title).FirstOrDefault();
        }

        public List<Movies> GetAllMovies()
        {
            return context.Movies.ToList();
        }
    }
}