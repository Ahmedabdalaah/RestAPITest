﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPITest.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }   
        public string? Notes { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey(nameof(category))]
        public int CategoryId { get; set; }
        public Category? category { get; set; }

    }
}
