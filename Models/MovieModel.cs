using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TrainingDotNetCoreMVC.Models
{
    public class MovieModel
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; } 
        public string coverImg { get; set; }
        [Required]
        public DateTime? releaseDate { get; set; }
        [Required]
        public string genre { get; set; }
        public int duration { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? modifyDate { get; set; }
    }
}
