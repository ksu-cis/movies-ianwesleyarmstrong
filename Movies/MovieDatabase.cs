using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search (string searchString)
        {
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.Title != null && movie.Title.ToLower().Contains(searchString.ToLower()))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public List<Movie> SearchAndFilter(string searchString, List<string> ratings)
        {
            //case 0: no string, no ratings
            if (searchString == null && ratings.Count == 0) return All;

            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                //both
                if (searchString != null && ratings.Count > 0)
                {
                    if (movie.Title != null && movie.Title.ToLower().Contains(searchString.ToLower()) && 
                        ratings.Contains(movie.MPAA_Rating))
                    {
                        results.Add(movie);
                    }
                }
                else if (searchString != null) {
                    // case 2: search only
                    if (movie.Title != null && movie.Title.ToLower().Contains(searchString.ToLower()))
                    {
                        results.Add(movie);
                    }
                }
                //ratings
                else if (ratings.Count > 0)
                {
                    if (ratings.Contains(movie.MPAA_Rating))
                    {
                        results.Add(movie);
                    }
                }
                    
                
                
                // case 1: string and ratings


                // case 3: ratings only
            }
            return results;
        }
    }
}
