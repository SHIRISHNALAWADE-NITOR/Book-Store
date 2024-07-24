namespace BookStoreBackEnd.JwtSecurity
{
    public interface IAuthService
    {
        public AuthenticationResponse Authenticate(AuthenticationModel authenticationModel);
    
    }
}
