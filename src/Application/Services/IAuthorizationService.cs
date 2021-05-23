using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthorizationService
    {
        ClaimsPrincipal GetClaimsPrincipal(User user, List<OperationClaim> operationClaims);
        object GetClaimsPrincipal(User user, object operationClaims);
    }
}
