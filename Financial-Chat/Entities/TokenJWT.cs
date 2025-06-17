namespace Financial.Chat.Domain.Shared.Entity
{
    public class TokenJWT
    {
        public TokenJWT(bool authenticated, string token)
        {
            Authenticated = authenticated;
            Token = token;
        }

        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
    }
}
