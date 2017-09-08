﻿using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
    public interface ILoginLogRepository
    {
        void AddLoginlog(LoginLog model);

        Tuple<int,List<LoginLog>> GetLoginlog(LoginLog model); 
    }
}
