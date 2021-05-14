using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DDD.Domain.Models
{
    public class Authenticate
    {
        public int UserId { get; }
        public string UserName { get; }
        public Authenticate(
            int userId,
            string userName)
        {
            UserId = userId;
            UserName = userName;
        }
        public ClaimsIdentity CreateClaimsIdentity()
        {
            Claim[] claims = {
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(ClaimTypes.Name, UserName),
            };
            return new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
