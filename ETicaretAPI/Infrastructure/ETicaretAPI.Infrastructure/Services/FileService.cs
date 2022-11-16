using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Operations;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnv;

        public FileService(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;

            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }

        }

        //bool first recursive e sokcak bizi
        /* AMAÇ →
         * elma.jp varsa elna-2.jpg elde etcez
         */
        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run(async () =>
               {
                   //filename deki extension'ı al
                   string extension = Path.GetExtension(fileName);
                   // without extension name al

                   string newFileName = string.Empty;
                   //ilk turdaysak bu işlemleri yap
                   if (!first)
                   {
                       string oldName = Path.GetFileNameWithoutExtension(fileName);
                       newFileName = $"{NameOperations.CharacterRegulatory(oldName)}{extension}";
                   }
                   else
                   {
                       newFileName = fileName;
                       int indexNo1 = newFileName.LastIndexOf('-');

                       if (indexNo1 == -1)
                           newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                       else
                       {
                           int indexno2 = newFileName.IndexOf(".");
                           string fileNo = newFileName.Substring(indexNo1, indexno2 - indexNo1 - 1);
                           int _fileNo = int.Parse(fileNo);
                           _fileNo++;


                           newFileName = newFileName.Remove(indexNo1, indexno2 - indexNo1 - 1)
                           .Insert(indexNo1, _fileNo.ToString());

                       }
                   }

                   if (File.Exists($"{path}\\{newFileName}"))
                       return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)} -2{extension}", false);
                   else
                       return newFileName;
               });
            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnv.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            return null;
            //todo düzenleme yapılacak
        }
    }
}
