using Application.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Common
{
    public abstract class BaseService
    {
        private protected ICarRentalDbContext Context { get; }
    
        public BaseService(ICarRentalDbContext context)
        {
            Context = context;
        }
    
    
    
    }
}
