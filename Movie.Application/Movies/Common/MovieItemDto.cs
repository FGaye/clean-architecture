using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Application.Movies.Commands.CreateMovie
{
    public class MovieItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set;}
    }
}