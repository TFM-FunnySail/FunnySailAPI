using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IResourcesCEN
    {
        Task<int> AddResources(bool main, ResourcesEnum type, string uri)
Task<string> UploadImage(IFormFile image);
    }
}
