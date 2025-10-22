using Ecom_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Reposetores.Service
{
    public class ImageManagemintService : IImageManagmentService
    {
        //save

        public ImageManagemintService()
        {
        }

        public async Task<List<string>> Addimage(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);

            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    //get image name
                    var ImageName = item.FileName;
                    var ImageSrc = $"Images/{src}/{ImageName}";
                    var root = Path.Combine(ImageDirectory, ImageName);
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);
                }
            }
            return SaveImageSrc;
        }

        public void Deleteimage(string src)
        {
            try
            {
                var rootPath = Path.Combine("wwwroot", src);
                var fullPath = Path.GetFullPath(rootPath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ أثناء حذف الملف: {ex.Message}");
            }
        }
    }
}
