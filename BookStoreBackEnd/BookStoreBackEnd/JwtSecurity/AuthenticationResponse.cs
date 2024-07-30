namespace BookStoreBackEnd.JwtSecurity
{
    public class AuthenticationResponse
    {

      public string? Username { get; set; }
        public string Token { get; set; } // Property to hold JWT token
        public int RoleId { get; set; } // Add Role property
    }
}
