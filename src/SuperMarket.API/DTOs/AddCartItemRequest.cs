using System.ComponentModel.DataAnnotations;

namespace SuperMarket.API.DTOs
{
    public class AddCartItemRequest
    {
        [Required]
        public required string Sku { get; set; }
    }
}
