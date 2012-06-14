using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;

namespace MyanmarCV.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title apw is required")]
        public string Title { get; set; }        

        [Required(ErrorMessage="Date is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1,100,ErrorMessage="Price must be between $1 and $100")]
        public decimal Price { get; set; }

        [StringLength(5)]
        public string Rating { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}