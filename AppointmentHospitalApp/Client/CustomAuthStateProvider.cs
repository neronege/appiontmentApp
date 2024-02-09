using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace AppointmentHospitalApp.Client
{

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //Burada aldığımız tokenları nasıl bir düzen içerisinde yerleştireceğimizi yazıyor.
        //tokenların burada işlenip localstage'e işleecektir
        //client tarafından tokenleri burada alıp benckende göndriyoruz
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;



        public CustomAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;

        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");
            var identy = new ClaimsIdentity();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            //Jwt'nin parse edilmesi
            if (!string.IsNullOrEmpty(authToken))
            {

                try
                {
                    identy = new ClaimsIdentity(ParseClaimFromJwt(authToken), "jwt");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken.Replace("\"", " "));
                }
                catch (Exception)
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identy = new ClaimsIdentity();
                }
            }
            var user = new ClaimsPrincipal(identy);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
        public static IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payLoad = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payLoad);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;

            }
            return Convert.FromBase64String(base64);

        }
    }
}
