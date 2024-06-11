namespace Vegist.Extentions
{
    public static class FileExtensions
    {
        public static bool CheckFileSize(this IFormFile file, int maxSizeInMB)
        {
            return file.Length <= maxSizeInMB * 1024 * 1024;
        }

        public static bool CheckFileType(this IFormFile file, string fileType)
        {
            return file.ContentType.StartsWith(fileType, StringComparison.OrdinalIgnoreCase);
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string rootPath, params string[] folders)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(rootPath, Path.Combine(folders), fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public static void DeleteFile(this string fileName, string rootPath, params string[] folders)
        {
            var path = Path.Combine(rootPath, Path.Combine(folders), fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    //public static class FileExtension
    //{
    //    public static async Task<string> SaveFileAsync(this IFormFile file, string root, string Client, string assets, string folderName)
    //    {
    //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    //        string path = Path.Combine(root, Client, assets, folderName, uniqueFileName);


    //        using FileStream fs = new FileStream(path, FileMode.Create);

    //        await file.CopyToAsync(fs);

    //        return uniqueFileName;
    //    }

    //    public static bool CheckFileType(this IFormFile file, string fileType)
    //    {
    //        if (file.ContentType.Contains(fileType))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public static bool CheckFileSize(this IFormFile file, int fileSize)
    //    {
    //        if (file.Length > fileSize * 1024 * 1024)
    //        {
    //            return false;
    //        }
    //        return true;
    //    }

    //    public static void DeleteFile(this IFormFile file, string root, string Client, string assets, string folderName, string fileName)
    //    {
    //        string path = Path.Combine(root, Client, assets, folderName, fileName);
    //        if (System.IO.File.Exists(path))
    //        {
    //            System.IO.File.Delete(path);
    //        }
    //    }
    //}
}
