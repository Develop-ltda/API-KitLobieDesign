using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KitLobieDesign.Models
{
    public class Kit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public KitType Type {get; set;}

        public List<Category> Categories { get; set; } = new List<Category>();
    }

        public enum KitType
        {
            Standard,
            Premium,
            Deluxe
        }

        public class Category
        {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        
        [JsonIgnore]
        public List<Item> Items { get; set; } = new List<Item>();
        public int KitId { get; set; }
    }
      public class Item
      {
          public int Id { get; set; }

          [Required]
          public string Name { get; set; } = string.Empty;

         [Required]
         [Column(TypeName = "decimal(18,2)")]
         public decimal Price { get; set; }

          [ForeignKey("CategoryId")]
          public int CategoryId { get; set; }
          

          [JsonIgnore]
          public Category? Category { get; set; }
      }


}
    
