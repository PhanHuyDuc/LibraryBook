using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Implementation
{
    public class WidgetContentService : IWidgetContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WidgetContentService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateWidgetContent(WidgetContent widgetContent)
        {
            _unitOfWork.WidgetContent.Add(widgetContent);
            _unitOfWork.Save();
        }

        public bool DeleteWidgetContent(int id)
        {
            try
            {
                WidgetContent? objFromDb = _unitOfWork.WidgetContent.Get(u => u.Id == id);
                if (objFromDb is not null)
                {  
                    _unitOfWork.WidgetContent.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<WidgetContent> GetAllWidgetContent()
        {
           return _unitOfWork.WidgetContent.GetAll(includeProperties:"WidgetContentCategory");
        }

        public IEnumerable<WidgetContent> GetWidgetContentByCategory(string widgetContentCategory)
        {
            return _unitOfWork.WidgetContent.GetAll(includeProperties: "WidgetContentCategory").Where(u=>u.WidgetContentCategory.Name == widgetContentCategory);
        }

        public WidgetContent GetWidgetContentById(int id)
        {
            return _unitOfWork.WidgetContent.Get(u=>u.Id == id, includeProperties: "WidgetContentCategory");
        }

        public void UpdateWidgetContent(WidgetContent widgetContent)
        {            
            _unitOfWork.WidgetContent.Update(widgetContent);
            _unitOfWork.Save();
        }
    }
}
