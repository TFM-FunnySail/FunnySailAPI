using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Output.Account
{
    public class AuthenticateResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
