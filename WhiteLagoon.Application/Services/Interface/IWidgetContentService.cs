using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IWidgetContentService
    {
        IEnumerable<WidgetContent> GetAllWidgetContent();
        WidgetContent GetWidgetContentById(int id);
        void CreateWidgetContent(WidgetContent widgetContent);
        void UpdateWidgetContent(WidgetContent widgetContent);
        bool DeleteWidgetContent(int id);
        IEnumerable<WidgetContent> GetWidgetContentByCategory(string widgetContentCategory);
    }
}
