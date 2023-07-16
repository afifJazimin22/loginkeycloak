using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeycloakLoginMicroservice.Helper;
using KeycloakLoginMicroservice.Model;
using Newtonsoft.Json;

namespace KeycloakLoginMicroservice.Services
{
    public class LoginService : ServerCertificate, ILoginService
    {
        private readonly IConfiguration _configuration;

        HttpClientHandler handler = new HttpClientHandler();

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ServiceResponse> Login(UserCredential user)
        {
            handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;
            HttpClient client = new HttpClient(handler);

            var url = _configuration["Keycloak:url"];
            var clientId = _configuration["Keycloak:clientId"];
            var clientSecret = _configuration["Keycloak:clientSecret"];
            var username = user.username;
            var password = user.password;


            System.Console.WriteLine(url);

            var request = new HttpRequestMessage(HttpMethod.Post, url + "/token");

            var collection = new List<KeyValuePair<string,string>>();
                collection.Add(new ("client_id", clientId));
                collection.Add(new ("client_secret", clientSecret));
                collection.Add(new ("username", username));
                collection.Add(new ("password", password));
                collection.Add(new ("grant_type", "password"));

            var content = new FormUrlEncodedContent(collection);

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            var jsonPayload = JsonConvert.DeserializeObject<LoginResponse>(result);

            var error = jsonPayload.error_description;

            response.EnsureSuccessStatusCode();

            System.Console.WriteLine(error);

            ServiceResponse sr = new ServiceResponse();


            if(response.IsSuccessStatusCode)
            {
                sr.success = true;
                sr.status = "Authorized";
                sr.message = "User logged in successfully";
                System.Console.WriteLine(sr.message);
                sr.accessToken = jsonPayload.access_token;
                sr.expiresIn = Convert.ToInt32(jsonPayload.expires_in);
                sr.refreshToken = jsonPayload.refresh_token;
                sr.refreshExpireIn = Convert.ToInt32(jsonPayload.refresh_expires_in);
            } 
                sr.status = jsonPayload.error;
                sr.success = false;
                sr.message = jsonPayload.error_description;

            

            return sr;
            //to be continued...
        }
    }
}