using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Genre")]
        [Required]
        public byte? GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name="Number In Stock")]
        [Range(1,20)]
        public byte? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 
                    ? "Edit Movie" 
                    : "New Movie";
            }
        }

        public NewMovieViewModel()
        {
            Id = 0;
        }

        public NewMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            DateAdded = DateTime.Now;
        }
    }
}