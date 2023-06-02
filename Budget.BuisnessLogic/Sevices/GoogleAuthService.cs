using Budget.BuisnessLogic.Models;
using Budget.BuisnessLogic.Sevices.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Budget.BuisnessLogic.Sevices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private IConfigurationRoot _configuration;
        private string _clientId;
        private string _clientSecret;
        private string _redirectUri;
        private string _tokenUrl;
        private string _scope;

        public GoogleAuthService()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
            _clientId = _configuration["GoogleAuth:ClientId"];
            _clientSecret = _configuration["GoogleAuth:ClientSecretKey"];
            _redirectUri = _configuration["GoogleAuth:RedirectUri"];
            _tokenUrl = _configuration["GoogleAuth:TokenUrl"];
            _scope = _configuration["GoogleAuth:Scope"];
        }

        public string GetAuthUrl()
        {
            var responseType = "code";
            var state = Guid.NewGuid().ToString();
            var authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_clientId}&redirect_uri={_redirectUri}&response_type={responseType}&scope={_scope}&state={state}";

            return authUrl;
        }

        public async Task<string> GetToken(string code)
        {
            var requestBody = CreateRequestBody(code);
            var client = new HttpClient();

            var response = await client.PostAsync(_tokenUrl, requestBody);

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while retrieving Google access token.");
            }

            return tokenResponse.TokenId;
        }

        private FormUrlEncodedContent CreateRequestBody(string code)
        {
            return new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", _redirectUri),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
        });
        }
    }
}