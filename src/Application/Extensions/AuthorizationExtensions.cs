﻿using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions //.NetCore un tanımladığı claimse metot ekliyoruz aslında...
{
    public static class AuthorizationExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Count(c => c.Type == ClaimTypes.Role && c.Value == AuthenticationConstants.OperationClaims.AdminStr) > 0;
        }
    }
}
