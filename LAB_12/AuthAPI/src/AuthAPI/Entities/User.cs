﻿namespace AuthAPI
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; } 
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
    }
}