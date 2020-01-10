using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabaseApi
{
    public class GenreResult
    {
        public List<Genre> genres { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    
}
