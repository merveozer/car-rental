using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services.Concrete
{
    internal interface IUserOperationClaimService
    {
        List<OperationClaim> GetClaims(int ıd);
    }
}