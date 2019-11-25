using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CPUWS.DAL.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter a Socket Type")]
        public string Socket { get; set; }
        [Required(ErrorMessage = "Please enter core number")]
        public int CoresCount { get; set; }
    }
}
