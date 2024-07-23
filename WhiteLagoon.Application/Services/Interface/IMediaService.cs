using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IMediaService
    {
        IEnumerable<Media> GetAllMedia();
        Media GetMediaById(int id);
        void CreateMedia(Media media);
        void UpdateMedia(Media media);
        bool DeleteMedia(int id);
        IEnumerable<Media> GetMediaByCategory(string mediaCategory);
    }
}
