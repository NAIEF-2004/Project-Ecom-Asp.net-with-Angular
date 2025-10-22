using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Services
{
   public  interface IImageManagmentService
    {
        //استخدمت لست ممكن يعطي اكثر من صورة لاضيفها للمنتج
        Task<List<string>> Addimage(IFormFileCollection file,string src );
        void Deleteimage(string src);
    }
}
