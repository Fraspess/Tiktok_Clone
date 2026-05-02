namespace Application.Dtos.User
{
    public class SeedUserDTO
    {
        public String? Email { get; set; }
        public String? Username { get; set; }
        public String? LastName { get; set; }

        public String? FirstName { get; set; }

        public String? Image { get; set; }

        public String? Password { get; set; }

        public String[]? Roles { get; set; }
    }
}