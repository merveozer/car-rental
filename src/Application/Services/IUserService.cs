﻿using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
   public interface IUserService
    {
        Response<User> Add(User user);
        User GetByEmail(string eMail);
    }
}
