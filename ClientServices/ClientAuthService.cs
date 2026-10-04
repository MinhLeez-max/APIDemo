using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;

namespace APIdemo.ClientServices
{
    public class ClientAuthService
    {
        private const string TokenKey = "qlkh_token";
        private readonly IJSRuntime _js;

        public string Token { get; private set; } = string.Empty;

        public ClientAuthService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var t = await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
                Token = t ?? string.Empty;
            }
            catch { Token = string.Empty; }
        }

        public async Task SaveTokenAsync(string token)
        {
            Token = token ?? string.Empty;
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, Token);
        }

        public async Task ClearTokenAsync()
        {
            Token = string.Empty;
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        public void ConfigureClient(System.Net.Http.HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrWhiteSpace(Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
        }
    }
}
