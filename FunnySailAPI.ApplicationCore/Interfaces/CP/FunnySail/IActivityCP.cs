using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IActivityCP
    {
        Task<int> AddImage(int id, IFormFile image, bool main);
    }
}
