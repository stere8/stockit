﻿using System.ComponentModel.DataAnnotations;

public class ProductEditViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)] // Ensure positive quantity
    public int Quantity { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [Range(0, double.MaxValue)] // Allow non-negative price
    public decimal Price { get; set; }
    public string ImagePaths { get; set; }
    public IFormFile[] Images { get; set; } // Array of IFormFile for multiple uploads
    public List<string> ImagesList { get; set; }
    public string DeleteImage { get; set; } // Optional flag for deleting image

}