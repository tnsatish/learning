using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Services
{
    public interface IIdentityService
    {
        bool IsAuthenticated();
        string GetMail();
        string GetId();
    }

    public class AzureAdIdentityService : IIdentityService
    {
        public const string ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        public const string Scope = "http://schemas.microsoft.com/identity/claims/scope";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AzureAdIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetMail()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public string GetId()
        {
            var idClaims = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ObjectId);

            return idClaims?.Value;
        }
    }
}
