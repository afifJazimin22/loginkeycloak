using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeycloakLoginMicroservice.Model
{
    public class LoginResponse
    {
        public string? access_token { get; set; }
        public string expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string refresh_expires_in { get; set; }
        public string? token_type { get; set; }
        public string? error_description { get; set; }
        public string? error { get; set; }
    }
}