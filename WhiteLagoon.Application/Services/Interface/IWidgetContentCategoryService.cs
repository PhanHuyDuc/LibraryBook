using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IWidgetContentCategoryService
    {
        IEnumerable<WidgetContentCategory> GetAllWidgetContentCategory();
        WidgetContentCategory GetWidgetContentCategoryById(int id);
        void CreateWidgetContentCategory(WidgetContentCategory widgetContentCategory);
        void UpdateWidgetContentCategory(WidgetContentCategory widgetContentCategory);
        bool DeleteWidgetContentCategory(int id);
    }
}
