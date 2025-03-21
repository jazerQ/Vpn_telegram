﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Abstractions
{
    public interface IUserPaymentsRepository
    {
        Task AddNewUserPayment(long telegramId);
        Task<UserPayments> GetUserById(long telegramId);
    }
}
