using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeycloakLoginMicroservice.Model;

namespace KeycloakLoginMicroservice.Services
{
    public interface ILoginService
    {
        Task<ServiceResponse> Login(UserCredential user);
    }
}