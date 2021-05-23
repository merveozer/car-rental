using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
   public interface IAuthenticationService
    {
        Response<ClaimsPrincipal> Register(UserRegisterDTO user);
        Response<ClaimsPrincipal> Login(UserLoginDTO user);
    }
}
