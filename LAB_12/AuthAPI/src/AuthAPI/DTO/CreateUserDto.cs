namespace AuthAPI
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string Username { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public required string Password { get; set; }

        public User Map()
        {
            return new User
            {
                Username = Username,
                Password = Password
            };
        }
    }
}