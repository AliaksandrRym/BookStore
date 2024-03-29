﻿namespace BookStore.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string? Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
    }
}
