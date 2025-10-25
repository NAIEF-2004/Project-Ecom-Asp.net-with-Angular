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

        public void Deleteimage(string relativeFolderPath)
        {
            try
            {
                var rootPath = Path.Combine("wwwroot", relativeFolderPath);
                var fullPath = Path.GetFullPath(rootPath);

                if (Directory.Exists(fullPath))
                {
                    Directory.Delete(fullPath, recursive: true); // يحذف المجلد وكل محتوياته
                    Console.WriteLine("تم حذف المجلد بنجاح.");
                }
                else
                {
                    Console.WriteLine("المجلد غير موجود.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ أثناء حذف المجلد: {ex.Message}");
            }
        }
    }
}
