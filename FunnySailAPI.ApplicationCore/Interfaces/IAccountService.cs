using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponseDTO> LoginUser(LoginUserInputDTO loginUserInput, string ipAddress);
        Task<AuthenticateResponseDTO> RefreshToken(string token, string ipAddress);
        Task<bool> IsOwnsToken(UsersEN user, string token);
        Task RevokeToken(string token, string v);
    }
}
