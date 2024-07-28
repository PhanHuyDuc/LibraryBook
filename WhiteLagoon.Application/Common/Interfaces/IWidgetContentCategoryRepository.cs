﻿using LibraryBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Application.Common.Interfaces
{
    public interface IWidgetContentCategoryRepository : IRepository<WidgetContentCategory>
    {
        void Update(WidgetContentCategory entity);
    }
}
