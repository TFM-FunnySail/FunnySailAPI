using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
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
    }
}
