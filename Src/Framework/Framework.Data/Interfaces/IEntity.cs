﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
