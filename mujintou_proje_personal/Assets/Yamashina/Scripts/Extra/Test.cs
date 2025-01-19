using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Test 
{
    public static async Task Main(string[] args)
    {
        string authorizationCode = "AUTHORIZATION_CODE"; // 取得した認証コード
        string clientId = "YOUR_CLIENT_ID";
        string clientSecret = "YOUR_CLIENT_SECRET";
        string redirectUri = "YOUR_REDIRECT_URI";

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", authorizationCode },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            })
        };

        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        Console.WriteLine($"Access Token: {tokenResponse.AccessToken}");
        Console.WriteLine($"Refresh Token: {tokenResponse.RefreshToken}");
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
