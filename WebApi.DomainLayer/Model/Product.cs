using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApi.DomainLayer.Model
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string ImgUri { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
