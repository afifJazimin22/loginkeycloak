using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeycloakLoginMicroservice.Model
{
    public class ServiceResponse
    {
        public bool success { get; set; } = false;
        public string status { get; set; }
        public string message { get; set; }
        public string? accessToken { get; set; }
        public int expiresIn { get; set; }
        public string? refreshToken { get; set; }
        public int refreshExpireIn { get; set; }
    }
}