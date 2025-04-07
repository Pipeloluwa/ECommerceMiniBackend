namespace DomainLayer.DTOs
{
    public class RefreshTokenDto
    {
        public required string JWTToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
