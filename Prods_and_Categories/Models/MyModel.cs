using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace Prods_and_Categories.Models
{

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Association> ProductA { get; set; }

    }

    public class Association
    {
        [Key]
        public int AssociationId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Product Products { get; set; }
        public Category Categories { get; set; }

    }

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Association> CategoryA { get; set; }


    }
}