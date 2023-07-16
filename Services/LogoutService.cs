using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeycloakLoginMicroservice.Helper;
using KeycloakLoginMicroservice.Model;

namespace KeycloakLoginMicroservice.Services
{
    public class LogoutService : ServerCertificate, ILogoutService
    {
        private readonly IConfiguration _config;

        public LogoutService(IConfiguration config)
        {
            _config = config;
        }

        HttpClientHandler handler = new HttpClientHandler();


        public async Task<LogoutResponse> Logout(string refreshToken){
            handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;
            HttpClient client = new HttpClient(handler);

            var url = _config["Keycloak:url"];
            var clientId = _config["Keycloak:clientId"];
            var clientSecret = _config["Keycloak:clientSecret"];


            System.Console.WriteLine(url);

            var request = new HttpRequestMessage(HttpMethod.Post, url + "/logout");

            var collection = new List<KeyValuePair<string,string>>();
                collection.Add(new ("client_id", clientId));
                collection.Add(new ("client_secret", clientSecret));
                collection.Add(new ("refresh_token", refreshToken));

            var content = new FormUrlEncodedContent(collection);

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            
            LogoutResponse lr = new LogoutResponse();

            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {
                lr.message = "Successfully logged out";
                lr.status = "Success";
            }

            return lr;
        }
    }
}