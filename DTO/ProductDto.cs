﻿namespace BookStore.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public string? Image_Path { get; set; }
    }
}
