using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeycloakLoginMicroservice.Model
{
    public class UserCredential
    {
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
    }
}