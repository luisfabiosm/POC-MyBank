using Domain.Core.Base;

namespace Domain.Core.Models.Response
{
    public record GetTokenResponse : BaseTransactionResponse
    {
        public string BearerToken { get; set; }
        public DateTime Expiration { get;  }


        public GetTokenResponse(string bearerToken, DateTime expiration)
        {
            this.BearerToken = bearerToken;
            this.Expiration = expiration;
        }
    }
}
